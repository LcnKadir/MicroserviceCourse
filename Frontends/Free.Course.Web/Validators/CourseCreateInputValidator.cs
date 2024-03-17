using FluentValidation;
using Free.Course.Web.Models.Catalogs;

namespace Free.Course.Web.Validators
{
    public class CourseCreateInputValidator : AbstractValidator<CourseCreateInput>
    {
        public CourseCreateInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş geçilemez");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş geçilemez");
            RuleFor(x => x.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Süre alanı boş geçilemez");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat alanı boş geçilemez").ScalePrecision(2, 6).WithMessage("Hatalı para formatı");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Lütfen kategori alanı seçiniz");
        }
    }
}
