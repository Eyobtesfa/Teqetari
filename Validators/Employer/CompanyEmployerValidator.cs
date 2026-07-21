using TeqetariApi.Validators.Employer;
using FluentValidation;
using TeqetariApi.DTO;

public class CompanyEmployerValidator : AbstractValidator<CompanyEmployerDto>
{
    public CompanyEmployerValidator()
    {
        RuleFor(x => (CreateEmployerDto)x)
            .SetValidator(new CreateEmployerValidator());

        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage("Company name is required.")
            .MinimumLength(3).WithMessage("Company name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Company name cannot exceed 100 characters.");

        RuleFor(x => x.TradeLicenseNumber)
            .NotEmpty().WithMessage("Trade license number is required.")
            .MinimumLength(5).WithMessage("Trade license number must be at least 5 characters long.")
            .MaximumLength(50).WithMessage("Trade license number cannot exceed 50 characters.");

        RuleFor(x => x.TaxRegistrationNumber)
            .NotEmpty().WithMessage("TIN (Tax Registration Number) is required.")
            .Matches(@"^\d{10}$").WithMessage("Ethiopian TIN number must be exactly 10 digits.")
            .When(x => !string.IsNullOrEmpty(x.TaxRegistrationNumber));

        RuleFor(x => x.ContactPersonName)
            .NotEmpty().WithMessage("Contact person name is required.")
            .MinimumLength(3).WithMessage("Contact person name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Contact person name cannot exceed 100 characters.");

        RuleFor(x => x.ContactPersonRole)
            .NotEmpty().WithMessage("Contact person role is required.")
            .MinimumLength(2).WithMessage("Contact person role must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("Contact person role cannot exceed 50 characters.");

        RuleFor(x => x.CompanySize)
            .IsInEnum()
            .WithMessage("Invalid company size.");
    }
}