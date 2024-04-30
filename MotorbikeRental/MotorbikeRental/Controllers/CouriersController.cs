using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorbikeRental.Domain.Commands.Courier;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Filters;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace MotorbikeRental.Controllers
{
    [Authorize(Policy = "user")]
    [CustomExceptionFilter]
    [ApiController, Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1/couriers", Name = "Couriers")]
    public class CouriersController : Controller
    {
        private readonly IMediator mediator;

        public CouriersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Registra um novo entregador no sistema.
        /// </summary>
        /// <remarks>
        /// Obs.: É necessário fornecer o 'Bearer Token' no header para acessar esse endpoint.
        /// 
        /// Descrição dos campos do payload:
        /// 
        ///     {
        ///         "name": "Nome completo do entregador.",
        ///         "cnpj": "CNPJ do entregador.",
        ///         "dateOfBirth": "Data de nascimento do entregador.",
        ///         "registerNumber": "CNH do entregador.",
        ///         "registerType": "Tipo de CNH do entregador (Exemplos: A, B ou AB)."
        ///     }
        /// 
        /// Exemplo de chamada:
        ///
        ///     POST /api/v1/couriers/create-courier
        ///     {
        ///         "name": "Gabriel Wottawa",
        ///         "cnpj": "87.308.375/0001-09",
        ///         "dateOfBirth": "1996-11-01",
        ///         "registerNumber": "78745771895",
        ///         "registerType": "AB"
        ///     }
        ///     
        /// Exemplo de resposta:
        /// 
        ///     {
        ///         "message": "Entregador cadastrado com sucesso.",
        ///         "data": null
        ///     }
        ///
        /// </remarks>
        /// <param name="createCourierCommand">Payload para criação de um novo entregador.</param>
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
        [HttpPost("create-courier")]
        public async Task<IActionResult> CreateCourierAsync([FromBody] CreateCourierCommand createCourierCommand)
        {
            try
            {
                return Ok(await mediator.Send(createCourierCommand));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Envia uma foto do documento do entregador.
        /// </summary>
        /// <remarks>
        /// Obs.: É necessário fornecer o 'Bearer Token' no header para acessar esse endpoint.
        /// 
        /// Descrição dos campos do payload:
        /// 
        ///     {
        ///         "cnpj": "CNPJ do entregador.",
        ///         "registerNumber": "CNH do entregador.",
        ///         "fileName": "Nome e extensão do arquivo. (Exemplos de extensões válidas: .bmp e .png",
        ///         "content": "Arquivo em 'BASE64' para ser salvo no sistema"
        ///     }
        /// 
        /// Exemplo de chamada:
        ///
        ///     POST /api/v1/couriers/upload-document
        ///     {
        ///         "cnpj": "87308375000100",
        ///         "registerNumber": "78745771893",
        ///         "fileName": "test.bmp",
        ///         "content": "Qk2KkH4AAAAAAIoAAAB8AAAAAAoAADgEAAABABgAAAAAAACQfgAnAAAAJ..."      
        ///     }
        ///     
        /// Exemplo de resposta:
        /// 
        ///     {
        ///         "message": "Documento enviado com sucesso.",
        ///         "data": null
        ///     }
        ///
        /// </remarks>
        /// <param name="documentCourierCommand">Payload para envio da foto do documento do entregador.</param>
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
        [HttpPost("upload-document")]
        public async Task<IActionResult> PostDocumentCourierAsync([FromBody] PostDocumentCourierCommand documentCourierCommand)
        {
            try
            {
                return Ok(await mediator.Send(documentCourierCommand));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}