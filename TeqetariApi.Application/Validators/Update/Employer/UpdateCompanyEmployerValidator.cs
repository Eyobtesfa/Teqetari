using FluentValidation;
using TeqetariApi.Application.DTOs.Update.Employer;

namespace TeqetariApi.Application.Validators.Update.Employer;

public class UpdateCompanyEmployerValidator : AbstractValidator<UpdateCompanyEmployerDto>
{
    public UpdateCompanyEmployerValidator()
    {

        RuleFor(x => (UpdateEmployerDto)x)
            .SetValidator(new UpdateEmployerValidator());

        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage("Company name is required.")
            .Length(3, 100).WithMessage("Company name must be between 3 and 100 characters.");

        RuleFor(x => x.TradeLicenseNumber)
            .NotEmpty().WithMessage("Trade license number is required.")
            .Length(5, 50).WithMessage("Trade license number must be between 5 and 50 characters.");


        RuleFor(x => x.TaxRegistrationNumber)
            .NotEmpty().WithMessage("TIN (Tax Registration Number) is required.")
            .Matches(@"^\d{10}$").WithMessage("Ethiopian TIN number must be exactly 10 digits.");

        RuleFor(x => x.ContactPersonName)
            .NotEmpty().WithMessage("Contact person name is required.")
            .Length(3, 100).WithMessage("Contact person name must be between 3 and 100 characters.");

        RuleFor(x => x.ContactPersonRole)
            .NotEmpty().WithMessage("Contact person role is required.")
            .Length(2, 50).WithMessage("Contact person role must be between 2 and 50 characters.");

        RuleFor(x => x.CompanySize)
            .IsInEnum().WithMessage("Invalid company size selected.");
    }
}