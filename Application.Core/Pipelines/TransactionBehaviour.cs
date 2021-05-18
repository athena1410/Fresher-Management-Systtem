using Application.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Extensions;
using Application.Core.Interfaces.CQRS;
using Common.Guard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Core.Pipelines
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehaviour(IUnitOfWork unitOfWork,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _unitOfWork = Guard.NotNull(unitOfWork, nameof(unitOfWork));
            _logger = Guard.NotNull(logger, nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // No need create transaction for query or has current transaction
            if (_unitOfWork.HasActiveTransaction || request is not ICommand)
            {
                return await next();
            }

            var response = default(TResponse);
            var typeName = request.GetType().GetGenericTypeName();

            try
            {
                IExecutionStrategy strategy = _unitOfWork.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    await using var transaction = await _unitOfWork.BeginTransactionAsync();
                    _logger.LogInformation($"----- Begin transaction {transaction.TransactionId} for {typeName} ({request})");

                    response = await next();

                    _logger.LogInformation($"----- Commit transaction {transaction.TransactionId} for {typeName}");

                    await _unitOfWork.CommitAsync(transaction, cancellationToken);
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ERROR Handling transaction for {typeName} ({request})");
                throw;
            }
        }
    }
}
