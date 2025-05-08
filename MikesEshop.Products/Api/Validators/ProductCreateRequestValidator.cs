using FluentValidation;
using MikesEshop.Products.Api.Dtos.Requests;

namespace MikesEshop.Products.Api.Validators;

public class ProductCreateRequestValidator : AbstractValidator<CreateProductRequestDto>
{
    public ProductCreateRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.ImageUrl).NotEmpty();
    }
}