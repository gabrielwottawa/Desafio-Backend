using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorbikeRental.Domain.Commands.Auth;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Filters;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
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

        /// <summary>
        /// Gera um 'Bearer Token' para acesso ao restante da API.
        /// </summary>
        /// <remarks>
        /// Obs.: Caso não tenha um usuário, solicitar para o administrador do sistema.
        /// 
        /// Descrição dos campos do payload:
        /// 
        ///     {
        ///         "name": "Usuário cadastrado no sistema."
        ///         "password": "Senha do usuário cadastrado no sistema."
        ///     }
        /// 
        /// Exemplo de chamada:
        ///
        ///     POST /api/v1/auth/generate-token
        ///     {
        ///        "name": "entregador"
        ///        "password": "123"
        ///     }
        ///     
        /// Exemplo de resposta:
        /// 
        ///     {
        ///         "message": "Token de acesso",
        ///         "data": {
        ///             "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <param name="authCommand">Payload para solicitar um token</param>
        /// <returns></returns>
        /// <response code="200">
        /// O servidor atendeu à requisição com sucesso.
        /// </response>
        /// <response code="400">
        /// O servidor não pôde processar a requisição devido a uma sintaxe de requisição mal formada, uma estrutura de mensagem de requisição inválida ou valores inválidos.
        /// </response>
        /// <response code="500">
        /// O servidor encontrou uma condição inesperada que o impediu de processar a requisição.
        /// </response>
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(CommandResult))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, null, typeof(CommandResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, null, typeof(CommandResult))]
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