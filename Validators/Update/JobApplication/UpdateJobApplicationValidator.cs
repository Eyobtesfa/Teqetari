using FluentValidation;
using TeqetariApi.DTO.Update.JobApplication;

public class UpdateJobApplicationValidator : AbstractValidator<UpdateJobApplicationDto>
{
    public UpdateJobApplicationValidator(string userRole)
    {
        RuleFor(x => x)
        .Must(x => x.CoverLetter != null || x.Status != null)
        .WithMessage("You must provide at least one field to update.");

        When(_ => userRole == "Employee", () =>
        {
            RuleFor(x => x.Status)
            .Null()
            .WithMessage("Employees are not allowed to application status.");
        });

        When(_ => userRole == "Employer" || userRole == "Admin", () =>
            {
                RuleFor(x => x.CoverLetter)
                    .Null()
                    .WithMessage("Employers cannot modify candidate cover letters.");

                RuleFor(x => x.Status)
                    .IsInEnum()
                    .WithMessage("Invalid status value.");
            });
    }
}