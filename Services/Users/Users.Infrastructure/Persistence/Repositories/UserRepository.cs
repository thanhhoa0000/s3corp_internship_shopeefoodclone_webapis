namespace ShopeeFoodClone.WebApi.Users.Infrastructure.Persistence.Repositories;

public class UserRepository(UserContext context) : Repository<AppUser, UserContext>(context), IUserRepository { }
