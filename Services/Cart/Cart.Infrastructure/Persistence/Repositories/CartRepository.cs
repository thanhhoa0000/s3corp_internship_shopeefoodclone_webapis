namespace ShopeeFoodClone.WebApi.Cart.Infrastructure.Persistence.Repositories;

public class CartRepository(CartContext context) : Repository<CartHeader, CartContext>(context), ICartRepository { }
