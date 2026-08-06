using FluentValidation;
using TeqetariApi.Application.DTOs.Create.JobApplication;

namespace TeqetariApi.Application.Validators.Create.JobApplication;

public class CreateDirectHireRequestDtoValidator : CreateJobApplicationBaseDtoValidator<CreateDirectHireRequestDto>
{
    public CreateDirectHireRequestDtoValidator()
    {
        RuleFor(x => x.EmployerId)
            .GreaterThan(0)
            .WithMessage("EmployerId must be a valid positive integer.");

        RuleFor(x => x.JobTitle)
            .NotEmpty().WithMessage("Job title is required.")
            .MaximumLength(100).WithMessage("Job title must not exceed 100 characters.");

        RuleFor(x => x.RequestedDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage("Requested date cannot be set in the past.");

        RuleFor(x => x.DutyDescription)
            .NotEmpty().WithMessage("Duty description is required.")
            .MinimumLength(15).WithMessage("Duty description must be at least 15 characters long.")
            .MaximumLength(2000).WithMessage("Duty description must not exceed 2000 characters.");
    }
}