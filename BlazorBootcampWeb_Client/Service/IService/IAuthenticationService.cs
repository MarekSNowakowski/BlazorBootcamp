using BlazorBootcamp_Models;

namespace BlazorBootcampWeb_Client.Service.IService
{
    public interface IAuthenticationService
    {
        Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO signUpRequestDTO);

        Task<SignInResponseDTO> Login(SignInRequestDTO signInRequestDTO);

        Task Logut();
    }
}
