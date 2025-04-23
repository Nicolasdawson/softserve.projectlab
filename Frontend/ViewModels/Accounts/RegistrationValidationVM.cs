using FluentValidation;

namespace Frontend.ViewModels.Accounts
{
    public class RegistrationValidationVM : AbstractValidator<RegistrationVM>
    {
        public RegistrationValidationVM()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm Password is required.")
                .Equal(_ => _.Password)
                .WithMessage("Passwords do not match.");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<RegistrationVM>.CreateWithOptions((RegistrationVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
