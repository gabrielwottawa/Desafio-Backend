using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorbikeRental.Domain.Commands.Auth;
using System.Net.Mime;

namespace MotorbikeRental.Controllers
{
    [ApiController, Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1/auth", Name = "Authentication")]
    public class AuthController : Controller
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] AuthCommand authCommand)
            => Ok(await mediator.Send(authCommand));
    }
}
