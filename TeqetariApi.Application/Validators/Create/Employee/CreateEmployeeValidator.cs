using FluentValidation;
using TeqetariApi.Application.DTOs.Create.Employee;

namespace TeqetariApi.Application.Validators.Create.Employee;

public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Length(3, 50).WithMessage("First name must be between 3 and 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Length(3, 50).WithMessage("Last name must be between 3 and 50 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")

            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Please enter a valid phone number.");

        RuleFor(x => x.NationalIdNumber)
            .NotEmpty().WithMessage("National ID number is required.")
            .Length(16).WithMessage("National ID number must be exactly 16 characters.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Please enter a valid email address.")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City name is too long.");

        RuleFor(x => x.SubCity)
            .NotEmpty().WithMessage("Sub-city is required.")
            .MaximumLength(100).WithMessage("Sub-city name is too long.");

        RuleFor(x => x.Woreda)
            .NotEmpty().WithMessage("Woreda is required.")
            .MaximumLength(100).WithMessage("Woreda designation is too long.");

        RuleFor(x => x.YearsOfExperience)
            .InclusiveBetween(0, 50).WithMessage("Years of experience must be between 0 and 50.");

        RuleFor(x => x.ExpectedSalary)
            .InclusiveBetween(0, 1000000).WithMessage("Expected salary must be between 0 and 1,000,000.")
            .PrecisionScale(18, 2, ignoreTrailingZeros: true).WithMessage("Salary cannot exceed 2 decimal places.");

        RuleFor(x => x.Skills)
            .NotNull().WithMessage("Skills list is required.")
            .Must(skills => skills != null && skills.Count > 0).WithMessage("At least one skill must be provided.")
            .ForEach(skill =>
                skill.NotEmpty().WithMessage("Skill entries cannot be blank."));

        RuleFor(x => x.JobCategory)
            .IsInEnum().WithMessage("Please select a valid job category.");
    }
}
