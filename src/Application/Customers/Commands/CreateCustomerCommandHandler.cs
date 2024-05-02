using HumbleMediator;
using WebApiTemplate.Core;
using WebApiTemplate.Core.Customers;

namespace WebApiTemplate.Application.Customers.Commands;

public class CreateCustomerCommandHandler
    : CustomerCommandHandlerBase,
        ICommandHandler<CreateCustomerCommand, int>
{
    public CreateCustomerCommandHandler(
        IUnitOfWorkFactory uowFactory,
        ICustomerWriteRepository repository
    )
        : base(uowFactory, repository) { }

    public async Task<int> Handle(
        CreateCustomerCommand command,
        CancellationToken cancellationToken = default
    )
    {
        Product product = new Product(null)
        {
            Description = command.Description, Price = command.Price, Quantity = command.Quantity, Name = command.Name,
        };

        await using var uow = await _uowFactory.Create(cancellationToken);
        await _repository.Create(product, uow);
        await uow.Commit(cancellationToken);
        return product.Id ?? throw new InvalidOperationException("New customer has no Id");
    }
}
