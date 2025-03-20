namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Repositories;

public class WardRepository(StoreContext context) : LocationRepository<Ward>(context), IWardRepository;
