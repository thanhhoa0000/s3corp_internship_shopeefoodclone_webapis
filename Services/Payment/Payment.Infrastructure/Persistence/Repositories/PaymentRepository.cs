namespace ShopeeFoodClone.WebApi.Payment.Infrastructure.Persistence.Repositories;

public class PaymentRepository(PaymentContext context) : Repository<Transaction, PaymentContext>(context), IPaymentRepository;
