using FluentValidation;
using TeqetariApi.Application.DTOs.Create.JobPost;

namespace TeqetariApi.Application.Validators.Create.JobPost;

public class CreateJobPostDtoValidator : AbstractValidator<CreateJobPostDto>
{
    public CreateJobPostDtoValidator()
    {
        // --- Foreign Keys ---
        RuleFor(x => x.EmployerId)
            .GreaterThan(0)
            .WithMessage("EmployerId must be a valid positive integer.");

        // --- Text Fields ---
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Job title is required.")
            .MaximumLength(150).WithMessage("Job title must not exceed 150 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Job description is required.")
            .MinimumLength(20).WithMessage("Job description must be at least 20 characters long.")
            .MaximumLength(3000).WithMessage("Job description must not exceed 3000 characters.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(100).WithMessage("Location must not exceed 100 characters.");

        // --- Enums ---
        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Invalid job category provided.");

        // --- Salary Range Validation ---
        RuleFor(x => x.OfferedSalaryMin)
            .GreaterThanOrEqualTo(0).WithMessage("Minimum offered salary cannot be negative.");

        RuleFor(x => x.OfferedSalaryMax)
            .GreaterThanOrEqualTo(x => x.OfferedSalaryMin)
            .WithMessage("Maximum offered salary must be greater than or equal to the minimum offered salary.");

        // --- Experience ---
        RuleFor(x => x.MinimumExperienceYears)
            .GreaterThanOrEqualTo(0).WithMessage("Minimum experience years cannot be negative.")
            .LessThanOrEqualTo(40).WithMessage("Minimum experience years must be realistic (40 years max).");

        // --- Required Skills List ---
        RuleFor(x => x.RequiredSkills)
            .NotNull().WithMessage("Required skills list cannot be null.")
            .Must(skills => skills != null && skills.Count > 0)
            .WithMessage("At least one required skill must be specified.");

        RuleForEach(x => x.RequiredSkills)
            .NotEmpty().WithMessage("Skill item cannot be empty.")
            .MaximumLength(50).WithMessage("Individual skill name must not exceed 50 characters.");

        // --- Expiration Date ---
        RuleFor(x => x.ExpirationDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Expiration date must be set in the future.");
    }
}