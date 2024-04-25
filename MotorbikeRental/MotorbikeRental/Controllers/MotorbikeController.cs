using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorbikeRental.Domain.Commands.Motorbike;
using MotorbikeRental.Filters;
using System.Net.Mime;

namespace MotorbikeRental.Controllers
{
    [Authorize(Policy = "Admin")]
    [CustomExceptionFilter]
    [ApiController, Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1/motorbike", Name = "Authentication")]
    public class MotorbikeController : Controller
    {
        private readonly IMediator mediator;

        public MotorbikeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("create-motorbike")]
        public async Task<IActionResult> CreateMotorbikeAsync([FromBody] CreateMotorbikeCommand command)
        {
            try
            {
                return Ok(await mediator.Send(command));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}