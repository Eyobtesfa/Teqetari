using FluentValidation;
using TeqetariApi.Application.DTOs.Create.JobApplication;

namespace TeqetariApi.Application.Validators.Create.JobApplication;

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