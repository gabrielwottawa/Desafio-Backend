using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorbikeRental.Domain.Commands.Motorbike;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Filters;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace MotorbikeRental.Controllers
{
    [Authorize(Policy = "Admin")]
    [CustomExceptionFilter]
    [ApiController, Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1/motorbike", Name = "Motorbike")]
    public class MotorbikeController : Controller
    {
        private readonly IMediator mediator;

        public MotorbikeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Retorna as motos cadastradas no sistema, podendo filtrar por uma placa.
        /// </summary>
        /// <remarks>
        /// Obs.: É necessário fornecer o 'Bearer Token' no header para acessar esse endpoint.
        ///     
        /// Exemplo de resposta:
        /// 
        ///     {
        ///         "message": "Motos registradas na plataforma.",
        ///         "data": [
        ///             {
        ///                 "id": 1,
        ///                 "plate": "ABS1222",
        ///                 "year": 2017,
        ///                 "type": "Honda Titan"
        ///             },
        ///             {
        ///                 "id": 2,
        ///                 "plate": "ABS1224",
        ///                 "year": 2017,
        ///                 "type": "Honda Titan"
        ///             }
        ///         ]
        ///     }
        ///
        /// </remarks>
        /// <param name="plate">Placa da moto. Parâmetro não obrigatório.</param>
        /// <param name="pageNumber">Número da página que quer retornar, para casos com muitas motos no sistema. Parâmetro não obrigatório. (Valor default: 1)</param>
        /// <param name="pageSize">Número de registro por página. Parâmetro não obrigatório. (Valor default: 20)</param>
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

        /// <summary>
        /// Registra uma nova moto no sistema.
        /// </summary>
        /// <remarks>
        /// Obs.: Caso não tenha um usuário, solicitar para o administrador do sistema.
        /// 
        /// Descrição dos campos do payload:
        /// 
        ///     {
        ///         "plate": "Placa da moto.",
        ///         "year": "Ano da moto.",
        ///         "type": "Modelo da moto."
        ///     }
        /// 
        /// Exemplo de chamada:
        ///
        ///     POST /api/v1/auth/generate-token
        ///     {
        ///         "plate": "ABS1224",
        ///         "year": 2017,
        ///         "type": "Honda Titan"
        ///     }
        ///     
        /// Exemplo de resposta:
        /// 
        ///     {
        ///         "message": "Moto cadastrada com sucesso. Placa ABS1222",
        ///         "data": null
        ///     }
        ///
        /// </remarks>
        /// <param name="command">Payload para criação de uma moto.</param>
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

        /// <summary>
        /// Atualiza uma moto no sistema.
        /// </summary>
        /// <remarks>
        /// Obs.: Caso não tenha um usuário, solicitar para o administrador do sistema.
        /// 
        /// Descrição dos campos do payload:
        /// 
        ///     {
        ///         "id": "id da moto já registra no sistema."
        ///         "plate": "Nova placa da moto."
        ///     }
        /// 
        /// Exemplo de chamada:
        ///
        ///     PATCH /api/v1/motorbike/update-plate
        ///     {
        ///         "id": 2
        ///         "plate": "CVB1234"
        ///     }
        ///     
        /// Exemplo de resposta:
        /// 
        ///     {
        ///         "message": "Moto atualiza com sucesso. Nova placa 'CVB1234'",
        ///         "data": null
        ///     }
        ///
        /// </remarks>
        /// <param name="command">Payload para atualização da placa de uma moto.</param>
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

        /// <summary>
        /// Deleta uma moto no sistema que nunca foi alugada.
        /// </summary>
        /// <remarks>
        /// Obs.: Caso não tenha um usuário, solicitar para o administrador do sistema.
        /// 
        /// Descrição dos campos do payload:
        /// 
        ///     {
        ///         "id": "id da moto já registra no sistema."
        ///     }
        /// 
        /// Exemplo de chamada:
        ///
        ///     DELETE /api/v1/motorbike/delete-motorbike
        ///     {
        ///         "id": 2
        ///     }
        ///     
        /// Exemplo de resposta:
        /// 
        ///     {
        ///         "message": "Moto deleta com sucesso.",
        ///         "data": null
        ///     }
        ///
        /// </remarks>
        /// <param name="command">Payload para deletar uma moto que nunca foi alugada.</param>
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
        [HttpDelete("delete-motorbike")]
        public async Task<IActionResult> DeleteMotorbikeAsync([FromQuery] DeleteMotorbikeCommand command)
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