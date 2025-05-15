using API.Implementations.Domain;
using API.Services.Logistics;
using Moq;
using API.Data.Repositories.LogisticsRepositories.Interfaces;
using API.Data.Entities;

[TestFixture]
public class WarehouseServiceTests
{
    private Mock<IWarehouseRepository> _repoMock;
    private WarehouseDomain _domain;
    private WarehouseService _service;

    [SetUp]
    public void SetUp()
    {
        _repoMock = new Mock<IWarehouseRepository>();
        _domain = new WarehouseDomain(_repoMock.Object);
        _service = new WarehouseService(_domain);
    }

    [Test]
    public async Task GetAllWarehousesAsync_ReturnsMappedDtos_WhenDomainSuccess()
    {
        var warehouseEntity = new WarehouseEntity
        {
            WarehouseId = 1,
            WarehouseLocation = "Loc",
            WarehouseCapacity = 10,
            BranchId = 1,
            WarehouseItemEntities = new List<WarehouseItemEntity>()
        };
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<WarehouseEntity> { warehouseEntity });

        var result = await _service.GetAllWarehousesAsync();

        Assert.That(result.Count, Is.EqualTo(1)); // Should return one warehouse DTO
        Assert.That(result[0].WarehouseId, Is.EqualTo(1)); // WarehouseId should be 1
        Assert.That(result[0].Name, Is.Not.Null); // Name should not be null (mapping logic)
    }

    [Test]
    public async Task GetAllWarehousesAsync_ReturnsEmptyList_WhenDomainFails()
    {
        _repoMock.Setup(r => r.GetAllAsync()).ThrowsAsync(new System.Exception("error"));

        var result = await _service.GetAllWarehousesAsync();

        Assert.That(result, Is.Not.Null); // Should not be null even if domain fails
        Assert.That(result.Count, Is.EqualTo(0)); // Should return an empty list
    }

    [Test]
    public async Task GetWarehouseByIdAsync_ReturnsMappedDto_WhenDomainSuccess()
    {
        var warehouseEntity = new WarehouseEntity
        {
            WarehouseId = 1,
            WarehouseLocation = "Loc",
            WarehouseCapacity = 10,
            BranchId = 1,
            WarehouseItemEntities = new List<WarehouseItemEntity>()
        };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(warehouseEntity);

        var result = await _service.GetWarehouseByIdAsync(1);

        Assert.That(result.IsSuccess, Is.True); // Should succeed when warehouse is found
        Assert.That(result.Data.WarehouseId, Is.EqualTo(1)); // WarehouseId should be 1
        Assert.That(result.Data.Name, Is.Not.Null); // Name should not be null (mapping logic)
    }

    [Test]
    public async Task GetWarehouseByIdAsync_ReturnsFailure_WhenDomainFails()
    {
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((WarehouseEntity)null);

        var result = await _service.GetWarehouseByIdAsync(1);

        Assert.That(result.IsSuccess, Is.False); // Should fail when warehouse is not found
        Assert.That(result.ErrorMessage, Is.EqualTo("Warehouse not found.")); // Should return correct error message
    }

    [Test]
    public async Task AddItemToWarehouseAsync_AddsItemAndUpdatesWarehouse()
    {
        var warehouseEntity = new WarehouseEntity
        {
            WarehouseId = 1,
            WarehouseLocation = "Loc",
            WarehouseCapacity = 10,
            BranchId = 1,
            WarehouseItemEntities = new List<WarehouseItemEntity>()
        };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(warehouseEntity);
        _repoMock.Setup(r => r.UpdateAsync(It.IsAny<WarehouseEntity>())).Returns(Task.CompletedTask);

        var result = await _service.AddItemToWarehouseAsync(1, 100, 5);

        Assert.That(result.IsSuccess, Is.True); // Should succeed when item is added
        Assert.That(result.Data, Is.True); // Data should be true indicating update success
    }

    [Test]
    public async Task AddItemToWarehouseAsync_ReturnsFailure_WhenWarehouseNotFound()
    {
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((WarehouseEntity)null);

        var result = await _service.AddItemToWarehouseAsync(1, 100, 5);

        Assert.That(result.IsSuccess, Is.False); // Should fail when warehouse is not found
        Assert.That(result.ErrorMessage, Is.EqualTo("Warehouse not found.")); // Should return correct error message
    }

    [Test]
    public async Task RemoveItemFromWarehouseAsync_RemovesItemAndUpdatesWarehouse()
    {
        var warehouseEntity = new WarehouseEntity
        {
            WarehouseId = 1,
            WarehouseLocation = "Loc",
            WarehouseCapacity = 10,
            BranchId = 1,
            WarehouseItemEntities = new List<WarehouseItemEntity>
            {
                new WarehouseItemEntity { Sku = 100, ItemQuantity = 5 }
            }
        };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(warehouseEntity);
        _repoMock.Setup(r => r.UpdateAsync(It.IsAny<WarehouseEntity>())).Returns(Task.CompletedTask);

        var result = await _service.RemoveItemFromWarehouseAsync(1, 100);

        Assert.That(result.IsSuccess, Is.True); // Should succeed when item is removed
        Assert.That(result.Data, Is.True); // Data should be true indicating update success
    }

    [Test]
    public async Task TransferItemAsync_TransfersItemBetweenWarehouses()
    {
        var sourceEntity = new WarehouseEntity
        {
            WarehouseId = 1,
            WarehouseLocation = "Loc",
            WarehouseCapacity = 10,
            BranchId = 1,
            WarehouseItemEntities = new List<WarehouseItemEntity>
            {
                new WarehouseItemEntity { Sku = 100, ItemQuantity = 5 }
            }
        };
        var targetEntity = new WarehouseEntity
        {
            WarehouseId = 2,
            WarehouseLocation = "Loc2",
            WarehouseCapacity = 10,
            BranchId = 1,
            WarehouseItemEntities = new List<WarehouseItemEntity>()
        };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(sourceEntity);
        _repoMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(targetEntity);
        _repoMock.Setup(r => r.UpdateAsync(It.IsAny<WarehouseEntity>())).Returns(Task.CompletedTask);

        var result = await _service.TransferItemAsync(1, 100, 2, 2);

        Assert.That(result.IsSuccess, Is.True); // Should succeed when transfer is successful
        // You can add more asserts here to check the updated quantities if needed
    }
}

