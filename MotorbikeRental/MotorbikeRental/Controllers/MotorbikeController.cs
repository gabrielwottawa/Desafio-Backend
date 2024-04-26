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

        [HttpGet]
        public async Task<IActionResult> GetMotorbikesAsync(string? plate, int? pageNumber, int? pageSize)
        {
            try
            {
                return Ok(await mediator.Send(new GetMotorbikesCommand(plate, pageNumber, pageSize)));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
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

        [HttpPatch("update-plate")]
        public async Task<IActionResult> UpdateMotorbikePlateAsync([FromBody] UpdateMotorbikeCommand command)
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

        [HttpDelete("delete-motorbike")]
        public async Task<IActionResult> DeleteMotorbikeAsync([FromQuery] DeleteMotorbikeCommand command)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}