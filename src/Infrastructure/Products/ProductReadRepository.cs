using WebApiTemplate.Core.Products;
using WebApiTemplate.Infrastructure.Persistence;

namespace WebApiTemplate.Infrastructure.Products;

public class ProductReadRepository : ReadRepositoryBase<Product>, IProductReadRepository
{
    public ProductReadRepository(AppDbContext db)
        : base(db) { }
}
