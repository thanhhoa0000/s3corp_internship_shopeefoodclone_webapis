namespace ShopeeFoodClone.WebApi.Payment.Application.Interfaces;

public interface IPaymentService
{
    Task<Response> ProcessPaymentAsync(PaymentRequest request);
    Task<Response> ConfirmPaymentAsync(Guid transactionId);
}
