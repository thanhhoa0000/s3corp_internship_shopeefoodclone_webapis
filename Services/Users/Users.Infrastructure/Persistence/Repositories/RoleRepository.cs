namespace ShopeeFoodClone.WebApi.Users.Infrastructure.Persistence.Repositories;

public class RoleRepository(UserContext context) : Repository<AppRole, UserContext>(context), IRoleRepository { }
