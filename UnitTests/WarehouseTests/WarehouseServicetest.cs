using API.Implementations.Domain;
using API.Models.Logistics.Warehouses;
using API.Services.Logistics;
using Moq;
using softserve.projectlabs.Shared.Utilities;
using API.Models.IntAdmin;
using API.Data.Repositories.LogisticsRepositories.Interfaces;

[TestFixture]
public class WarehouseServiceTests
{
    private Mock<IWarehouseRepository> _repoMock;
    private Mock<WarehouseDomain> _domainMock;
    private WarehouseService _service;

    [SetUp]
    public void SetUp()
    {
        _repoMock = new Mock<IWarehouseRepository>();
        _domainMock = new Mock<WarehouseDomain>(_repoMock.Object) { CallBase = false };
        _service = new WarehouseService(_domainMock.Object);
    }

    [Test]
    public async Task GetAllWarehousesAsync_ReturnsMappedDtos_WhenDomainSuccess()
    {
        var warehouse = new Warehouse(1, "W1", "Loc", 10, 1, new List<Item>
        {
            new Item { ItemId = 1, Sku = 100, ItemName = "Item1", ItemDescription = "Desc", CurrentStock = 5 }
        });
        var domainResult = Result<List<Warehouse>>.Success(new List<Warehouse> { warehouse });

        _domainMock.Setup(d => d.GetAllWarehousesAsync()).ReturnsAsync(domainResult);

        var result = await _service.GetAllWarehousesAsync();

        Assert.That(result.Count, Is.EqualTo(1)); // Should return one warehouse DTO
        Assert.That(result[0].WarehouseId, Is.EqualTo(1)); // WarehouseId should be 1
        Assert.That(result[0].Name, Is.EqualTo("W1")); // Name should be "W1"
        Assert.That(result[0].Items.Count, Is.EqualTo(1)); // Should have one item
        Assert.That(result[0].Items[0].Sku, Is.EqualTo(100)); // Item Sku should be 100
    }

    [Test]
    public async Task GetAllWarehousesAsync_ReturnsEmptyList_WhenDomainFails()
    {
        var domainResult = Result<List<Warehouse>>.Failure("error");
        _domainMock.Setup(d => d.GetAllWarehousesAsync()).ReturnsAsync(domainResult);

        var result = await _service.GetAllWarehousesAsync();

        Assert.That(result, Is.Not.Null); // Should not be null even if domain fails
        Assert.That(result.Count, Is.EqualTo(0)); // Should return an empty list
    }

    [Test]
    public async Task GetWarehouseByIdAsync_ReturnsMappedDto_WhenDomainSuccess()
    {
        var warehouse = new Warehouse(1, "W1", "Loc", 10, 1, new List<Item>());
        var domainResult = Result<Warehouse>.Success(warehouse);

        _domainMock.Setup(d => d.GetWarehouseByIdAsync(1)).ReturnsAsync(domainResult);

        var result = await _service.GetWarehouseByIdAsync(1);

        Assert.That(result.IsSuccess, Is.True); // Should succeed when warehouse is found
        Assert.That(result.Data.WarehouseId, Is.EqualTo(1)); // WarehouseId should be 1
        Assert.That(result.Data.Name, Is.EqualTo("W1")); // Name should be "W1"
    }

    [Test]
    public async Task GetWarehouseByIdAsync_ReturnsFailure_WhenDomainFails()
    {
        var domainResult = Result<Warehouse>.Failure("not found");
        _domainMock.Setup(d => d.GetWarehouseByIdAsync(1)).ReturnsAsync(domainResult);

        var result = await _service.GetWarehouseByIdAsync(1);

        Assert.That(result.IsSuccess, Is.False); // Should fail when warehouse is not found
        Assert.That(result.ErrorMessage, Is.EqualTo("not found")); // Should return correct error message
    }

