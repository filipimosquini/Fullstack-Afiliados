using Backend.Core.Entities;
using Backend.Core.Resources;
using FluentValidation;

namespace Backend.Core.Services.Contracts.Business;

public class FinancialTransactionBusinessContract : AbstractValidator<FinancialTransaction>
{
    public FinancialTransactionBusinessContract()
    {
        RuleFor(x => x.Seller)
            .NotNull()
            .WithMessage(Messages.FinancialTransactionBusinessContract_SellerIsRequired);

        RuleFor(x => x.Product)
            .NotNull()
            .WithMessage(Messages.FinancialTransactionBusinessContract_ProductIsRequired);

        RuleFor(x => x.FinancialTransactionType)
            .NotNull()
            .WithMessage(Messages.FinancialTransactionBusinessContract_FinancialTransactionTypeIsRequired);

        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithMessage(Messages.FinancialTransactionBusinessContract_ValueMustBeGreaterThanZero);

        RuleFor(x => x.Date)
            .Must(BeAValidDate)
            .WithMessage(Messages.FinancialTransactionBusinessContract_DateMustBeValid);

    }

    private bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }
}