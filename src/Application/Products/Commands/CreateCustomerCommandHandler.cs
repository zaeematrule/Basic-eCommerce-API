using HumbleMediator;
using WebApiTemplate.Core;
using WebApiTemplate.Core.Products;

namespace WebApiTemplate.Application.Products.Commands;

public class CreateProductCommandHandler
    : ProductCommandHandlerBase,
        ICommandHandler<CreateProductCommand, Product>
{
    public CreateProductCommandHandler(
        IUnitOfWorkFactory uowFactory,
        IProductWriteRepository repository
    )
        : base(uowFactory, repository) { }

    public async Task<Product> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken = default
    )
    {
        Product product = new Product(null)
        {
            Description = command.Description,
            Price = command.Price,
            Quantity = command.Quantity,
            Name = command.Name,
        };

        await using var uow = await _uowFactory.Create(cancellationToken);
        await _repository.Create(product, uow);
        await uow.Commit(cancellationToken);
        return product;
    }
}
