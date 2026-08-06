using FluentValidation;
using TeqetariApi.Application.DTOs.Update.Employer;

namespace TeqetariApi.Application.Validators.Update.Employer;

public class UpdateGovernmentEmployerValidator : AbstractValidator<UpdateGovernmentEmployerDto>
{
    public UpdateGovernmentEmployerValidator()
    {
        RuleFor(x => (UpdateEmployerDto)x)
            .SetValidator(new UpdateEmployerValidator());

        RuleFor(x => x.OrganizationName)
            .NotEmpty().WithMessage("Organization name is required.")
            .MinimumLength(2).WithMessage("Organization name must be at least 2 characters long.")
            .MaximumLength(100).WithMessage("Organization name cannot exceed 100 characters.");
        RuleFor(x => x.Sector)
            .IsInEnum().WithMessage("Sector must be a valid government sector.");
        RuleFor(x => x.Department)
            .NotEmpty().WithMessage("Department is required.")
            .MinimumLength(2).WithMessage("Department must be at least 2 characters long.")
            .MaximumLength(100).WithMessage("Department cannot exceed 100 characters.");
        RuleFor(x => x.AuthorizedOfficerName)
            .NotEmpty().WithMessage("Authorized officer name is required.")
            .MinimumLength(2).WithMessage("Authorized officer name must be at least 2 characters long.")
            .MaximumLength(100).WithMessage("Authorized officer name cannot exceed 100 characters.");
        RuleFor(x => x.OfficialLetterRefNumber)
            .NotEmpty().WithMessage("Official letter reference number is required.")
            .MinimumLength(2).WithMessage("Official letter reference number must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("Official letter reference number cannot exceed 50 characters.");
    }
}