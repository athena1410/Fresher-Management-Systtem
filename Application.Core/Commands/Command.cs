using Application.Core.Interfaces.CQRS;
using System;

namespace Application.Core.Commands
{
    public class Command : ICommand
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

    public class Command<TResult> : ICommand<TResult>
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
