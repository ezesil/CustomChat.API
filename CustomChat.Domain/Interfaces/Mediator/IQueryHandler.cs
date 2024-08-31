using CustomChat.Domain.Models;
using MediatR;

namespace CustomChat.Domain.Interfaces.Mediator
{
    public interface IBaseQueryHandler { }
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>, IBaseQueryHandler where TQuery : IQuery<TResponse> { }
    public interface IQueryHandler<TQuery> : IRequestHandler<TQuery, Result>, IBaseQueryHandler where TQuery : IQuery { }
}
