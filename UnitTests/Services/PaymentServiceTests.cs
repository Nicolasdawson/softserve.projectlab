using Xunit;
using API.Services;
using API.Models;
using API.implementations.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class PaymentServiceTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // DB aislada por test
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public void CreatePayment_ShouldAddPayment()
    {
        var context = GetDbContext();
        var service = new PaymentService(context);
        var payment = new Payment
        {
            StripeSessionId = "session_123",
            Status = "pending",
            Amount = 100,
            Currency = "usd",
            PaymentIntentId = "intent_123"
        };

        var result = service.CreatePayment(payment);

        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(1, context.Payments.Count());
    }

    [Fact]
    public void GetAllPayments_ShouldReturnAll()
    {
        var context = GetDbContext();
        context.Payments.AddRange(
            new Payment { Id = Guid.NewGuid(), StripeSessionId = "1", Amount = 10, Currency = "usd", Status = "ok", CreatedAt = DateTime.UtcNow },
            new Payment { Id = Guid.NewGuid(), StripeSessionId = "2", Amount = 20, Currency = "usd", Status = "ok", CreatedAt = DateTime.UtcNow }
        );
        context.SaveChanges();

        var service = new PaymentService(context);
        var all = service.GetAllPayments();

        Assert.Equal(2, all.Count());
    }

    [Fact]
    public void GetPaymentById_ShouldReturnCorrect()
    {
        var context = GetDbContext();
        var id = Guid.NewGuid();
        context.Payments.Add(new Payment { Id = id, StripeSessionId = "s", Amount = 50, Currency = "usd", Status = "ok", CreatedAt = DateTime.UtcNow });
        context.SaveChanges();

        var service = new PaymentService(context);
        var result = service.GetPaymentById(id);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
    }

    [Fact]
    public void DeletePayment_ShouldRemovePayment()
    {
        var context = GetDbContext();
        var id = Guid.NewGuid();
        context.Payments.Add(new Payment
        {
            Id = id,
            StripeSessionId = "test_session",
            Currency = "usd",
            Status = "ok",
            Amount = 0,
            CreatedAt = DateTime.UtcNow
        });

        context.SaveChanges();

        var service = new PaymentService(context);
        var success = service.DeletePayment(id);

        Assert.True(success);
        Assert.Null(service.GetPaymentById(id));
    }

    [Fact]
    public void UpdatePayment_ShouldModifyData()
    {
        var context = GetDbContext();
        var id = Guid.NewGuid();
        context.Payments.Add(new Payment
        {
            Id = id,
            StripeSessionId = "old",
            Amount = 100,
            Currency = "usd",
            Status = "pending",
            CreatedAt = DateTime.UtcNow
        });
        context.SaveChanges();

        var service = new PaymentService(context);
        var updated = new Payment
        {
            StripeSessionId = "new",
            Amount = 200,
            Currency = "eur",
            Status = "success"
        };

        var result = service.UpdatePayment(id, updated);

        Assert.True(result);
        var actual = service.GetPaymentById(id);
        Assert.Equal("new", actual.StripeSessionId);
        Assert.Equal("eur", actual.Currency);
    }

    [Fact]
    public void SavePaymentStatus_ShouldUpdateIfExists()
    {
        var context = GetDbContext();
        context.Payments.Add(new Payment
        {
            Id = Guid.NewGuid(),
            PaymentIntentId = "intent_1",
            Currency = "usd",
            StripeSessionId = "s_123",
            Status = "old",
            Amount = 0,
            CreatedAt = DateTime.UtcNow
        });

        context.SaveChanges();

        var service = new PaymentService(context);
        service.SavePaymentStatus("intent_1", "updated");

        var result = context.Payments.FirstOrDefault(p => p.PaymentIntentId == "intent_1");
        Assert.Equal("updated", result.Status);
    }

    [Fact]
    public void SavePaymentStatus_ShouldCreateIfNotExists()
    {
        var context = GetDbContext();
        var service = new PaymentService(context);

        service.SavePaymentStatus("intent_missing", "created");

        var result = context.Payments.FirstOrDefault(p => p.PaymentIntentId == "intent_missing");
        Assert.NotNull(result);
        Assert.Equal("created", result.Status);
    }

    [Fact]
    public void SavePaymentStatusBySessionId_ShouldUpdateIfExists()
    {
        var context = GetDbContext();
       context.Payments.Add(new Payment
        {
            Id = Guid.NewGuid(),
            StripeSessionId = "session_1",
            Currency = "usd",
            Status = "old",
            Amount = 0,
            CreatedAt = DateTime.UtcNow
        });

        context.SaveChanges();

        var service = new PaymentService(context);
        service.SavePaymentStatusBySessionId("session_1", "paid");

        var result = context.Payments.First(p => p.StripeSessionId == "session_1");
        Assert.Equal("paid", result.Status);
    }
}
