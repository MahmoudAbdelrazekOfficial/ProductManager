using FluentValidation;
using ProductManager.Application.Features.Products.Commands;

namespace ProductManager.Application.Validators;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Dto.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Dto.Description).MaximumLength(1000).When(x => x.Dto.Description != null);
    }
}
