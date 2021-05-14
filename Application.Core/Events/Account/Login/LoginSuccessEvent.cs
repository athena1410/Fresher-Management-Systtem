using MediatR;
using System;

namespace Application.Core.Events.Account.Login
{
    public class LoginSuccessEvent : INotification
    {
        public string UserName { get; set; }
        public DateTimeOffset LoginSuccessAt { get; set; }
    }
}
