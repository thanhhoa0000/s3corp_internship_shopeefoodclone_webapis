namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Repositories;

public class CategoryRepository(StoreContext context)
    : Repository<Category, StoreContext>(context), ICategoryRepository;
