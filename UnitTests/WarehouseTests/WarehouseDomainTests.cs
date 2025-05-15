using API.Implementations.Domain;
using API.Data.Entities;
using API.Data.Repositories.LogisticsRepositories.Interfaces;
using API.Models.Logistics.Warehouses;
using softserve.projectlabs.Shared.DTOs;
using softserve.projectlabs.Shared.Utilities;
using Moq;
using API.Models.IntAdmin;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

[TestFixture]
public class WarehouseDomainTests
{
    private Mock<IWarehouseRepository> _repoMock;
    private WarehouseDomain _domain;

    [SetUp]
    public void SetUp()
    {
        _repoMock = new Mock<IWarehouseRepository>();
        _domain = new WarehouseDomain(_repoMock.Object);
    }

    [Test]
    public async Task GetWarehouseByIdAsync_ReturnsWarehouse_WhenFound()
    {
        var entity = new WarehouseEntity { WarehouseId = 1, WarehouseLocation = "Loc", WarehouseCapacity = 10, BranchId = 1, WarehouseItemEntities = new List<WarehouseItemEntity>() };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

        var result = await _domain.GetWarehouseByIdAsync(1);

        Assert.That(result.IsSuccess, Is.True); // Should succeed when warehouse is found
        Assert.That(result.Data, Is.Not.Null); // Data should not be null when found
        Assert.That(result.Data.WarehouseId, Is.EqualTo(1)); // Returned warehouse should have ID 1
    }

    [Test]
    public async Task GetWarehouseByIdAsync_ReturnsFailure_WhenNotFound()
    {
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((WarehouseEntity)null);

        var result = await _domain.GetWarehouseByIdAsync(1);

        Assert.That(result.IsSuccess, Is.False); // Should fail when warehouse is not found
        Assert.That(result.ErrorMessage, Is.EqualTo("Warehouse not found.")); // Should return correct error message
    }

    [Test]
    public async Task GetAllWarehousesAsync_ReturnsWarehouses()
    {
        var entities = new List<WarehouseEntity>
        {
            new WarehouseEntity { WarehouseId = 1, WarehouseLocation = "Loc", WarehouseCapacity = 10, BranchId = 1, WarehouseItemEntities = new List<WarehouseItemEntity>() }
        };
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);

        var result = await _domain.GetAllWarehousesAsync();

