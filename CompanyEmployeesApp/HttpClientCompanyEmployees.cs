using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployeesApp
{
    public abstract class HttpClientCompanyEmployees
    {
        private readonly Uri _baseUri;
        protected HttpClientCompanyEmployees()
        {
            _baseUri = new Uri("https://localhost:7220/");
        }
        protected HttpClient GetHttpClient()
        {
            var client = new HttpClient
            {
                BaseAddress = _baseUri
            };
            return client;
        }
    }
}
