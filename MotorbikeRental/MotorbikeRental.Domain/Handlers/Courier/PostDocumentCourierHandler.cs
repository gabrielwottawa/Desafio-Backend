using MediatR;
using MotorbikeRental.Domain.Commands.Courier;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Domain.Validations;
using MotorbikeRental.Domain.Validations.Courier;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Domain.Handlers.Courier
{
    public class PostDocumentCourierHandler : IRequestHandler<PostDocumentCourierCommand, CommandResult>
    {
        private readonly ICouriersRepository _couriersRepository;
        private readonly Validator<PostDocumentCourierCommand> _validator = new(new PostDocumentCourierCommandValidator());

        public PostDocumentCourierHandler(ICouriersRepository couriersRepository)
        {
            _couriersRepository = couriersRepository;
        }

        public async Task<CommandResult> Handle(PostDocumentCourierCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAsync(request, cancellationToken);

            var courier = await _couriersRepository.GetCourierByCnpjAndRegisterNumberAsync(request.Cnpj, request.RegisterNumber)
                            ?? throw new ApplicationException("Não existe entregador com os dados fornecidos.");

            var rootDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var rootDrive = Path.GetPathRoot(rootDirectory);

            var uploadFolder = Path.Combine(rootDrive + "Documents", "RegisterDocument");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.FileName).ToLower();
            var filePath = Path.Combine(uploadFolder, fileName);

            await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(request.Content));

            await _couriersRepository.InsertUrlImageAsync(courier.Id, filePath);

            return new CommandResult { Message = "Documento enviado com sucesso." };
        }
    }
}