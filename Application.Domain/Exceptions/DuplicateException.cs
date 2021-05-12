using FluentValidation.Results;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Application.Domain.Exceptions
{
    public class DuplicateException : DomainException
    {
        public DuplicateException(string message) : base(message)
        {
        }

        public DuplicateException(string message, IEnumerable<ValidationFailure> errors) : base(message, errors)
        {
        }

        public DuplicateException(string message, IEnumerable<ValidationFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        public DuplicateException(IEnumerable<ValidationFailure> errors) : base(errors)
        {
        }

        public DuplicateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
