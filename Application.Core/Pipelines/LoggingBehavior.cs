using Application.Core.Commands;
using Application.Core.Extensions;
using Common.Guard;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.DTOs;

namespace Application.Core.Pipelines
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : Command<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = Guard.Null(logger, nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var commandTypeName = request.GetType().GetGenericTypeName();
            var commandAudit = new CommandAudit
            {
                ExternalId = request.Id,
                Name = commandTypeName,
                Result = null,
                CreatedDate = request.CreatedDate,
                CreatedBy = request.CreatedBy,
                ExecutionTime = TimeSpan.Zero
            };

            TResponse response;
            try
            {
                try
                {
                    commandAudit.Payload = JsonConvert.SerializeObject(request);
                }
                catch (NotSupportedException)
                {
                    _logger.LogInformation($"[Serialization ERROR] {commandAudit.ExternalId} Could not serialize the request.");
                }

                response = await next();
                commandAudit.Result = response == null ? null : JsonConvert.SerializeObject(response);
                commandAudit.ExecutionTime = DateTimeOffset.Now - request.CreatedDate;
            }
            finally
            {
                _logger.LogInformation(@$"[Handle {commandAudit.Name}]: 
                    Info={JsonConvert.SerializeObject(commandAudit)}; Execution time={commandAudit.ExecutionTime}ms");
            }
            return response;
        }
    }
}
