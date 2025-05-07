namespace ShopeeFoodClone.WebApi.Payment.Application.Models.Requests;

public sealed class PaymentRequest
{
    public Guid CustomerId { get; set; }
    public Guid OrderId { get; set; }
    public string Method { get; set; } = nameof(PaymentMethod.Card).ToLower();
    public string Currency { get; set; } = nameof(PaymentCurrency.VND).ToLower();
    public decimal Amount { get; set; }
}
