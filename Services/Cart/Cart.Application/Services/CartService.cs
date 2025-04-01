﻿namespace ShopeeFoodClone.WebApi.Cart.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartHeaderRepository;
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public CartService(
        ICartRepository cartHeaderRepository, 
        ICartItemRepository cartItemRepository,
        IProductService productService,
        IMapper mapper)
    {
        _cartHeaderRepository = cartHeaderRepository;
        _cartItemRepository = cartItemRepository;
        _productService = productService;
        _mapper = mapper;
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
            
            foreach (var item in cart.CartItems)
            {
                var responseFromProductApi = await _productService.GetProductAsync(item.ProductId);
                
                var productDto = JsonSerializer.Deserialize<ProductDto>(
                    Convert.ToString(responseFromProductApi!.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                item.Product = productDto;
                cart.CartHeader.TotalPrice += (item.Quantity * item.Product!.Price);
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
    /// <param name="request">Info of item to add</param>
    /// <returns>The updated cart</returns>
    public async Task<Response> AddToCartAsync(AddToCartRequest request)
    {
        var response = new Response();

        try
        {
            var cartHeaderFromDb = await _cartHeaderRepository.GetAsync(
                c => c.CustomerId == request.CustomerId);

            // Fix: Create cart header and ensure it's not null
            if (cartHeaderFromDb is null)
            {
                cartHeaderFromDb = new CartHeader
                {
                    CustomerId = request.CustomerId,
                };
                
                await _cartHeaderRepository.CreateAsync(cartHeaderFromDb);
            }

            // Check if item already exists in the cart
            var cartItemFromDb = await _cartItemRepository.GetAsync(
                i => i.ProductId == request.ProductId && i.CartHeaderId == cartHeaderFromDb.Id);

            if (cartItemFromDb is null)
            {
                var responseFromProductApi = await _productService.GetProductAsync(request.ProductId);
                
                var productDto = JsonSerializer.Deserialize<ProductDto>(
                    Convert.ToString(responseFromProductApi!.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (productDto!.StoreId != cartHeaderFromDb.StoreId)
                {
                    var oldCartItems =
                        await _cartItemRepository.GetAllAsync(i => i.CartHeaderId == cartHeaderFromDb.Id);

                    foreach (var item in oldCartItems)
                    {
                        await _cartItemRepository.RemoveAsync(item);
                    }
                    
                    cartHeaderFromDb.StoreId = productDto!.StoreId;
                    
                    await _cartHeaderRepository.UpdateAsync(cartHeaderFromDb);
                }

                var newCartItem = new CartItem
                {
                    CartHeader = cartHeaderFromDb,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    Price = productDto!.Price,
                };
                
                newCartItem.CartHeaderId = cartHeaderFromDb.Id;
                newCartItem.Id = Guid.NewGuid();
                
                await _cartItemRepository.CreateAsync(newCartItem);
            }
            else
            {
                cartItemFromDb.Quantity += request.Quantity;
                
                await _cartItemRepository.UpdateAsync(cartItemFromDb);
            }

            response.Message = "The cart has been updated successfully!";
            
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

            response.Message = "The cart has been updated successfully!";
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
