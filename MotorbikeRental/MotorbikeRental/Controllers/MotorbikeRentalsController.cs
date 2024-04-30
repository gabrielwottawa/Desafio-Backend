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

        /// <summary>
        /// Registra um aluguel de moto no sistema.
        /// </summary>
        /// <remarks>
        /// Obs.: Caso não tenha um usuário, solicitar para o administrador do sistema.
        /// 
        /// Descrição dos campos do payload:
        /// 
        ///     {
        ///         "motorbikePlate": "Placa da moto que deseja alugar.",
        ///         "courierCnpj": "CNPJ do entregador que vai alugar a moto.",
        ///         "courierRegisterNumber": "CNHdo entregador que vai alugar a moto.",
        ///         "estimatedEndDate": "Data de estimativa de entrega da moto.",
        ///         "rentalPlansId": "Id do plano que está sendo contratado"
        ///     }
        /// 
        /// Exemplo de chamada:
        ///
        ///     POST /api/v1/motorbike-rental/create-motorbike-rental
        ///     {
        ///         "motorbikePlate": "ABS1224",
        ///         "courierCnpj": "82.479.095/0001-32",
        ///         "courierRegisterNumber": "60411512544",
        ///         "estimatedEndDate": "2024-05-06",
        ///         "rentalPlansId": 1
        ///     }
        ///     
        /// Exemplo de resposta:
        /// 
        ///     {
        ///         "message": "Aluguel da moto 'Honda Titan' com a placa: 'ABS1222' feito com sucesso.",
        ///         "data": null
        ///     }
        ///
        /// </remarks>
        /// <param name="command">Payload para registro de aluguel de uma moto.</param>
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

        /// <summary>
        /// Retorna os valores do aluguel.
        /// </summary>
        /// <remarks>
        /// Obs.: Caso não tenha um usuário, solicitar para o administrador do sistema.
        /// 
        /// Descrição dos campos do payload:
        /// 
        ///     {
        ///         "motorbikePlate": "Placa da moto que deseja alugar.",
        ///         "courierCnpj": "CNPJ do entregador que vai alugar a moto.",
        ///         "courierRegisterNumber": "CNHdo entregador que vai alugar a moto.",
        ///         "endDate": "Data de encerramento do aluguel."
        ///     }
        /// 
        /// Exemplo de chamada:
        ///
        ///     GET /api/v1/motorbike-rental/get-total-rental-value
        ///     {
        ///         "motorbikePlate": "ABS1224",
        ///         "courierCnpj": "82.479.095/0001-32",
        ///         "courierRegisterNumber": "60411512544",
        ///         "endDate": "2024-05-13"
        ///     }
        ///     
        /// Exemplo de resposta:
        /// 
        ///     {
        ///         "message": "Valores do aluguel retornados com sucesso.",
        ///         "data": {
        ///             "fine": 50,
        ///             "valueTotal": 386.00000,
        ///             "plate": "ABS1222",
        ///             "cnh": "87308375000100"
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <param name="command">Payload para retorno dos valores do aluguel.</param>
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