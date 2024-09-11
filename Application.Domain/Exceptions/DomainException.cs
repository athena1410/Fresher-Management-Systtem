using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Application.Domain.Exceptions
{
    public class DomainException : ValidationException
    {
        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, IEnumerable<ValidationFailure> errors) : base(message, errors)
        {
        }

        public DomainException(string message, IEnumerable<ValidationFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        public DomainException(IEnumerable<ValidationFailure> errors) : base(errors)
        {
        }
    }
}
