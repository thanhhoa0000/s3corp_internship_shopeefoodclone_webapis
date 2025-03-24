namespace ShopeeFoodClone.WebApi.Products.Application.Mappings;

public class ProductsMappingProfile : Profile
{
    public ProductsMappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<CreateProductRequest, Product>();
        CreateMap<VendorUpdateProductRequest, Product>();
        CreateMap<VendorUpdateProductRequest, Product>();
    }
}
