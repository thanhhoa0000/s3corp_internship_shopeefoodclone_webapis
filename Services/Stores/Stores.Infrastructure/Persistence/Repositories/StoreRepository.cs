namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Repositories;

public class StoreRepository(StoreContext context) : Repository<Store, StoreContext>(context), IStoreRepository;
