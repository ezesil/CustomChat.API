using CustomChat.Application.Features.Chats.Commands;
using CustomChat.Application.Features.Chats.Queries;
using CustomChat.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomChat.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController(IMediator _mediator) : ControllerBase
    {
        [Authorize]
        [HttpPost("createChat")]
        public async Task<Result<CreateChatCommandResponse>> CreateChat()
            => await _mediator.Send(new CreateChatCommand());

        [Authorize]
        [HttpGet("getChat")]
        public async Task<Result<GetChatQueryResponse>> GetEntireChat([FromQuery] GetChatQuery request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpGet("getAllChats")]
        public async Task<Result<GetAllChatsQueryResponse>> GetAllChats()
            => await _mediator.Send(new GetAllChatsQuery());
    }
}
