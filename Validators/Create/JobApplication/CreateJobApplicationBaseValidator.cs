using FluentValidation;
using TeqetariApi.DTO.Create.JobApplication;

namespace TeqetariApi.Validators.Create.JobApplication;

public class CreateJobApplicationBaseDtoValidator<T> : AbstractValidator<T>
    where T : CreateJobApplicationBaseDto
{
    public CreateJobApplicationBaseDtoValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0)
            .WithMessage("EmployeeId must be a valid positive integer.");
    }
}