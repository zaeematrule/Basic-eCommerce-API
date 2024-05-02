using HumbleMediator;
using WebApiTemplate.Core;
using WebApiTemplate.Core.Products;

namespace WebApiTemplate.Application.Products.Commands;

public class DeleteProductCommandHandler
    : ProductCommandHandlerBase,
        ICommandHandler<DeleteProductCommand, StatusCodeResponse>
{
    public DeleteProductCommandHandler(
        IUnitOfWorkFactory uowFactory,
        IProductWriteRepository repository
    )
        : base(uowFactory, repository) { }

    public async Task<StatusCodeResponse> Handle(
        DeleteProductCommand command,
        CancellationToken cancellationToken = default
    )
    {
        await using var uow = await _uowFactory.Create(cancellationToken);
        var status = await _repository.Delete(command.Id, uow);
        await uow.Commit(cancellationToken);
        return new StatusCodeResponse() { StatusCode = Status.Success, Id = command.Id };
    }
}
