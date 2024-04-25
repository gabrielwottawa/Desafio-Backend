using MediatR;
using MotorbikeRental.Domain.Commands.Auth;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;
using MotorbikeRental.Services.Auth;

namespace MotorbikeRental.Domain.Handlers.Auth
{
    public class AuthHandler : IRequestHandler<AuthCommand, string>
    {
        private readonly IAuthService _authService;
        private readonly IUsersRepository _usersRepository;

        public AuthHandler(IAuthService authService, IUsersRepository usersRepository)
        {
            _authService = authService;
            _usersRepository = usersRepository;
        }

        public async Task<string> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetUser(request.Name, request.Password);

            if (user == null)
                return await Task.FromResult("Usuário não encontrado.");

            if (user.Token != null && user.TokenDateExpire > DateTime.Now)
                return await Task.FromResult(user.Token);

            var token = _authService.Authenticate(user);
            await _usersRepository.UpdateToken(user.Id, token, DateTime.UtcNow.AddHours(1));

            return await Task.FromResult(token);
        }
    }
}