using FluentValidation;
using TeqetariApi.Application.DTOs.Update.Employer;

namespace TeqetariApi.Application.Validators.Update.Employer;

public class UpdateEmployerValidator : AbstractValidator<UpdateEmployerDto>
{
    public UpdateEmployerValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^(?:\+251|0)?9\d{8}$|^(?:\+251|0)?7\d{8}$")
            .WithMessage("Invalid Ethiopian phone number format.");

        RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
        RuleFor(x => x.SubCity).NotEmpty().WithMessage("SubCity is required.");
        RuleFor(x => x.Woreda).NotEmpty().WithMessage("Woreda is required.");

        RuleForEach(x => x.SpecialInstructions)
            .NotEmpty().WithMessage("Special instruction entries cannot be empty.")
            .When(x => x.SpecialInstructions != null);
    }
}