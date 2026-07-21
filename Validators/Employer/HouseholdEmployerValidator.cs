using TeqetariApi.Validators.Employer;
using FluentValidation;
using TeqetariApi.DTO;

namespace TeqetariApi.Validators.Employer;

public class HouseholdEmployerValidator : AbstractValidator<HouseholdEmployerDto>
{
    public HouseholdEmployerValidator()
    {
        RuleFor(x => (CreateEmployerDto)x)
            .SetValidator(new CreateEmployerValidator());

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MinimumLength(3).WithMessage("First name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MinimumLength(3).WithMessage("Last name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x => x.NationalIdNumber)
            .NotEmpty().WithMessage("National ID number is required.")
            .Length(16).WithMessage("National ID number must be exactly 16 characters long.");

        RuleFor(x => x.NumberOfFamilyMembers)
            .GreaterThanOrEqualTo(1).WithMessage("Family size must be at least 1.");
    }
}