using MediatR;
using MotorbikeRental.Domain.Responses;

namespace MotorbikeRental.Domain.Commands.Courier
{
    public class PostDocumentCourierCommand : IRequest<CommandResult>
    {
        public string Cnpj { get; set; }
        public string RegisterNumber { get; set; }
        public string FileName { get; set; }
        public string Content { get; set; }
    }
}