namespace ShopeeFoodClone.WebApi.Products.Infrastructure.Persistence.Repositories;

public class MenuRepository(ProductContext context)
    : Repository<Menu, ProductContext>(context), IMenuRepository;
    