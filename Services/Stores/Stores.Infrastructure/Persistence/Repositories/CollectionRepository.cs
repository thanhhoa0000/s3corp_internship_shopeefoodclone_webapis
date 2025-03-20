namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Repositories;

public class CollectionRepository(StoreContext context)
    : Repository<Collection, StoreContext>(context), ICollectionRepository;
