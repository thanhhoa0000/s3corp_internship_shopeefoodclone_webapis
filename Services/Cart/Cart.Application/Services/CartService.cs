namespace ShopeeFoodClone.WebApi.Cart.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartHeaderRepository;
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IRabbitMqPublisher _publisher;
    private readonly IRabbitMqSubscriber _subscriber;
    private IEnumerable<ProductDto> _cachedProducts = new List<ProductDto>();
    private readonly IMapper _mapper;

    public CartService(
        ICartRepository cartHeaderRepository, 
        ICartItemRepository cartItemRepository,
        IRabbitMqPublisher publisher,
        IRabbitMqSubscriber subscriber,
        IMapper mapper)
    {
        _cartHeaderRepository = cartHeaderRepository;
        _cartItemRepository = cartItemRepository;
        _publisher = publisher;
        _subscriber = subscriber;
        _mapper = mapper;
        
        _subscriber.Subscribe<ProductInfoResponse>("product_info_response", response =>
        {
            _cachedProducts = response.Products!;
        });
    }

    /// <summary>
    /// Get the cart of current user
    /// </summary>
    /// <param name="customerId">The user ID</param>
    /// <returns>The user's cart</returns>
    public async Task<Response> GetCartAsync(Guid customerId)
    {
        var response = new Response();

        try
        {
            var cart = new CartDto()
            {
                CartHeader = _mapper
                    .Map<CartHeaderDto>(
                        await _cartHeaderRepository
                            .GetAsync(c => c.CustomerId == customerId, tracked: false))
            };
            
            if (cart.CartHeader is null || cart.CartHeader.Id == Guid.Empty)
            {
                response.IsSuccessful = false;
                response.Message = "The cart is empty.";
            
                return response;
            }
            
            cart.CartItems = _mapper
                .Map<ICollection<CartItemDto>>(
                    await _cartItemRepository.GetAllAsync(i => i.CartHeaderId == cart.CartHeader.Id, tracked: false));

            var missingProductIds = cart.CartItems
                .Where(item => !_cachedProducts.Any(p => p.Id == item.ProductId))
                .Select(item => item.ProductId)
                .ToList();

            if (missingProductIds.Any())
            {
                LoadProductInfoForCart(missingProductIds);
                
                int maxRetries = 10;
                for (int attempt = 0; attempt < maxRetries; attempt++)
                {
                    missingProductIds = cart.CartItems
                        .Where(item => !_cachedProducts.Any(p => p.Id == item.ProductId))
                        .Select(item => item.ProductId)
                        .ToList();

                    if (!missingProductIds.Any())
                        break;

                    LoadProductInfoForCart(missingProductIds);
                    Console.WriteLine($"Retrying fetch for missing products... Attempt {attempt + 1}/{maxRetries}");
                    await Task.Delay(1000);
                }

            }
            
            foreach (var item in cart.CartItems)
            {
                var product = _cachedProducts.FirstOrDefault(p => p.Id == item.ProductId);

                if (product is null)
                {
                    Console.WriteLine($"Warning: ProductId {item.ProductId} is still missing.");
                    continue;
                }

                item.ProductId = product.Id;
                cart.CartHeader.TotalPrice += (item.Quantity * product.Price);
            }


            
            response.Body = cart;
            
            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            
            return response;
        }
    }

    /// <summary>
    /// Add an item to cart
    /// </summary>
    /// <param name="cartDto">Info of item to add</param>
    /// <returns>The updated cart</returns>
    public async Task<Response> AddToCartAsync(CartDto cartDto)
    {
        var response = new Response();

        try
        {
            var cartItemDto = cartDto.CartItems.First();
            CartHeader? cartHeaderFromDb = await _cartHeaderRepository.GetAsync(
                c => c.CustomerId == cartDto.CartHeader!.CustomerId);

            // Fix: Create cart header and ensure it's not null
            if (cartHeaderFromDb is null)
            {
                cartHeaderFromDb = _mapper.Map<CartHeader>(cartDto.CartHeader);
                await _cartHeaderRepository.CreateAsync(cartHeaderFromDb);
            }

            // Check if item already exists in the cart
            var cartItemFromDb = await _cartItemRepository.GetAsync(
                i => i.ProductId == cartItemDto.ProductId && i.CartHeaderId == cartHeaderFromDb.Id);

            if (cartItemFromDb is null)
            {
                var newCartItem = _mapper.Map<CartItem>(cartItemDto);
                newCartItem.CartHeaderId = cartHeaderFromDb.Id;
                newCartItem.Id = Guid.NewGuid();
                await _cartItemRepository.CreateAsync(newCartItem);
            }
            else
            {
                cartItemFromDb.Quantity += cartItemDto.Quantity;
                await _cartItemRepository.UpdateAsync(cartItemFromDb);
            }

            response.Body = cartDto;
            
            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            
            return response;
        }
    }

    public async Task<Response> RemoveFromCartAsync(Guid cartItemId)
    {
        var response = new Response();

        try
        {
            var cartItem = await _cartItemRepository.GetAsync(i => i.Id == cartItemId, tracked: false);

            if (cartItem is null)
            {
                response.IsSuccessful = false;
                response.Message = "Cart item not found.";
                
                return response;
            }
            
            var cartHeader = await _cartHeaderRepository.GetAsync(c => c.Id == cartItem.CartHeaderId);
        
            if (cartHeader is null)
            {
                response.IsSuccessful = false;
                response.Message = "Cart header not found.";
                
                return response;
            }

            var cartItemsQuantity = (await _cartItemRepository
                    .GetAllAsync(i => i.CartHeaderId == cartHeader.Id))
                .Count();

            await _cartItemRepository.RemoveAsync(cartItem);

            if (cartItemsQuantity == 1)
            {
                await _cartHeaderRepository.RemoveAsync(cartHeader);
            }

            response.Body = true;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }

        return response;
    }

    
    /// <summary>
    /// Load product information from EventBus
    /// </summary>
    /// <param name="productIds">Needed products' ID for cart</param>
    public void LoadProductInfoForCart(IEnumerable<Guid> productIds)
    {
        _publisher.Publish(new ProductInfoRequest { ProductIds = productIds }, "product_info_request");
    }
}