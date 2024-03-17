using FluentValidation;
using Free.Course.Web.Models.Discount;

namespace Free.Course.Web.Validators
{
    public class DiscountApplyInputValidator: AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Kupon alanı boş olamaz");
        }
    }
}
