using FluentAssertions;
using NSubstitute;
using WebApiTemplate.Application.Products.Queries;
using WebApiTemplate.Core.Products;
using Xunit;

namespace WebApiTemplate.UnitTests.Products.GetProductByIdQueryHandler;

public class HandleTests
{
    [Fact]
    public async Task WithValidRequestShouldCallRepository()
    {
        // Arrange
        var expected = new Product(1);
        var mock = Substitute.For<IProductReadRepository>();
        mock.GetById(default).ReturnsForAnyArgs(expected);

        var sut = new Application.Products.Queries.GetProductByIdQueryHandler(mock);

        // Act
        var result = await sut.Handle(new GetProductByIdQuery(1));

        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}
