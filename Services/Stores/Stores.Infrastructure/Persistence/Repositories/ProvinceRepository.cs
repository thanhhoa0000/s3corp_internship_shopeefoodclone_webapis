namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Repositories;

public class ProvinceRepository(StoreContext context) : LocationRepository<Province>(context), IProvinceRepository;