        Assert.That(result.IsSuccess, Is.True); // Should succeed when warehouses are found
        Assert.That(result.Data.Count, Is.EqualTo(1)); // Should return one warehouse
    }

    [Test]
    public async Task CreateWarehouseAsync_ReturnsCreatedWarehouse()
    {
        var dto = new WarehouseDto { Location = "Loc", Capacity = 10, BranchId = 1 };
        _repoMock.Setup(r => r.AddAsync(It.IsAny<WarehouseEntity>())).Returns(Task.CompletedTask);

        var result = await _domain.CreateWarehouseAsync(dto);

        Assert.That(result.IsSuccess, Is.True); // Should succeed when warehouse is created
        Assert.That(result.Data.Location, Is.EqualTo("Loc")); // Created warehouse should have correct location
    }

    [Test]
    public async Task UpdateWarehouseAsync_ReturnsSuccess()
    {
        var warehouse = new Warehouse(1, "Name", "Loc", 10, 1, new List<Item>());
        _repoMock.Setup(r => r.UpdateAsync(It.IsAny<WarehouseEntity>())).Returns(Task.CompletedTask);

        var result = await _domain.UpdateWarehouseAsync(warehouse);

        Assert.That(result.IsSuccess, Is.True); // Should succeed when warehouse is updated
        Assert.That(result.Data, Is.True); // Data should be true indicating update success
    }

    [Test]
    public async Task SoftDeleteWarehouseAsync_ReturnsSuccess_WhenFound()
    {
        var entity = new WarehouseEntity { WarehouseId = 1, IsDeleted = false, WarehouseItemEntities = new List<WarehouseItemEntity>() };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
        _repoMock.Setup(r => r.UpdateAsync(entity)).Returns(Task.CompletedTask);

        var result = await _domain.SoftDeleteWarehouseAsync(1);

        Assert.That(result.IsSuccess, Is.True); // Should succeed when warehouse is found and soft deleted
        Assert.That(result.Data, Is.True); // Data should be true indicating soft delete success
        Assert.That(entity.IsDeleted, Is.True); // Entity should be marked as deleted
    }

    [Test]
    public async Task SoftDeleteWarehouseAsync_ReturnsFailure_WhenNotFound()
    {
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((WarehouseEntity)null);

        var result = await _domain.SoftDeleteWarehouseAsync(1);

        Assert.That(result.IsSuccess, Is.False); // Should fail when warehouse is not found
        Assert.That(result.ErrorMessage, Is.EqualTo("Warehouse not found.")); // Should return correct error message
    }

    [Test]
    public async Task UndeleteWarehouseAsync_ReturnsSuccess_WhenFound()
    {
        var entity = new WarehouseEntity { WarehouseId = 1, IsDeleted = true, WarehouseItemEntities = new List<WarehouseItemEntity>() };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
        _repoMock.Setup(r => r.UpdateAsync(entity)).Returns(Task.CompletedTask);

        var result = await _domain.UndeleteWarehouseAsync(1);

        Assert.That(result.IsSuccess, Is.True); // Should succeed when warehouse is found and undeleted
        Assert.That(result.Data, Is.True); // Data should be true indicating undelete success
        Assert.That(entity.IsDeleted, Is.False); // Entity should be marked as not deleted
    }

    [Test]
    public async Task GetLowStockItemsAsync_ReturnsLowStockItems()
    {
        // Arrange: Set up a warehouse entity with two items, one below threshold
        var entity = new WarehouseEntity
        {
            WarehouseId = 1,
            WarehouseItemEntities = new List<WarehouseItemEntity>
            {
                new WarehouseItemEntity { Sku = 1, ItemQuantity = 2 },
                new WarehouseItemEntity { Sku = 2, ItemQuantity = 10 }
            }
        };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

        // Act
        var result = await _domain.GetLowStockItemsAsync(1, 5);

        // Assert
        Assert.That(result.IsSuccess, Is.True); // Should succeed when low stock items are found
        Assert.That(result.Data.Count, Is.EqualTo(1)); // Only one item should be low stock
        Assert.That(result.Data[0].Sku, Is.EqualTo(1)); // The low stock item should have Sku 1
    }

    [Test]
    public async Task GetTotalInventoryValueAsync_ReturnsSum()
    {
        // Arrange: Set up a warehouse entity with two items (no ItemPrice)
        var entity = new WarehouseEntity
        {
            WarehouseId = 1,
            WarehouseItemEntities = new List<WarehouseItemEntity>
            {
                new WarehouseItemEntity { Sku = 1, ItemQuantity = 2 },
                new WarehouseItemEntity { Sku = 2, ItemQuantity = 3 }
            }
        };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

        // Act
        var result = await _domain.GetTotalInventoryValueAsync(1);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Data, Is.EqualTo(5)); // Should return the sum of quantities (2 + 3)
    }

    [Test]
    public async Task GenerateInventoryReportAsync_ReturnsJson()
    {
        // Arrange: Set up a warehouse entity with one item
        var entity = new WarehouseEntity
        {
            WarehouseId = 1,
            WarehouseItemEntities = new List<WarehouseItemEntity>
            {
                new WarehouseItemEntity { Sku = 1, ItemQuantity = 2 }
            }
        };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

        // Act
        var result = await _domain.GenerateInventoryReportAsync(1);

        // Assert
        Assert.That(result.IsSuccess, Is.True); // Should succeed when report is generated
        StringAssert.Contains("\"Sku\":1", result.Data); // JSON should contain Sku 1
    }

    [Test]
    public async Task ReserveStockForOrderAsync_Success()
    {
        // Arrange: Set up a warehouse entity with enough stock
        var entity = new WarehouseEntity
        {
            WarehouseId = 1,
            WarehouseItemEntities = new List<WarehouseItemEntity>
            {
                new WarehouseItemEntity { Sku = 1, ItemQuantity = 10 }
            }
        };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
        _repoMock.Setup(r => r.UpdateAsync(It.IsAny<WarehouseEntity>())).Returns(Task.CompletedTask);

        // Act
        var result = await _domain.ReserveStockForOrderAsync(1, 1, 5);

        // Assert
        Assert.That(result.IsSuccess, Is.True); // Should succeed when stock is reserved
        Assert.That(entity.WarehouseItemEntities.First(i => i.Sku == 1).ItemQuantity, Is.EqualTo(5)); // Stock should be reduced by 5
    }

    [Test]
    public async Task ReserveStockForOrderAsync_Fails_WhenInsufficientStock()
    {
        // Arrange: Set up a warehouse entity with insufficient stock
        var entity = new WarehouseEntity
        {
            WarehouseId = 1,
            WarehouseItemEntities = new List<WarehouseItemEntity>
            {
                new WarehouseItemEntity { Sku = 1, ItemQuantity = 2 }
            }
        };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

        // Act
        var result = await _domain.ReserveStockForOrderAsync(1, 1, 5);

        // Assert
        Assert.That(result.IsSuccess, Is.False); // Should fail when not enough stock
        Assert.That(result.ErrorMessage, Is.EqualTo("Insufficient stock for reservation.")); // Should return correct error message
    }
}
