namespace ShopeeFoodClone.WebApi.Users.Application.Mappings;

public class UsersMappingProfile : Profile
{
    public UsersMappingProfile()
    {
        CreateMap<AppUser, AppUserDto>().ReverseMap();
        CreateMap<AppRole, AppRoleDto>().ReverseMap();
    }
}