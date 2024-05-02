using HumbleMediator;
using WebApiTemplate.Core.Products;

namespace WebApiTemplate.Application.Products.Commands;

public record UpdateProductCommand(int Id, Product Product) : ICommand<Product>;
