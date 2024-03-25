using FluentValidation.Results;

namespace SynCore.Api.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationFailure[] Errors { get; }

    public ValidationException(ValidationFailure[] errors) : base("Erro de validação")
    {
        Errors = errors;
    }
}