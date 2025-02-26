namespace ShopeeFoodClone.WebApi.Identity.Application.Mappings;

public class IdentityMappingProfile : Profile
{
    public IdentityMappingProfile()
    {
        CreateMap<AppUser, AppUserDto>();
        CreateMap<RegistrationRequest, AppUser>();
    }
}