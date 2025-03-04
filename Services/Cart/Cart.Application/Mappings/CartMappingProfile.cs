namespace ShopeeFoodClone.WebApi.Cart.Application.Mappings;

public class CartMappingProfile : Profile
{
    public CartMappingProfile()
    {
        CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
        CreateMap<CartItem, CartItemDto>().ReverseMap();
    }
}
