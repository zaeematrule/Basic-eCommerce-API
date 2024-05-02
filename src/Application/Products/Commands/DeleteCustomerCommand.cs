using HumbleMediator;
using WebApiTemplate.Core.Products;

namespace WebApiTemplate.Application.Products.Commands;

public record DeleteProductCommand(int Id) : ICommand<StatusCodeResponse>;
