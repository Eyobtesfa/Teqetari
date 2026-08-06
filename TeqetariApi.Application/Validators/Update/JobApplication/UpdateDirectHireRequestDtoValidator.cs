using FluentValidation;
using TeqetariApi.Application.DTOs.Update.JobApplication;

namespace TeqetariApi.Application.Validators.Update.JobApplication;

public class UpdateDirectHireRequestDtoValidator : AbstractValidator<UpdateDirectHireRequestDto>
{
    public UpdateDirectHireRequestDtoValidator()
    {
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