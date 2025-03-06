namespace ShopeeFoodClone.WebApi.Orders.Application.Mappings;

public class OrdersMappingProfile : Profile
{
    public OrdersMappingProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();

        CreateMap<OrderDetailDto, CartItemDto>();
        
        CreateMap<CartItemDto, OrderDetailDto>()
            .ForMember(dest => dest.ProductName, 
                opt => opt.MapFrom(src => src.Product!.Name))
            .ForMember(dest => dest.Price,
                opt => opt.MapFrom(src => src.Product!.Price));
        
        CreateMap<OrderDto, CartHeaderDto>()
            .ForMember(dest => dest.TotalPrice,
                opt => opt.MapFrom(src => src.TotalPrice))
            .ReverseMap();
    }    
}
