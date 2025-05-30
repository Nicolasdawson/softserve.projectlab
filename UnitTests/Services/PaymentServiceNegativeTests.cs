using Xunit;
using API.Services;
using API.Models;
using API.implementations.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class PaymentServiceNegativeTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public void GetPaymentById_ShouldReturnNull_WhenIdDoesNotExist()
    {
        // Arrange
        var context = GetDbContext();
        var service = new PaymentService(context);

        // Act
        var result = service.GetPaymentById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeletePayment_ShouldReturnFalse_WhenIdDoesNotExist()
    {
        // Arrange
        var context = GetDbContext();
        var service = new PaymentService(context);

        // Act
        var result = service.DeletePayment(Guid.NewGuid());

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void UpdatePayment_ShouldReturnFalse_WhenPaymentDoesNotExist()
    {
        // Arrange
        var context = GetDbContext();
        var service = new PaymentService(context);
        var updated = new Payment
        {
            StripeSessionId = "no-session",
            Status = "none",
            Amount = 0,
            Currency = "usd"
        };

        // Act
        var result = service.UpdatePayment(Guid.NewGuid(), updated);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void SavePaymentStatus_ShouldCreateNew_WhenPaymentIntentIdNotFound()
    {
        // Arrange
        var context = GetDbContext();
        var service = new PaymentService(context);
        var intentId = "missing_intent";

        // Act
        service.SavePaymentStatus(intentId, "failed");

        // Assert
        var saved = context.Payments.FirstOrDefault(p => p.PaymentIntentId == intentId);
        Assert.NotNull(saved);
        Assert.Equal("failed", saved.Status);
    }

    [Fact]
    public void SavePaymentStatusBySessionId_ShouldDoNothing_WhenSessionIdNotFound()
    {
        // Arrange
        var context = GetDbContext();
        var service = new PaymentService(context);

        // Act
        service.SavePaymentStatusBySessionId("nonexistent_session", "paid");

        // Assert
        Assert.Empty(context.Payments); // No se debería haber agregado nada
    }
}
