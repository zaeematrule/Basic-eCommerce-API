using HumbleMediator;
using WebApiTemplate.Core;
using WebApiTemplate.Core.Customers;

namespace WebApiTemplate.Application.Customers.Queries;

public sealed record GetAllCustomersQuery() : IQuery<PaginatedResponse<Product>>
{
    public int PageSize { get; set;}
    public int Page { get; set; }

    
}
