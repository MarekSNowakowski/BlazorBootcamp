using BlazorBootcamp_Common;
using BlazorBootcamp_Models;
using BlazorBootcampWeb_Client.Service.IService;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace BlazorBootcampWeb_Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _client = client;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<SignInResponseDTO> Login(SignInRequestDTO signInRequestDTO)
        {
            var content = JsonConvert.SerializeObject(signInRequestDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signin", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignInResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync(SD.Local_Token, result.Token);
                await _localStorage.SetItemAsync(SD.Local_UserDetails, result.UserDTO);

                ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new SignInResponseDTO() { IsAuthSuccessful = true };
            }
            else
            {
                return result;
            }
        }

        public async Task Logut()
        {
            await _localStorage.RemoveItemAsync(SD.Local_Token);
            await _localStorage.RemoveItemAsync(SD.Local_UserDetails);

            ((AuthStateProvider)_authStateProvider).NotifyUserLoggedLogout();

            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO signUpRequestDTO)
        {
            var content = JsonConvert.SerializeObject(signUpRequestDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signup", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignUpResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new SignUpResponseDTO() { IsRegistrationSuccesful = true };
            }
            else
            {
                return new SignUpResponseDTO() { IsRegistrationSuccesful = false, Errors=result.Errors };
            }
        }
    }
}
