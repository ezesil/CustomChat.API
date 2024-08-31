using CustomChat.Domain.Interfaces.Mediator;
using CustomChat.Domain.Models;
using MediatR;

namespace CustomChat.Application.Features.Ping
{
    public class PingCommand : IQuery
    {
        public string? Message { get; set; }
    }

    public class PingCommandHandler : IQueryHandler<PingCommand>
    {
        public async Task<Result> Handle(PingCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(Result.Ok());
        }
    }
}
