using FluentValidation;
using TeqetariApi.Application.DTOs.Update.PlacementContract;


namespace TeqetariApi.Application.Validators.Update.PlacementContract;

public class UpdatePlacementContractValidator : AbstractValidator<UpdatePlacementContractDto>
{
    public UpdatePlacementContractValidator()
    {
        RuleFor(x => x.Salary)
        .NotNull().WithMessage("Salary is required.")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Salary cannot be negative.");




        RuleFor(x => x.AgencyCommissionPercentage)
        .NotNull().WithMessage("AgencyCommissionPercentage is required.")
            .InclusiveBetween(0m, 100m)
            .WithMessage("Agency commission percentage must be between 0 and 100.");



        RuleFor(x => x.StartDate)
            .NotNull().WithMessage("Start date is required.")
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Manufactured date cannot be in the past.");


        RuleFor(x => x.EndDate)
            .NotNull().WithMessage("End date is required.")

            .GreaterThan(x => x.StartDate).WithMessage("End date must be later than the start date.");
    }
}