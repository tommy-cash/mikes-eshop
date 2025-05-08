using FluentValidation;
using MikesEshop.Products.Api.Dtos.Requests;

namespace MikesEshop.Products.Api.Validators;

public class UpdateProductStockRequestValidator : AbstractValidator<UpdateProductStockRequestDto>
{
    public UpdateProductStockRequestValidator() 
    {
        RuleFor(x => x.NewQuantity).NotNull();
    }
}