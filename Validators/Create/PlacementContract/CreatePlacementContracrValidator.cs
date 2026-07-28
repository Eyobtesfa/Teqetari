using FluentValidation;
using TeqetariApi.DTO.Create.PlacementContract;


namespace TeqetariApi.Validators.Create.PlacementContract;

public class CreatePlacementContractValidator : AbstractValidator<CreatePlacementContractDto>
{
    public CreatePlacementContractValidator()
    {
        RuleFor(x => x.EmployerId).NotNull().GreaterThan(0);
        RuleFor(x => x.EmployeeId).NotNull().GreaterThan(0);
        RuleFor(x => x.JobPostId).NotNull().GreaterThan(0);

        RuleFor(x => x.StartDate)
            .NotNull()
            .WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .NotNull()
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be strictly after the start date.");

        RuleFor(x => x.Salary)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Salary cannot be negative.");

        RuleFor(x => x.AgencyCommissionPercentage)
            .NotNull()
            .InclusiveBetween(0m, 100m)
            .WithMessage("Agency commission percentage must be between 0 and 100.");
    }
}