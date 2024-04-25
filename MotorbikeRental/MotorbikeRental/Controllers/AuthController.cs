﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorbikeRental.Domain.Commands.Auth;
using MotorbikeRental.Filters;
using System.Net.Mime;

namespace MotorbikeRental.Controllers
{
    [CustomExceptionFilter]
    [ApiController, Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1/auth", Name = "Authentication")]
    public class AuthController : Controller
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("generate-token")]
        public async Task<IActionResult> GenerateTokenAsync([FromBody] AuthCommand authCommand)
        {
            try
            {
                return Ok(await mediator.Send(authCommand));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}