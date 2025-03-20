namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Repositories;

public class SubCategoryRepository(StoreContext context)
    : Repository<SubCategory, StoreContext>(context), ISubCategoryRepository;
