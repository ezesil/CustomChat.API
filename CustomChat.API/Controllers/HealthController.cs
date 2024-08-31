using CustomChat.Application.Features.Ping;
using CustomChat.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomChat.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("ping")]
        public async Task<Result> Ping() => await _mediator.Send(new PingCommand());
    }
}
