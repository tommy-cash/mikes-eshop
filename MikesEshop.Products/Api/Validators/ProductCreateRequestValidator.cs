using FluentValidation;
using MikesEshop.Products.Api.Requests;

namespace MikesEshop.Products.Api.Validators;

public class ProductCreateRequestValidator : AbstractValidator<CreateProductRequest>
{
    public ProductCreateRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.ImageUrl).NotEmpty();
    }
}