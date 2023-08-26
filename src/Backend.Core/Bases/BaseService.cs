using FluentValidation.Results;

namespace Backend.Core.Bases;

public abstract class BaseService
{
    protected CustomValidationResult CustomValidationResult;

    protected BaseService()
    {
        CustomValidationResult = new CustomValidationResult();
    }

    protected void AddError(string message)
    {
        CustomValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
    }

    protected void AddError(string propriedade, string message)
    {
        CustomValidationResult.Errors.Add(new ValidationFailure(propriedade, message));
    }

    protected void AddErrors(IReadOnlyCollection<string> messages)
    {
        foreach (var message in messages)
        {
            AddError(message);
        }
    }

    protected void AddErrors(IEnumerable<string> messages)
    {
        foreach (var message in messages)
        {
            AddError(message);
        }
    }

    protected void AddErrors(List<ValidationFailure> messages)
    {
        CustomValidationResult.Errors.AddRange(messages);
    }
}