using CoreAPISample.Core.Models;
using CoreAPISample.UI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreAPISample.UI.Helpers
{
    public class HttpClientProvider : IHttpClientProvider
    {
        private readonly ILogger<HttpClientProvider> _logger;
        private readonly HttpClient _httpClient;
        private readonly IOptions<HttpConfiguartion> _httpConfiguartion;

        public HttpClientProvider(ILogger<HttpClientProvider> logger, HttpClient httpClient, IOptions<HttpConfiguartion> httpConfiguartion)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpConfiguartion = httpConfiguartion;
        }

        /// <summary>
        /// Post an incident to tellsomeone api.
        /// </summary>
        /// <param name="model">View model of Authenticate to be posted.</param>
        /// <returns></returns>
        public HttpResponseMessage PostAsJsonAsync(AuthenticateModel model)
        {
            using (var client = _httpClient)
            {
                using (var multiPartContent = new MultipartFormDataContent())
                {
                    client.BaseAddress = new Uri("http://localhost:59851/");//http://localhost:59826/
                    
                    //httpConfiguartion.Value.BaseUrl.EndsWith("/") ? _httpConfiguartion.Value.BaseUrl : _httpConfiguartion.Value.BaseUrl + "/"

                    multiPartContent.Add(new StringContent(model.UserName), "UserName");
                    multiPartContent.Add(new StringContent(model.Password ?? ""), "Password");

                    var responseTask = client.PostAsync("api/Materials/Authenticate/", multiPartContent);
                    responseTask.Wait();

                    return responseTask.Result;
                }
            }
        }
    }
}
