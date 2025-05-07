using Stripe;

namespace ShopeeFoodClone.WebApi.Payment.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public PaymentService(
        IPaymentRepository paymentRepository,
        IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Process payment from order
    /// </summary>
    /// <param name="request">The payment request info</param>
    /// <returns>The result of the payment</returns>
    public async Task<Response> ProcessPaymentAsync(PaymentRequest request)
    {
        var response = new Response();

        try
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)request.Amount,
                Currency = request.Currency,
                PaymentMethodTypes = new List<string> { request.Method }
            };
            
            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            var transaction = new TransactionDto
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                OrderId = request.OrderId,
                PaymentId = paymentIntent.Id,
                Amount = request.Amount,
                Method = request.Method,
                Currency = request.Currency,
                IsSuccessful = false
            };
            
            await _paymentRepository.CreateAsync(_mapper.Map<Transaction>(transaction));

            response.Message = $"Payment request from order {request.OrderId} sent";
            response.Body = paymentIntent.ClientSecret;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }

    /// <summary>
    /// Confirm a payment from order successful
    /// </summary>
    /// <param name="transactionId">The transaction to confirm</param>
    /// <returns>Confirmed transaction</returns>
    // TODO: Not completed
    public async Task<Response> ConfirmPaymentAsync(Guid transactionId)
    {
        var response = new Response();
        
        try
        {
            var transaction = await _paymentRepository.GetAsync(t => t.Id == transactionId, tracked: false);

            if (transaction is null)
            {
                response.IsSuccessful = false;
                response.Message = $"Transaction {transactionId} not found";
                
                return response;
            }

            var options = new PaymentIntentConfirmOptions
            {
                PaymentMethodTypes = new List<string> { transaction.Method }
            };
            
            response.Message = $"Payment request from order {transaction.OrderId} confirmed";
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }
}