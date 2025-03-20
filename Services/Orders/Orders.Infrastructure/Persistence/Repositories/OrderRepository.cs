namespace ShopeeFoodClone.WebApi.Orders.Infrastructure.Persistence.Repositories;

public class OrderRepository(OrderContext context) : Repository<Order, OrderContext>(context), IOrderRepository;
