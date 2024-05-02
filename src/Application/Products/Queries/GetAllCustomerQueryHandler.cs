using HumbleMediator;
using WebApiTemplate.Core;
using WebApiTemplate.Core.Products;

namespace WebApiTemplate.Application.Products.Queries;

public class GetAllProductQueryHandler : IQueryHandler<GetAllProductsQuery, PaginatedResponse<Product>>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetAllProductQueryHandler(IProductReadRepository customerReadRepository)
    {
        _productReadRepository = customerReadRepository;
    }

    public Task<PaginatedResponse<Product>> Handle(
        GetAllProductsQuery query,
        CancellationToken cancellationToken = default
    )
    {
        return _productReadRepository.GetPaginatedResults(query.Page, query.PageSize);
    }
}
