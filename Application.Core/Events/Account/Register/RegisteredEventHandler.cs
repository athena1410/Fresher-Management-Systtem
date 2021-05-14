using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Events.Account.Register
{
    public class RegisteredEventHandler : INotificationHandler<RegisteredEvent>
    {
        private readonly ILogger<RegisteredEventHandler> _logger;

        public RegisteredEventHandler(ILogger<RegisteredEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(RegisteredEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"User with name {notification.UserName} has registered success.");
            return Task.CompletedTask;
        }
    }
}
