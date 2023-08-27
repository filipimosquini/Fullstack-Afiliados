using Backend.Core.Entities;
using FluentValidation;

namespace Backend.Core.Services.Contracts.Business;

public class FinancialTransactionBusinessContract : AbstractValidator<FinancialTransaction>
{
    public FinancialTransactionBusinessContract()
    {
        RuleFor(x => x.Seller)
            .NotNull()
            .WithMessage("Seller is required at line {0}");

        RuleFor(x => x.Product)
            .NotNull()
            .WithMessage("Product is required at line {0}");

        RuleFor(x => x.FinancialTransactionType)
            .NotNull()
            .WithMessage("Transaction Type is required at line {0}");

        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithMessage("Value must be greater than zero at line {0}");

        RuleFor(x => x.Date)
            .Must(BeAValidDate)
            .WithMessage("Date must be valid at line {0}");

    }

    private bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }
}