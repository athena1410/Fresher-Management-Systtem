using FluentValidation.Results;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Application.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, IEnumerable<ValidationFailure> errors) : base(message, errors)
        {
        }

        public NotFoundException(string message, IEnumerable<ValidationFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        public NotFoundException(IEnumerable<ValidationFailure> errors) : base(errors)
        {
        }

        public NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
