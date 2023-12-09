using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ComEmp
{
    public class Rep : HttpClientComEmp
    {
        private readonly string AuthenticationUrl = "api/authentication";
        private readonly string LoginUrl = "api/authentication/login";
        private readonly HttpClient _httpClient;
        public static string Token { get; set; }
        public Rep()
        {
            _httpClient = GetHttpClient();
        }
        public async Task<HttpResponseMessage> PostAuthenticationLogin(UserForAuthenticationDto login)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(LoginUrl, login);
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(await responseMessage.Content.ReadAsStringAsync());
            Token = responseData.access_token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            return responseMessage;
        }
        public async Task<HttpResponseMessage> PostAuthenticationRegister(UserForRegistrationDto register)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(AuthenticationUrl, register);
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(await responseMessage.Content.ReadAsStringAsync());
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            return responseMessage;
        }
    }
}
