using HumbleMediator;
using WebApiTemplate.Core;
using WebApiTemplate.Core.Products;

namespace WebApiTemplate.Application.Products.Queries;

public sealed record GetAllProductsQuery() : IQuery<PaginatedResponse<Product>>
{
    public int PageSize { get; set; }
    public int Page { get; set; }


}
