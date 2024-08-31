using CustomChat.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomChat.Domain.Interfaces.Mediator
{
    public interface ICommand : IRequest<Result>, IBaseCommand { }
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand { }
    public interface IBaseCommand { }
}
