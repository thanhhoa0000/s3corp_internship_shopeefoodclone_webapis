namespace ShopeeFoodClone.WebApi.Products.Application.Mappings;

public class ProductsMappingProfile : Profile
{
    public ProductsMappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Menu, MenuDto>().ReverseMap();
    }
}
