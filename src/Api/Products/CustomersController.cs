using HumbleMediator;
using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.Api.Customers.Requests;
using WebApiTemplate.Api.Customers.Responses;
using WebApiTemplate.Application.Customers.Commands;
using WebApiTemplate.Application.Customers.Queries;
using WebApiTemplate.Core;
using WebApiTemplate.Core.Customers;
using WebApiTemplate.Infrastructure.Persistence;

namespace WebApiTemplate.Api.Customers;

public sealed class CustomersController : AppControllerBase
{
    public CustomersController(IMediator mediator)
        : base(mediator) { }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<CustomerCreatedResponse>> Create(CreateCustomerCommand request)
    {
        var id = await _mediator.SendCommand<CreateCustomerCommand, int>(request);
        return CreatedAtAction(nameof(GetById), new { id }, new CustomerCreatedResponse(id));
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var result = await _mediator.SendQuery<GetCustomerByIdQuery, Product?>(
            new GetCustomerByIdQuery(id)
        );
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<Product>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
    {
        var result = await _mediator.SendQuery<GetAllCustomersQuery, PaginatedResponse<Product>>(
            new GetAllCustomersQuery { Page = page, PageSize = pageSize}
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
        Product product = new Product(request.ProductId)
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity
        };
        await _mediator.SendCommand<UpdateCustomerCommand, Nothing>(
            new UpdateCustomerCommand(id, product)
        );
        return NoContent();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.SendCommand<DeleteCustomerCommand, Nothing>(new DeleteCustomerCommand(id));
        return NoContent();
    }
}
