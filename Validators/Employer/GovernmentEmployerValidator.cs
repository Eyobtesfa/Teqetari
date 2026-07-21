using FluentValidation;
using TeqetariApi.Validators.Employer;
using TeqetariApi.DTO;

public class GovernmentOrganizationValidator : AbstractValidator<GovernmentEmployerDto>
{
    public GovernmentOrganizationValidator()
    {
        RuleFor(x => (CreateEmployerDto)x)
             .SetValidator(new CreateEmployerValidator());

        RuleFor(x => x.OrganizationName)
            .NotEmpty().WithMessage("Organization name is required.")
            .MinimumLength(3).WithMessage("Organization name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Organization name cannot exceed 100 characters.");

        RuleFor(x => x.Sector)
            .IsInEnum()
            .WithMessage("Invalid government sector.");

        RuleFor(x => x.Department)
            .NotEmpty().WithMessage("Department is required.")
            .MinimumLength(3).WithMessage("Department must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Department cannot exceed 100 characters.");

        RuleFor(x => x.AuthorizedOfficerName)
            .NotEmpty().WithMessage("Authorized officer name is required.")
            .MinimumLength(3).WithMessage("Authorized officer name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Authorized officer name cannot exceed 100 characters.");

        RuleFor(x => x.OfficialLetterRefNumber)
            .NotEmpty().WithMessage("Official letter reference number is required.")
            .MinimumLength(3).WithMessage("Official letter reference number must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Official letter reference number cannot exceed 50 characters.");
    }
}