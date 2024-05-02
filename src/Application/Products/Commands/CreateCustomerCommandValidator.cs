using FluentValidation;

namespace WebApiTemplate.Application.Products.Commands;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        // Validate the 'Name' field: it should not be empty or null.
        RuleFor(e => e.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .Length(2, 100).WithMessage("Product name must be between 2 and 100 characters.");

        // Validate the 'Description' field: optional length validation
        RuleFor(e => e.Description)
            .MaximumLength(1000).WithMessage("Description can not be more than 1000 characters.");

        // Validate the 'Price' field: it must be a positive number and not exceed a certain range if needed
        RuleFor(e => e.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.")
            .ScalePrecision(2, 10).WithMessage("Price must not exceed 10 digits in total, with up to 2 decimal places.");

        // Validate the 'Quantity' field: it must be a non-negative number
        RuleFor(e => e.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity must be non-negative.");

    }
}
