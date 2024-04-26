using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorbikeRental.Domain.Commands.Courier;
using MotorbikeRental.Filters;
using System.Net.Mime;

namespace MotorbikeRental.Controllers
{
    [Authorize(Policy = "user")]
    [CustomExceptionFilter]
    [ApiController, Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1/couriers", Name = "Delivery Man")]
    public class CouriersController : Controller
    {
        private readonly IMediator mediator;

        public CouriersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("create-courier")]
        public async Task<IActionResult> CreateCourier([FromBody] CreateCourierCommand command)
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