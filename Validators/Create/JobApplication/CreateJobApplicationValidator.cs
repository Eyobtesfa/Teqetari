using FluentValidation;
using TeqetariApi.DTO.Create.JobApplication;

namespace TeqetariApi.Validators.Create.JobApplication;

public class CreateJobApplicationDtoValidator : AbstractValidator<CreateJobApplicationDto>
{
    public CreateJobApplicationDtoValidator()
    {

        RuleFor(x => x.JobPostId)
            .GreaterThan(0).WithMessage("A valid Job Post ID is required.");


        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).WithMessage("A valid Employee ID is required.");


        RuleFor(x => x.CoverLetter)
            .MaximumLength(10000).WithMessage("Cover letter must not exceed 10,000 characters.")
            .When(x => x.CoverLetter != null);
    }
}
