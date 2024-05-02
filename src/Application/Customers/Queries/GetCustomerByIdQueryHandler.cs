using HumbleMediator;
using WebApiTemplate.Application.Customers.Queries;
using WebApiTemplate.Core.Customers;

namespace WebApiTemplate.Application.Customers.Queries;

public class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, Product?>
{
    private readonly ICustomerReadRepository _customerReadRepository;

    public GetCustomerByIdQueryHandler(ICustomerReadRepository customerReadRepository)
    {
        _customerReadRepository = customerReadRepository;
    }

    public Task<Product?> Handle(
        GetCustomerByIdQuery query,
        CancellationToken cancellationToken = default
    ) => _customerReadRepository.GetById(query.Id);
}
