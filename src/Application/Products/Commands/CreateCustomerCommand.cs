using HumbleMediator;
using WebApiTemplate.Core.Products;

namespace WebApiTemplate.Application.Products.Commands;

public record CreateProductCommand() : ICommand<Product>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
};
