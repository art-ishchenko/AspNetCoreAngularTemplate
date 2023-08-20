using FluentValidation.Results;

namespace AspNetCoreAngularTemplate.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string[] errors) : this()
    {
        this.Errors.Add("model", errors);
    }

    public ValidationException() : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                         .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}
