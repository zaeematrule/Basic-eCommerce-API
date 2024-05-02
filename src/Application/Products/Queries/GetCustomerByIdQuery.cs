using HumbleMediator;
using WebApiTemplate.Core.Products;

namespace WebApiTemplate.Application.Products.Queries;

public sealed record GetProductByIdQuery(int Id) : IQuery<Product?>;
