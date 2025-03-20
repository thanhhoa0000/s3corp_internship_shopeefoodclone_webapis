namespace ShopeeFoodClone.WebApi.Products.Infrastructure.Persistence.Repositories;

public class ProductRepository(ProductContext context)
    : Repository<Product, ProductContext>(context), IProductRepository;
