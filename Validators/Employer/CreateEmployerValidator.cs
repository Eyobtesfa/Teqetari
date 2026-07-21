using FluentValidation;
using TeqetariApi.DTO;

namespace TeqetariApi.Validators.Employer;

public class CreateEmployerValidator : AbstractValidator<CreateEmployerDto>
{
    public CreateEmployerValidator()
    {
        RuleFor(x => x.EmployerType)
            .IsInEnum()
            .WithMessage("Invalid employer type.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^(?:\+251|0)?9\d{8}$|^(?:\+251|0)?7\d{8}$").WithMessage("Invalid phone number format.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.");

        RuleFor(x => x.SubCity)
            .NotEmpty().WithMessage("SubCity is required.");

        RuleFor(x => x.Woreda)
            .NotEmpty().WithMessage("Woreda is required.");
        RuleForEach(x => x.SpecialInstruction)
            .NotEmpty()
            .WithMessage("Special instruction entries cannot be empty.")
            .When(x => x.SpecialInstruction != null);
    }
}