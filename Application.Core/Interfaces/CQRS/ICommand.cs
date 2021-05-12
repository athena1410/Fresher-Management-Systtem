namespace Application.Core.Interfaces.CQRS
{
    public interface ICommand
    {
    }

    public interface ICommand<TResult> : ICommand
    {
    }
}
