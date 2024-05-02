using WebApiTemplate.Core.Products;
using WebApiTemplate.Infrastructure.Persistence;

namespace WebApiTemplate.Infrastructure.Products;

public class ProductWriteRepository : WriteRepositoryBase<Product>, IProductWriteRepository
{
    public ProductWriteRepository()
        : base() { }
}
