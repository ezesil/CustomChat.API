using CustomChat.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomChat.Domain.Interfaces.Mediator
{
    public interface IBaseCommandHandler { }
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>, IBaseCommandHandler where TCommand : ICommand<TResponse> { }
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>, IBaseCommandHandler where TCommand : ICommand { }

}
