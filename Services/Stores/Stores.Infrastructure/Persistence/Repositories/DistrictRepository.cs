namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Repositories;

public class DistrictRepository(StoreContext context) : LocationRepository<District>(context), IDistrictRepository;
