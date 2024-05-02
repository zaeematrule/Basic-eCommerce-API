using HumbleMediator;
using WebApiTemplate.Core.Products;

namespace WebApiTemplate.Application.Products.Queries;

public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, Product?>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetProductByIdQueryHandler(IProductReadRepository customerReadRepository)
    {
        _productReadRepository = customerReadRepository;
    }

    public Task<Product?> Handle(
        GetProductByIdQuery query,
        CancellationToken cancellationToken = default
    )
    {
        return _productReadRepository.GetById(query.Id);
    }
}