    [Test]
    public async Task AddItemToWarehouseAsync_AddsItemAndUpdatesWarehouse()
    {
        var warehouse = new Warehouse(1, "W1", "Loc", 10, 1, new List<Item>());
        var getResult = Result<Warehouse>.Success(warehouse);
        var updateResult = Result<bool>.Success(true);

        _domainMock.Setup(d => d.GetWarehouseByIdAsync(1)).ReturnsAsync(getResult);
        _domainMock.Setup(d => d.UpdateWarehouseAsync(It.IsAny<Warehouse>())).ReturnsAsync(updateResult);

        var result = await _service.AddItemToWarehouseAsync(1, 100, 5);

        Assert.That(result.IsSuccess, Is.True); // Should succeed when item is added
        Assert.That(result.Data, Is.True); // Data should be true indicating update success
        Assert.That(warehouse.Items.Count, Is.EqualTo(1)); // Should have one item after add
        Assert.That(warehouse.Items[0].Sku, Is.EqualTo(100)); // Item Sku should be 100
        Assert.That(warehouse.Items[0].CurrentStock, Is.EqualTo(5)); // Item stock should be 5
    }

    [Test]
    public async Task AddItemToWarehouseAsync_ReturnsFailure_WhenWarehouseNotFound()
    {
        var getResult = Result<Warehouse>.Failure("not found");
        _domainMock.Setup(d => d.GetWarehouseByIdAsync(1)).ReturnsAsync(getResult);

        var result = await _service.AddItemToWarehouseAsync(1, 100, 5);

        Assert.That(result.IsSuccess, Is.False); // Should fail when warehouse is not found
        Assert.That(result.ErrorMessage, Is.EqualTo("not found")); // Should return correct error message
    }

    [Test]
    public async Task RemoveItemFromWarehouseAsync_RemovesItemAndUpdatesWarehouse()
    {
        var warehouse = new Warehouse(1, "W1", "Loc", 10, 1, new List<Item>
        {
            new Item { Sku = 100, CurrentStock = 5 }
        });

        var getResult = Result<Warehouse>.Success(warehouse);
        var updateResult = Result<bool>.Success(true);

        _domainMock.Setup(d => d.GetWarehouseByIdAsync(1)).ReturnsAsync(getResult);
        _domainMock.Setup(d => d.UpdateWarehouseAsync(It.IsAny<Warehouse>())).ReturnsAsync(updateResult);

        var result = await _service.RemoveItemFromWarehouseAsync(1, 100);

        Assert.That(result.IsSuccess, Is.True); // Should succeed when item is removed
        Assert.That(result.Data, Is.True); // Data should be true indicating update success
        Assert.That(warehouse.Items.Count, Is.EqualTo(0)); // Should have no items after removal
    }

    [Test]
    public async Task TransferItemAsync_TransfersItemBetweenWarehouses()
    {
        var sourceWarehouse = new Warehouse(1, "W1", "Loc", 10, 1, new List<Item>
        {
            new Item { Sku = 100, CurrentStock = 5 }
        });
        var targetWarehouse = new Warehouse(2, "W2", "Loc", 10, 1, new List<Item>());
        var getSourceResult = Result<Warehouse>.Success(sourceWarehouse);
        var getTargetResult = Result<Warehouse>.Success(targetWarehouse);
        var updateResult = Result<bool>.Success(true);

        _domainMock.Setup(d => d.GetWarehouseByIdAsync(1)).ReturnsAsync(getSourceResult);
        _domainMock.Setup(d => d.GetWarehouseByIdAsync(2)).ReturnsAsync(getTargetResult);
        _domainMock.Setup(d => d.UpdateWarehouseAsync(It.IsAny<Warehouse>())).ReturnsAsync(updateResult);

        var result = await _service.TransferItemAsync(1, 100, 2, 2);

        Assert.That(result.IsSuccess, Is.True); // Should succeed when transfer is successful
        Assert.That(sourceWarehouse.Items.First(i => i.Sku == 100).CurrentStock, Is.EqualTo(3)); // Source warehouse item stock should decrease by 2
    }
}
