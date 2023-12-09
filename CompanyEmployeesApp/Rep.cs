using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployeesApp
{
    public class Rep : HttpClientCompanyEmployees
    {
        //Authentication
        private readonly string AuthenticationUrl = "api/authentication";
        private readonly string LoginUrl = "api/authentication/login";
        private readonly HttpClient _httpClient;
        private static string Token { get; set; }
        public Rep()
        {
            _httpClient = GetHttpClient();
        }
        public async Task<HttpResponseMessage> PostAuthenticationLogin(UserForAuthenticationDto login)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(LoginUrl, login);
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(await responseMessage.Content.ReadAsStringAsync());
            Token = responseData.access_token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenStorage.Key);
            return responseMessage;
        }
        public async Task<HttpResponseMessage> PostAuthenticationRegister(UserForRegistrationDto register)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(AuthenticationUrl, register);
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(await responseMessage.Content.ReadAsStringAsync());
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenStorage.Key);
            return responseMessage;
        }s
    }
}
