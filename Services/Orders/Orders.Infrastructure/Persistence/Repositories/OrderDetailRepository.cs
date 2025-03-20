namespace ShopeeFoodClone.WebApi.Orders.Infrastructure.Persistence.Repositories;

public class OrderDetailRepository(OrderContext context)
    : Repository<OrderDetail, OrderContext>(context), IOrderDetailRepository;
