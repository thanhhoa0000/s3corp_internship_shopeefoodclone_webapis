namespace ShopeeFoodClone.WebApi.Stores.Application.Mappings;

public class StoresMappingProfile : Profile
{
    public StoresMappingProfile()
    {
        CreateMap<Store, StoreDto>().ReverseMap();
        CreateMap<Category, CategoryDto>()
            .ReverseMap()
            .ForMember(dest => 
                dest.Stores, 
                opt => opt.MapFrom(src => src.Stores));
    }
}
