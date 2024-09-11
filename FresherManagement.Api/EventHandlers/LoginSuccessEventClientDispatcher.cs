using Application.Core.Events.Account.Login;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Common.Guard;
using FresherManagement.Api.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace FresherManagement.Api.EventHandlers
{
    public class LoginSuccessEventClientDispatcher(
        IHubContext<AccountEventsClientHub> hubContext,
        ILogger<LoginSuccessEventClientDispatcher> logger)
        : INotificationHandler<LoginSuccessEvent>
    {
        private readonly IHubContext<AccountEventsClientHub> _hubContext = Guard.NotNull(hubContext, nameof(hubContext));
        private readonly ILogger<LoginSuccessEventClientDispatcher> _logger = logger;

        public async Task Handle(LoginSuccessEvent @event, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("welcome", $"User with name {@event.UserName} login success.",
                cancellationToken);
        }
    }
}
