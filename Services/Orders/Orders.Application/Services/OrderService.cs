using Microsoft.IdentityModel.Tokens;

namespace ShopeeFoodClone.WebApi.Orders.Application.Services;

// TODO: GetOrdersByStore
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderHeaderRepository;
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IOrderRepository orderHeaderRepository,
        IOrderDetailRepository orderDetailRepository,
        IMapper mapper)
    {
        _orderHeaderRepository = orderHeaderRepository;
        _orderDetailRepository = orderDetailRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all orders of 1 individual customer
    /// </summary>
    /// <param name="request">The Customer ID, order status and sort type</param>
    /// <returns>The orders list</returns>
    public async Task<Response> GetOrdersByCustomerIdAsync(GetOrdersRequest request)
    {
        var response = new Response();

        try
        {
            var customerId = request.CustomerId;
            var status = request.Status;
            var sortingOrder = request.SortingOrder;
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            Expression<Func<Order, bool>> filter = x =>
                (x.CustomerId == customerId) &&
                (status.ToString().IsNullOrEmpty() || x.OrderStatus == status);

            Func<IQueryable<Order>, IQueryable<Order>>? include = query =>
                query.Include(o => o.OrderDetails);
            
            Func<IQueryable<Order>, IOrderedQueryable<Order>>? orderBy = query =>
                query.OrderBy(o => o.OrderDate);
            
            var orders = 
                await _orderHeaderRepository.GetAllAsync(
                    filter: filter,
                    include: include,
                    orderBy: orderBy,
                    orderByDescending: sortingOrder == SortingOrder.Descending,
                    tracked: false,
                    pageSize: pageSize,
                    pageNumber: pageNumber);
            
            response.Body = _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }

    /// <summary>
    /// Get order by ID
    /// </summary>
    /// <param name="orderId">The order's ID</param>
    /// <returns>The order to get</returns>
    public async Task<Response> GetOrderByIdAsync(Guid orderId)
    {
        var response = new Response();

        try
        {
            var order = await _orderHeaderRepository.GetAsync(
                filter: o => o.Id == orderId,
                include: q => q.Include(o => o.OrderDetails),
                tracked: false);
            
            response.Body = _mapper.Map<OrderDto>(order);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }

    /// <summary>
    /// Create order when processing from cart
    /// </summary>
    /// <param name="cartDto">The cart to process</param>
    /// <returns>The created order</returns>
    public async Task<Response> CreateOrderAsync(CartDto cartDto)
    {
        var response = new Response();

        try
        {
            var orderHeaderDto = _mapper.Map<OrderDto>(cartDto.CartHeader);
            orderHeaderDto.OrderDate = DateTime.UtcNow;
            orderHeaderDto.OrderStatus = OrderStatus.Pending;
            orderHeaderDto.OrderDetails = _mapper.Map<ICollection<OrderDetailDto>>(cartDto.CartItems);
            await _orderHeaderRepository.CreateAsync(_mapper.Map<Order>(orderHeaderDto));
            
            response.Body = orderHeaderDto;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }

    /// <summary>
    /// Change the order state to "deleted" (soft delete)
    /// </summary>
    /// <param name="orderId">The order's ID to "delete"</param>
    /// <returns>The "deleted" order</returns>
    public async Task<Response> SoftDeleteOrderAsync(Guid orderId)
    {
        var response = new Response();
        
        try
        {
            var order = await _orderHeaderRepository.GetAsync(
                filter: o => o.Id == orderId,
                tracked: false);

            var orderToSoftDelete = _mapper.Map<OrderDto>(order);
            orderToSoftDelete.OrderStatus = OrderStatus.DeletedByCustomer;
            await _orderHeaderRepository.UpdateAsync(_mapper.Map<Order>(orderToSoftDelete));
            
            response.Body = orderToSoftDelete;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }
}