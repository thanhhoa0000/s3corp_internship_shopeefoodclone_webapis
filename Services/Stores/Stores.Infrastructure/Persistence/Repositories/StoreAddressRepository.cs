namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Repositories;

public class StoreAddressRepository(StoreContext context) : Repository<StoreAddress, StoreContext>(context), IStoreAddressRepository { }
