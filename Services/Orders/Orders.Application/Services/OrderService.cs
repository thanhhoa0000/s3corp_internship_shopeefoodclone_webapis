using Microsoft.IdentityModel.Tokens;

namespace ShopeeFoodClone.WebApi.Orders.Application.Services;

// TODO: GetOrdersByStore
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderHeaderRepository;
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public OrderService(
        IOrderRepository orderHeaderRepository,
        IOrderDetailRepository orderDetailRepository,
        ICartService cartService,
        IProductService productService,
        IMapper mapper)
    {
        _orderHeaderRepository = orderHeaderRepository;
        _orderDetailRepository = orderDetailRepository;
        _cartService = cartService;
        _productService = productService;
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


            var ordersToReturn = _mapper.Map<IEnumerable<OrderDto>>(orders);
            
            foreach (var order in ordersToReturn)
            {
                foreach (var detail in order.OrderDetails)
                {
                    var responseFromProductApi = await _productService.GetProductAsync(detail.ProductId);
                
                    var productDto = JsonSerializer.Deserialize<ProductDto>(
                        Convert.ToString(responseFromProductApi!.Body)!,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                    detail.Product = productDto;
                }
            }
            
            response.Body = ordersToReturn;
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

            var orderToReturn = _mapper.Map<OrderDto>(order);
            
            foreach (var detail in orderToReturn.OrderDetails)
            {
                var responseFromProductApi = await _productService.GetProductAsync(detail.ProductId);
                
                var productDto = JsonSerializer.Deserialize<ProductDto>(
                    Convert.ToString(responseFromProductApi!.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                detail.Product = productDto;
            }
            
            response.Body = orderToReturn;
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
    /// <param name="request">The cart to process</param>
    /// <returns>The created order</returns>
    public async Task<Response> CreateOrderAsync(CreateOrderRequest request)
    {
        var response = new Response();

        try
        {
            var cartHeader = request.Cart!.CartHeader;
            var cartItems = request.Cart!.CartItems;
            var address = request.Address!;
            var customerName = request.CustomerName!;
            var phoneNumber = request.PhoneNumber!;
            
            var orderHeaderDto = _mapper.Map<OrderDto>(cartHeader);
            orderHeaderDto.CustomerName = customerName;
            orderHeaderDto.PhoneNumber = phoneNumber;
            orderHeaderDto.StoreId = cartHeader!.StoreId;
            orderHeaderDto.OrderDate = DateTime.UtcNow;
            orderHeaderDto.OrderStatus = OrderStatus.Pending;
            orderHeaderDto.OrderDetails = _mapper.Map<ICollection<OrderDetailDto>>(cartItems);
            orderHeaderDto.Address = address;
            
            await _orderHeaderRepository.CreateAsync(_mapper.Map<Order>(orderHeaderDto));

            var emptyCartSuccess = await _cartService.EmptyCart(orderHeaderDto.CustomerId);

            if (!emptyCartSuccess)
            {
                response.IsSuccessful = false;
                response.Message = "Error occurred when creating a new order!";
            }
            
            response.Message = "Order created successfully!";
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