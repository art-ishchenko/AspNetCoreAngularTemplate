using FluentValidation.Results;

namespace AspNetCoreAngularTemplate.Application.Common.Exceptions;

public class AppValidationException : Exception
{
    public AppValidationException(string[] errors) : this()
    {
        this.Errors.Add("model", errors);
    }

    public AppValidationException() : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public AppValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                         .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}
