using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorbikeRental.Domain.Commands.Test;
using System.Net.Mime;

namespace MotorbikeRental.Controllers
{
    [Authorize(Policy = "Admin")]
    [ApiController, Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1/test", Name = "Test")]
    public class TestController : Controller
    {
        private readonly IMediator mediator;

        public TestController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Endpoint para testar a aplicatação.
        /// </summary>
        /// <remarks>
        /// Endpoint criado para testes da aplicatação.
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> GetTest()
            => Ok(await mediator.Send(new TestCommand()));
    }
}