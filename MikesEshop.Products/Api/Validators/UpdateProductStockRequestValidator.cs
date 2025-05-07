using FluentValidation;
using MikesEshop.Products.Api.Requests;

namespace MikesEshop.Products.Api.Validators;

public class UpdateProductStockRequestValidator : AbstractValidator<UpdateProductStockRequest>
{
    public UpdateProductStockRequestValidator() 
    {
        RuleFor(x => x.NewQuantity).NotNull();
    }
}