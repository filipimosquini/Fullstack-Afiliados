using Backend.Core.Resources;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Backend.Core.Services.Contracts.Business;

public class FinancialTransactionFileContentBusinessContract : AbstractValidator<IFormFile>
{
    private List<string> Errors = new List<string>();

    public FinancialTransactionFileContentBusinessContract()
    {
        RuleFor(x => x)
            .CustomAsync(ValidateFileStructure);
    }

    protected string ProvideErrorMessage()
        => string.Format(Messages.FileStructureBusinessContract_Message, string.Join("|", Errors.ToArray()));

    protected async Task ValueIsInteger(string value, Func<List<string>, Task<List<string>>> executeCallback)
    {
        var _errors = new List<string>();

        if (string.IsNullOrWhiteSpace(value))
        {
            _errors.Add(Messages.FileStructureBusinessContract_ValueNoInformed);
        }

        if (!Int32.TryParse(value, out _))
        {
            _errors.Add(Messages.FileStructureBusinessContract_ValueMustBeInteger);
        }

        await executeCallback(_errors);
    }

    protected async Task ValueIsDouble(string value, Func<List<string>, Task<List<string>>> executeCallback)
    {
        var _errors = new List<string>();

        if (string.IsNullOrWhiteSpace(value))
        {
            _errors.Add(Messages.FileStructureBusinessContract_ValueNoInformed);
        }

        if (!double.TryParse(value, out _))
        {
            _errors.Add(Messages.FileStructureBusinessContract_ValueMustBeDouble);
        }

        await executeCallback(_errors);
    }

    protected async Task ValueIsDateTime(string value, Func<List<string>, Task<List<string>>> executeCallback)
    {
        var _errors = new List<string>();

        if (string.IsNullOrWhiteSpace(value))
        {
            _errors.Add(Messages.FileStructureBusinessContract_ValueNoInformed);
        }

        if (!DateTime.TryParse(value, out _))
        {
            _errors.Add(Messages.FileStructureBusinessContract_ValueMustBeDateTime);
        }

        await executeCallback(_errors);
    }

    protected async Task ValueIsNotNullOrWhiteSpace(string value, Func<List<string>, Task<List<string>>> executeCallback)
    {
        var _errors = new List<string>();

        if (string.IsNullOrWhiteSpace(value))
        {
            _errors.Add(Messages.FileStructureBusinessContract_ValueNoInformed);
        }

        await executeCallback(_errors);
    }

    protected async Task<bool> ValidateFileStructure(IFormFile file, ValidationContext<IFormFile> context, CancellationToken cancellationToken)
    {
        using (StreamReader sr = new StreamReader(file.OpenReadStream()))
        {
            string line;
            int index = 0;
            while ((line = sr.ReadLine()) != null)
            {
                index++;

                if (string.IsNullOrWhiteSpace(line))
                {
                    Errors.Add( string.Format(Messages.FileStructureBusinessContract_LineIsEmpty, index));
                    continue;
                }

                var lineContent = new
                {
                    type = line.Substring(0, 1),
                    date = line.Substring(1, 25),
                    product = line.Substring(26, 30),
                    value = line.Substring(56, 10),
                    seller = line.Substring(66, line.Length - 66)
                };

                await ValueIsInteger(lineContent.type, (errors) =>
                {
                    errors.ForEach(error =>
                    {
                        error = string.Format(error, "Type", index);

                        Errors.Add(error);
                    });

                    return Task.FromResult(Errors);
                });

                await ValueIsDateTime(lineContent.date, (errors) =>
                {
                    errors.ForEach(error =>
                    {
                        error = string.Format(error, "Date", index);

                        Errors.Add(error);
                    });

                    return Task.FromResult(Errors);
                });

                await ValueIsNotNullOrWhiteSpace(lineContent.product, (errors) =>
                {
                    errors.ForEach(error =>
                    {
                        error = string.Format(error, "Product", index);

                        Errors.Add(error);
                    });

                    return Task.FromResult(Errors);
                });

                await ValueIsDouble(lineContent.value, (errors) =>
                {
                    errors.ForEach(error =>
                    {
                        error = string.Format(error, "Value", index);

                        Errors.Add(error);
                    });

                    return Task.FromResult(Errors);
                });

                await ValueIsNotNullOrWhiteSpace(lineContent.seller, (errors) =>
                {
                    errors.ForEach(error =>
                    {
                        error = string.Format(error, "Seller", index);

                        Errors.Add(error);
                    });

                    return Task.FromResult(Errors);
                });
            }

            if (Errors.Any())
            {
                context.AddFailure(ProvideErrorMessage());
                return false;
            }

            return true;
        }
    }
}
