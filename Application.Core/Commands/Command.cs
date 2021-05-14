using Application.Core.Interfaces.CQRS;
using System;
using MediatR;

namespace Application.Core.Commands
{
    public class Command<T> : ICommand<T>, IRequest<T>
    {
        private Guid _id;

        public Guid Id
        {
            get
            {
                if (_id == Guid.Empty)
                {
                    _id = Guid.NewGuid();
                }
                return _id;
            }
        }

        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
