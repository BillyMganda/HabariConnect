﻿using HabariConnect.Application.Commands;
using HabariConnect.Application.Queries;
using HabariConnect.Domain.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HabariConnect.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public usersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDto userCreateDto)
        {
            var command = new CreateUserCommand { User = userCreateDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("email")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var query = new GetUserByEmailQuery { Email = email };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
