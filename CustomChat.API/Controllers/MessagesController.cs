using CustomChat.Application.Features.Messages.Commands;
using CustomChat.Application.Features.Messages.Queries;
using CustomChat.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomChat.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController(IMediator _mediator) : ControllerBase
    {
        [Authorize]
        [HttpGet("getMessages")]
        public async Task<Result<GetMessagesResponse>> GetChatMessages([FromQuery] GetMessagesQuery request)
    => await _mediator.Send(request);


        [Authorize]
        [HttpDelete("wipeChat")]
        public async Task<Result> WipeChat([FromQuery] WipeChatMessagesCommand request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPost("ask")]
        public async Task<Result<AskResponse>> Ask([FromQuery] AskCommand request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPut("editMessage")]
        public async Task<Result> EditMessage([FromQuery] EditMessageCommand request)
            => await _mediator.Send(request);
    }
}
