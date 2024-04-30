using MediatR;
using MotorbikeRental.Domain.Commands.MotorbikeRental;
using MotorbikeRental.Domain.Extensions;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Domain.Validations;
using MotorbikeRental.Domain.Validations.MotorbikeRental;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Domain.Handlers.MotorbikeRental
{
    public class GetTotalRentalValueHandler : IRequestHandler<GetTotalRentalValueCommand, CommandResult>
    {
        private readonly IMotorbikeRentalRepository _motorbikeRentalRepository;
        private readonly IRentalPlansRepository _rentalPlansRepository;
        private readonly Validator<GetTotalRentalValueCommand> _validator = new(new GetTotalRentalValueCommandValidator());

        public GetTotalRentalValueHandler(IMotorbikeRentalRepository motorbikeRentalRepository, IRentalPlansRepository rentalPlansRepository)
        {
            _motorbikeRentalRepository = motorbikeRentalRepository;
            _rentalPlansRepository = rentalPlansRepository;
        }

        public async Task<CommandResult> Handle(GetTotalRentalValueCommand request, CancellationToken cancellationToken)
        {
            decimal fine = 0;
            decimal valueTotal = 0;
            await _validator.ValidateAsync(request, cancellationToken);

            var motorbikeRental = await _motorbikeRentalRepository.GetMotorbikeRentalsAsync(request.MotorbikePlate, request.CourierCnpj, request.CourierRegisterNumber)
                                    ?? throw new ApplicationException("Não existe nenhum aluguel com os dados informados.");

            var rentalPlan = await _rentalPlansRepository.GetRentalPlanByIdAsync(motorbikeRental.RentalPlansId);

            if (request.EndDate <= motorbikeRental.EstimatedEndDate)
            {
                var days = motorbikeRental.EstimatedEndDate.DaysDifference(request.EndDate);
                var totalDays = motorbikeRental.EstimatedEndDate.DaysDifference(motorbikeRental.StartDate);

                if (rentalPlan.NumberDays <= 7)
                {
                    fine = (rentalPlan.ValuePerDay * Convert.ToDecimal(0.20)) * days;
                    valueTotal = rentalPlan.ValuePerDay * (totalDays - days);
                    valueTotal += fine;
                }
                else
                {
                    fine = (rentalPlan.ValuePerDay * Convert.ToDecimal(0.40)) * days;
                    valueTotal = rentalPlan.ValuePerDay * (rentalPlan.NumberDays - days);
                    valueTotal += fine;
                }
            }
            else
            {
                var days = motorbikeRental.EstimatedEndDate.DaysDifference(motorbikeRental.StartDate);
                var extraDays = request.EndDate.DaysDifference(motorbikeRental.EstimatedEndDate);
                fine = extraDays * Convert.ToDecimal(50); 
                valueTotal = rentalPlan.ValuePerDay * days;
                valueTotal += fine;
            }

            return new CommandResult { Message = "Valores do aluguel retornados com sucesso.", Data = new { Fine = fine, ValueTotal = valueTotal, Plate = motorbikeRental.MotorbikePlate, Cnh = motorbikeRental.CourierCnpj } };
        }
    }
}