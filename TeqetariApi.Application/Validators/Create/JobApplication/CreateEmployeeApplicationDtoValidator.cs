using FluentValidation;
using TeqetariApi.Application.DTOs.Create.JobApplication;

namespace TeqetariApi.Application.Validators.Create.JobApplication;

public class CreateEmployeeApplicationDtoValidator : CreateJobApplicationBaseDtoValidator<CreateEmployeeApplicationDto>
{
    public CreateEmployeeApplicationDtoValidator()
    {
        RuleFor(x => x.JobPostId)
            .GreaterThan(0)
            .WithMessage("JobPostId must be a valid positive integer.");

        RuleFor(x => x.CoverLetter)
            .MaximumLength(2000)
            .WithMessage("Cover letter must not exceed 2000 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.CoverLetter));
    }
}