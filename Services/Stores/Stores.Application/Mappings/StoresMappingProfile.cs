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
        CreateMap<AdministrativeRegion, AdministrativeRegionDto>().ReverseMap();
        CreateMap<AdministrativeUnit, AdministrativeUnitDto>().ReverseMap();
        CreateMap<Province, ProvinceDto>().ReverseMap();
        CreateMap<District, DistrictDto>().ReverseMap();
        CreateMap<Ward, WardDto>().ReverseMap();
    }
}
