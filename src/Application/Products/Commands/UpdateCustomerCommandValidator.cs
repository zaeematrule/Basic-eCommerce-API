using FluentValidation;

namespace WebApiTemplate.Application.Products.Commands;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        // Rule to ensure the entity's Id is greater than 0
        RuleFor(e => e.Id)
            .GreaterThan(0).WithMessage("ID must be greater than 0");

        // Rule to ensure the Product property is not empty
        RuleFor(e => e.Product)
            .NotNull().WithMessage("Product must not be null");

        // Rule to ensure the Product's Id is greater than 0
        RuleFor(e => e.Product.Id)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0");

        // Custom rule to ensure e.Id is equal to e.Product.Id
        RuleFor(e => e)
            .Must(e => e.Id == e.Product.Id)
            .WithMessage("Query parameter must match Product ID");

        // Validate 'Name' field: it should not be empty or null and must not exceed 100 characters
        RuleFor(e => e.Product.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

        // 'Description' field validation: no specific rule since it's nullable and can accept any length

        // Validate 'Price' field: it must be a positive number and formatted as decimal
        RuleFor(e => e.Product.Price)
            .NotEmpty().WithMessage("Price is required.")
            .GreaterThan(0).WithMessage("Price must be greater than zero.")
            .ScalePrecision(2, 10).WithMessage("Price must not exceed 10 digits in total, with up to 2 decimal places.");

        // Validate 'Quantity' field: it must be non-negative
        RuleFor(e => e.Product.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity must be non-negative.");

    }
}
