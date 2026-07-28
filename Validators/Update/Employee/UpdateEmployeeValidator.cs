using FluentValidation;
using TeqetariApi.DTO.Update.Employee;


namespace TeqetariApi.Validators.Update.Employee;

public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeDto>
{
    public UpdateEmployeeValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^(?:\+251|0)?9\d{8}$|^(?:\+251|0)?7\d{8}$")
            .WithMessage("Invalid Ethiopian phone number format.");

        RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
        RuleFor(x => x.SubCity).NotEmpty().WithMessage("SubCity is required.");
        RuleFor(x => x.Woreda).NotEmpty().WithMessage("Woreda is required.");


        RuleFor(x => x.Skills)
            .NotEmpty().WithMessage("At least one skill must be provided.");

        RuleForEach(x => x.Skills)
            .NotEmpty().WithMessage("Skill entries cannot be empty.");

        RuleFor(x => x.YearsOfExperience)
            .GreaterThanOrEqualTo(0).WithMessage("Years of experience cannot be negative.");

        RuleFor(x => x.ExpectedSalary)
            .GreaterThan(0).WithMessage("Expected salary must be greater than 0 ETB.");

    }
}