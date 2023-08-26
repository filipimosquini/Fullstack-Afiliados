using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Backend.Core.Services.Contracts.Business;

public class FinancialTransactionFileStructureBusinessContract : AbstractValidator<IFormFile>
{
    private List<string> Errors = new List<string>();

    public FinancialTransactionFileStructureBusinessContract()
    {
        RuleFor(x => x)
            .CustomAsync(ValidateFileStructure);
    }

    protected string ProvideErrorMessage()
        => $"File structure is invalid, see log below: \n\n {string.Join("\n ", Errors.ToArray())} ";

    protected async Task PropertyIsInteger(string property, Func<List<string>, Task<List<string>>> executeCallback)
    {
        var _errors = new List<string>();

        if (string.IsNullOrWhiteSpace(property))
        {
            _errors.Add("Property {0} was no informed");
        }

        if (!Int32.TryParse(property, out _))
        {
            _errors.Add("Property {0} must be an integer number valid");
        }

        await executeCallback(_errors);
    }

    protected async Task PropertyIsDouble(string property, Func<List<string>, Task<List<string>>> executeCallback)
    {
        var _errors = new List<string>();

        if (string.IsNullOrWhiteSpace(property))
        {
            _errors.Add("Property {0} was no informed");
        }

        if (!double.TryParse(property, out _))
        {
            _errors.Add("Property {0} must be a decimal number valid");
        }

        await executeCallback(_errors);
    }

    protected async Task PropertyIsDateTime(string property, Func<List<string>, Task<List<string>>> executeCallback)
    {
        var _errors = new List<string>();

        if (string.IsNullOrWhiteSpace(property))
        {
            _errors.Add("Property {0} was no informed");
        }

        if (!DateTime.TryParse(property, out _))
        {
            _errors.Add("Property {0} must be a date valid");
        }

        await executeCallback(_errors);
    }

    protected async Task PropertyIsNotNullOrWhiteSpace(string property, Func<List<string>, Task<List<string>>> executeCallback)
    {
        var _errors = new List<string>();

        if (string.IsNullOrWhiteSpace(property))
        {
            _errors.Add("Property {0} was no informed");
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
                    Errors.Add($"Line {index} is empty");
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

                await PropertyIsInteger(lineContent.type, (errors) =>
                {
                    errors.ForEach(error =>
                    {
                        error = string.Format(error, "Type");
                        error += $" at line {index}";

                        Errors.Add(error);
                    });

                    return Task.FromResult(Errors);
                });

                await PropertyIsDateTime(lineContent.date, (errors) =>
                {
                    errors.ForEach(error =>
                    {
                        error = string.Format(error, "Date");
                        error += $" at line {index}";

                        Errors.Add(error);
                    });

                    return Task.FromResult(Errors);
                });

                await PropertyIsNotNullOrWhiteSpace(lineContent.product, (errors) =>
                {
                    errors.ForEach(error =>
                    {
                        error = string.Format(error, "Product");
                        error += $" at line {index}";

                        Errors.Add(error);
                    });

                    return Task.FromResult(Errors);
                });

                await PropertyIsDouble(lineContent.value, (errors) =>
                {
                    errors.ForEach(error =>
                    {
                        error = string.Format(error, "Value");
                        error += $" at line {index}";

                        Errors.Add(error);
                    });

                    return Task.FromResult(Errors);
                });

                await PropertyIsNotNullOrWhiteSpace(lineContent.seller, (errors) =>
                {
                    errors.ForEach(error =>
                    {
                        error = string.Format(error, "Seller");
                        error += $" at line {index}";

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
