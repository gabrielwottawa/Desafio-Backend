using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorbikeRental.Domain.Commands.MotorbikeRental;
using MotorbikeRental.Filters;
using System.Net.Mime;

namespace MotorbikeRental.Controllers
{
    [Authorize(Policy = "User")]
    [CustomExceptionFilter]
    [ApiController, Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1/motorbike-rental", Name = "Motorbike Rental")]
    public class MotorbikeRentalsController : Controller
    {
        private readonly IMediator mediator;

        public MotorbikeRentalsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("create-motorbike-rental")]
        public async Task<IActionResult> CreateMotorbikeRentalAsync([FromBody] CreateMotorbikeRentalCommand command)
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

        [HttpGet("get-total-rental-value")]
        public async Task<IActionResult> GetTotalRentalValue([FromBody] GetTotalRentalValueCommand command)
        {
            try
            {
                return Ok(await mediator.Send(command));
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}