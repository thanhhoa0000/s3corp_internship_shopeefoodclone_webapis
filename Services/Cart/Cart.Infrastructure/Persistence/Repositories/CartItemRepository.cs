namespace ShopeeFoodClone.WebApi.Cart.Infrastructure.Persistence.Repositories;

public class CartItemRepository(CartContext context) : Repository<CartItem, CartContext>(context), ICartItemRepository { }
