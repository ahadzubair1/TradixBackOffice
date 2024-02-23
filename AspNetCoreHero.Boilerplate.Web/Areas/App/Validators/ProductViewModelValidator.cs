using AspNetCoreHero.Boilerplate.Web.Areas.App.Models;
using FluentValidation;

namespace AspNetCoreHero.Boilerplate.Web.Areas.App.Validators
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator()
        {
            RuleFor(p => p.Barcode)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Rate).GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than 1");
        }
    }
}