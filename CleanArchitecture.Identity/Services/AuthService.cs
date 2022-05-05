using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        /*Objeto que permite hacer la validacion de credenciales 
         * desde una instancia del objeto SingIn Manager*/
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService (
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            //primero buscar al usuario en la base de datos.
            var user = await _userManager.FindByEmailAsync(request.Email);
            
            if (user == null)
                throw new Exception($"El usuario con Email " +
                    $"{request.Email} no existe");

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName,
                request.Password, false, false);

            if (!result.Succeeded)
                throw new Exception("Las credenciales son incorrectas");

            var authResponse = new AuthResponse
            {
                Id = user.Id,
                Token = "",
                Email = user.Email,
                Username = user.UserName
            };
            return authResponse;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.Username);
            if (existingUser == null)
                throw new Exception("El username ya existe");

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser == null)
                throw new Exception("El email ya existe");

            var user = new ApplicationUser
            {
                Email = request.Email,
                Nombre = request.Nombre,
                Apellidos = request.Apellidos,
                UserName = request.Username,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Operator");
                return new RegistrationResponse
                {
                    Email = user.Email,
                    Token = "",
                    UserId = user.Id,
                    Username = user.UserName
                };
            }
            throw new Exception($"{result.Errors}");
        }
    }
}
