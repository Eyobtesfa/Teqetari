using FluentValidation;
using TeqetariApi.Application.DTOs.Update.JobApplication;

namespace TeqetariApi.Application.Validators.JobPost.JobApplication;

public class UpdateEmployeeApplicationDtoValidator : AbstractValidator<UpdateEmployeeApplicationDto>
{
    public UpdateEmployeeApplicationDtoValidator()
    {
        RuleFor(x => x.CoverLetter)
            .MaximumLength(2000)
            .WithMessage("Cover letter must not exceed 2000 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.CoverLetter));
    }
}