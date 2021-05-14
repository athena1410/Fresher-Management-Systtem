using MediatR;

namespace Application.Core.Events.Account.Register
{
    public class RegisteredEvent : INotification
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
