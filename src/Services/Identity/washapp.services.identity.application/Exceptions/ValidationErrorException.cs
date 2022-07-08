using System.Net;
using FluentValidation.Results;
using Humanizer;

namespace washapp.services.identity.application.Exceptions
{
    public class ValidationErrorException : AppException
    {
        public IEnumerable<ValidationFailure> _validationFailures;

        public override string Code { get; } = nameof(ValidationErrorException)
            .Underscore().Replace("_exception", string.Empty);

        public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public ValidationErrorException(IEnumerable<ValidationFailure> validationFailures): base(string.Join("\n",validationFailures.Select(x=>x.ToString())))
        {
            _validationFailures = validationFailures;
        }
    }
}
