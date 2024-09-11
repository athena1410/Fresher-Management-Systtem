using MediatR;

namespace Application.Core.Interfaces.CQRS
{
    public interface ICommand: IRequest
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
