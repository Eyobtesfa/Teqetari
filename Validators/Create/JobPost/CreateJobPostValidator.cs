using FluentValidation;
using TeqetariApi.DTO.Create.JobPost;


namespace TeqetariApi.Validators.Create.JobPost;

public class JobPostBaseDtoValidator : AbstractValidator<CreateJobPostDto>
{
    public JobPostBaseDtoValidator()
    {

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Job title is required.")
            .MaximumLength(100).WithMessage("Job title must not exceed 100 characters.");


        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Job description is required.")
            .MaximumLength(5000).WithMessage("Job description must not exceed 5000 characters.");

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Please select a valid job category.");


        RuleFor(x => x.RequiredSkills)
            .NotNull().WithMessage("Skills list cannot be null.")
            .Must(skills => skills != null && skills.Count > 0).WithMessage("At least one required skill must be specified.")
            .ForEach(skill =>
                skill.NotEmpty().WithMessage("Skill names cannot be blank."));


        RuleFor(x => x.OfferedSalary)
            .GreaterThan(0).WithMessage("Offered salary must be greater than zero.")

            .PrecisionScale(18, 2, ignoreTrailingZeros: true)
            .WithMessage("Salary cannot exceed 2 decimal places.");


        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(200).WithMessage("Location must not exceed 200 characters.");


        RuleFor(x => x.ExpirationDate)
            .NotEmpty().WithMessage("Expiration date is required.")

            .GreaterThan(DateTime.UtcNow).WithMessage("Expiration date must be a future date.");
    }
}
