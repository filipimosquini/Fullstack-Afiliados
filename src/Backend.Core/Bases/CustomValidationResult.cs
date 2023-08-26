using FluentValidation.Results;

namespace Backend.Core.Bases;

public class CustomValidationResult : ValidationResult
{
    public object? Data { get; set; }
}