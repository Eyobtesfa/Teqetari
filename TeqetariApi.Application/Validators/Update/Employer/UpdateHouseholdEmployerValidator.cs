using FluentValidation;
using TeqetariApi.Application.DTOs.Update.Employer;

namespace TeqetariApi.Application.Validators.Update.Employer;

public class UpdateHouseholdEmployerValidator : AbstractValidator<UpdateHouseholdEmployerDto>
{
    public UpdateHouseholdEmployerValidator()
    {
        RuleFor(x => (UpdateEmployerDto)x)
            .SetValidator(new UpdateEmployerValidator());

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MinimumLength(2).WithMessage("First name must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");
        RuleFor(x => x.NationalIdNumber)
            .NotEmpty().WithMessage("National ID number is required.");
        RuleFor(x => x.NumberOfFamilyMembers)
            .GreaterThan(0).WithMessage("Number of family members must be greater than 0.");
    }
}