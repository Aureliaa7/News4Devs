using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using News4Devs.Shared.DTOs;
using News4Devs.Shared.Entities;
using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Models;
using News4Devs.WebAPI.Controllers;
using System;
using System.Threading.Tasks;

namespace News4Devs.Server.Controllers
{
    [ApiVersion("1.0")]
    public class ChatController : News4DevsController
    {
        private readonly IMessageService messageService;
        private readonly IMapper mapper;

        public ChatController(IMessageService messageService, IMapper mapper)
        {
            this.messageService = messageService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{fromUserId}/{toUserId}")]
        public async Task<IActionResult> GetConversation(
            [FromRoute] Guid fromUserId, 
            [FromRoute] Guid toUserId, 
            [FromQuery] PaginationFilter paginationFilter)
        {
            var messages = await messageService.GetConversationAsync(fromUserId, toUserId, paginationFilter);
            var result = mapper.Map<PagedResponseDto<ChatMessageDto>>(messages);
            return Ok(result);
        }

        
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveMessage(ChatMessageDto message)
        {
            var savedMessage = await messageService.SaveMessageAsync(mapper.Map<ChatMessage>(message));
            return Ok(mapper.Map<ChatMessageDto>(savedMessage));
        }
    }
}
