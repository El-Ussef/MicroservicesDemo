using IdentityServiceApi.Dto;

namespace IdentityServiceApi.Contrat
{
    public interface IAuthService
    {
        Task<ResponseDto> RegisterAsync(RegistrationDto newUser);

        Task<LoginResponseDto> LoginAsync(LoginDto user);
    }
}
