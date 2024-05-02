using HumbleMediator;
using WebApiTemplate.Application.Customers.Queries;
using WebApiTemplate.Core;
using WebApiTemplate.Core.Customers;

namespace WebApiTemplate.Application.Customers.Queries;

public class GetAllCustomerQueryHandler : IQueryHandler<GetAllCustomersQuery, PaginatedResponse<Product>>
{
    private readonly ICustomerReadRepository _customerReadRepository;

    public GetAllCustomerQueryHandler(ICustomerReadRepository customerReadRepository)
    {
        _customerReadRepository = customerReadRepository;
    }

    public Task<PaginatedResponse<Product>> Handle(
        GetAllCustomersQuery query,
        CancellationToken cancellationToken = default
    )
    {
        return _customerReadRepository.GetPaginatedResults(query.Page, query.PageSize);
    }
}
