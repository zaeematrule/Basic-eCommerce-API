using HumbleMediator;
using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.Api.Products.Requests;
using WebApiTemplate.Application.Products.Commands;
using WebApiTemplate.Application.Products.Queries;
using WebApiTemplate.Core;
using WebApiTemplate.Core.Products;

namespace WebApiTemplate.Api.Products;

public sealed class ProductsController : AppControllerBase
{
    public ProductsController(IMediator mediator)
        : base(mediator)
    {
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Create(CreateProductCommand request)
    {
        var result = await _mediator.SendCommand<CreateProductCommand, Product>(request);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.SendQuery<GetProductByIdQuery, Product?>(
            new GetProductByIdQuery(id)
        );
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
    {
        var result = await _mediator.SendQuery<GetAllProductsQuery, PaginatedResponse<Product>>(
            new GetAllProductsQuery { Page = page, PageSize = pageSize }
        );
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductRequest request)
    {
        var product = new Product(request.ProductId)
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity
        };
        var result = await _mediator.SendCommand<UpdateProductCommand, Product>(
            new UpdateProductCommand(id, product)
        );
        return Ok(result);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var status =
            await _mediator.SendCommand<DeleteProductCommand, StatusCodeResponse>(new DeleteProductCommand(id));
        return Ok(status);
    }
}
