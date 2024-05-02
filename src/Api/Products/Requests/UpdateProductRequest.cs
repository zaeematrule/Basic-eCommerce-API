using WebApiTemplate.Core.Customers;

namespace WebApiTemplate.Api.Customers.Requests;

public class UpdateProductRequest
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
