using CleanArchitecture.Application.Models.Identity;

namespace CleanArchitecture.Application.Contracts.Identity
{
    public interface IAuthService
    {
        //Definimos la tarea Login
        Task<AuthResponse> Login(AuthRequest request);

        //Definimos otra operacion para el registro de nuevos usuarios
        Task<RegistrationResponse> Register(RegistrationRequest request);

    }
}
