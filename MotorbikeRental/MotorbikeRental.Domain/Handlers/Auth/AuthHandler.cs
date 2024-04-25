using MediatR;
using MotorbikeRental.Domain.Commands.Auth;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;
using MotorbikeRental.Services.Auth;

namespace MotorbikeRental.Domain.Handlers.Auth
{
    public class AuthHandler : IRequestHandler<AuthCommand, CommandResult>
    {
        private readonly IAuthService _authService;
        private readonly IUsersRepository _usersRepository;

        public AuthHandler(IAuthService authService, IUsersRepository usersRepository)
        {
            _authService = authService;
            _usersRepository = usersRepository;
        }

        public async Task<CommandResult> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetUser(request.Name, request.Password)
                            ?? throw new ApplicationException("Usuário não encontrado.");

            if (user.Token != null && user.TokenDateExpire > DateTime.Now)
                return new CommandResult { Message = "Token de acesso", Data = new { user.Token } };

            var token = _authService.Authenticate(user);
            await _usersRepository.UpdateToken(user.Id, token, DateTime.UtcNow.AddHours(1));

            return new CommandResult { Message = "Token de acesso", Data = new { Token = token } };
        }
    }
}