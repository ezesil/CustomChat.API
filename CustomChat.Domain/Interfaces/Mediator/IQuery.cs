using CustomChat.Domain.Models;
using MediatR;

namespace CustomChat.Domain.Interfaces.Mediator
{
    public interface IQuery : IRequest<Result>, IBaseQuery { }
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>, IBaseQuery { }
    public interface IBaseQuery { }
}
