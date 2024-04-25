using MediatR;
using MotorbikeRental.Domain.Responses;

namespace MotorbikeRental.Domain.Commands.Motorbike
{
    public class GetMotorbikesCommand : IRequest<CommandResult>
    {
        public string? Plate {  get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        public GetMotorbikesCommand(string? plate, int? pageNumber, int? pageSize)
        {
            Plate = plate;
            PageNumber = pageNumber ?? 1;
            PageSize = pageSize ?? 20;
        }
    }
}