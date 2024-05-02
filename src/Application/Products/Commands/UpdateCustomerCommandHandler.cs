using HumbleMediator;
using WebApiTemplate.Core;
using WebApiTemplate.Core.Products;

namespace WebApiTemplate.Application.Products.Commands;

public class UpdateProductCommandHandler
    : ProductCommandHandlerBase,
        ICommandHandler<UpdateProductCommand, Product>
{
    private readonly IProductReadRepository _productReadRepository;

    public UpdateProductCommandHandler(
        IUnitOfWorkFactory uowFactory,
        IProductWriteRepository repository, IProductReadRepository customerReadRepository
    )
        : base(uowFactory, repository)
    {
        _productReadRepository = customerReadRepository;
    }

    public async Task<Product> Handle(
        UpdateProductCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var product = await _productReadRepository.GetById(command.Id);
        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        product.Name = command.Product.Name;
        product.Description = command.Product.Description;
        product.Price = command.Product.Price;
        product.Quantity = command.Product.Quantity;
        product.ModifiedDate = DateTime.Now;


        await using var uow = await _uowFactory.Create(cancellationToken);
        await _repository.Update(product, uow);
        await uow.Commit(cancellationToken);
        return product;
    }
}
