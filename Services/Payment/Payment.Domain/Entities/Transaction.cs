namespace ShopeeFoodClone.WebApi.Payment.Domain.Entities;

public class Transaction : IEntity
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid OrderId { get; set; }
    public string PaymentId { get; set; } = String.Empty;
    public decimal Amount { get; set; }
    public string Method { get; set; } = nameof(PaymentMethod.Card).ToLower();
    public string Currency { get; set; } = nameof(PaymentCurrency.VND).ToLower();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
    public bool IsSuccessful { get; set; } = false;
}
