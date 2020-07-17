using FluentValidation;

namespace ControlTemplate.Validations
{
    public class Validation:AbstractValidator<string>
    {
        public static Validation TextValidation { get; } = new Validation();

        public Validation()
        {
            RuleFor(s => s).MaximumLength(15).WithMessage("超过最长输入限制");
            RuleFor(s => s).Matches(@"^$|^[\w,_]+(\.[\w]+)?$").WithMessage("不允许非法字符");          
        }
    }
}
