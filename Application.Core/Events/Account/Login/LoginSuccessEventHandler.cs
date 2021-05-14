﻿using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Core.Events.Account.Login
{
    public class LoginSuccessEventHandler : INotificationHandler<LoginSuccessEvent>
    {
        private readonly ILogger<LoginSuccessEventHandler> _logger;

        public LoginSuccessEventHandler(ILogger<LoginSuccessEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(LoginSuccessEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"User with name {notification.UserName} has login success.");
            return Task.CompletedTask;
        }
    }
}
