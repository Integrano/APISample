using CoreAPISample.API.Models;
using CoreAPISample.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreAPISample.API.Helpers
{
    public class HttpClientProvider : IHttpClientProvider
    {
        private readonly ILogger<HttpClientProvider> _logger;
        private readonly HttpClient _httpClient;
        private readonly IOptions<HttpConfiguartion> _httpConfiguartion;
        private readonly EncryptionHelper _encryptionHelper = new EncryptionHelper();

        public HttpClientProvider(ILogger<HttpClientProvider> logger, HttpClient httpClient, IOptions<HttpConfiguartion> httpConfiguartion)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpConfiguartion = httpConfiguartion;
        }

        /// <summary>
        /// Post login details to authentication api.
        /// </summary>
        /// <param name="model">View model of Authentication to be posted.</param>
        /// <returns></returns>
        public HttpResponseMessage PostAsJsonAsync(AuthenticateModel model)
        {
            using (var client = _httpClient)
            {
                using (var multiPartContent = new MultipartFormDataContent())
                {
                    model.Password = _encryptionHelper.GetEncryptedValue(model.Password);
                    client.BaseAddress = new Uri(_httpConfiguartion.Value.BaseUrl.EndsWith("/") ? _httpConfiguartion.Value.BaseUrl : _httpConfiguartion.Value.BaseUrl + "/");

                    multiPartContent.Add(new StringContent(model.UserName), "UserName");
                    multiPartContent.Add(new StringContent(model.Password ?? ""), "Password");

                    try
                    {
                        var responseTask = client.PostAsync("Authenticate/", multiPartContent);
                        responseTask.Wait();

                        return responseTask.Result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                        return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("{\"message\":\"Something went wrong.\"}", System.Text.Encoding.UTF8, "application/json") };
                    }
                }
            }
        }

        /// <summary>
        /// Post delivery details to api.
        /// </summary>
        /// <param name="model">View model of Manufacturer to be posted.</param>
        /// <returns></returns>
        public HttpResponseMessage PostDeliveryAsJsonAsync(ManufacturerViewModel model)
        {
            using (var client = _httpClient)
            {
                using (var multiPartContent = new MultipartFormDataContent())
                {
                    client.BaseAddress = new Uri(_httpConfiguartion.Value.BaseUrl.EndsWith("/") ? _httpConfiguartion.Value.BaseUrl : _httpConfiguartion.Value.BaseUrl + "/");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", model.Token);

                    try
                    {
                        var responseTask = client.PostAsJsonAsync("SaveDelivery/", model);
                        responseTask.Wait();

                        return responseTask.Result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                        return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("{\"message\":\"Something went wrong.\"}", System.Text.Encoding.UTF8, "application/json") };
                    }
                }
            }
        }

        /// <summary>
        /// Post delivery details to api.
        /// </summary>
        /// <param name="model">View model of Delivery to be posted.</param>
        /// <returns></returns>
        public HttpResponseMessage PostDeliveryDetailsAsJsonAsync(DeliveryViewModel model)
        {
            using (var client = _httpClient)
            {
                using (var multiPartContent = new MultipartFormDataContent())
                {
                    client.BaseAddress = new Uri(_httpConfiguartion.Value.BaseUrl.EndsWith("/") ? _httpConfiguartion.Value.BaseUrl : _httpConfiguartion.Value.BaseUrl + "/");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", model.Token);

                    try
                    {
                        var responseTask = client.PostAsJsonAsync("SaveDeliveryDetails/", model);
                        responseTask.Wait();

                        return responseTask.Result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                        return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("{\"message\":\"Something went wrong.\"}", System.Text.Encoding.UTF8, "application/json") };
                    }
                }
            }
        }
    }
}
