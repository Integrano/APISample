using System;
using System.Net;
using CoreAPISample.API.Helpers;
using CoreAPISample.API.Models;
using CoreAPISample.API.Resources;
using CoreAPISample.Core.Models;
using CoreAPISample.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

namespace CoreAPISample.API.Controllers
{
    public class MaterialsClientController : Controller
    {
        private readonly ILogger<MaterialsClientController> _logger;
        private readonly IHttpClientProvider _httpClientProvider;
        private readonly IMaterialsRepository _materialsRepository;

        public MaterialsClientController(IMaterialsRepository materialsRepository, ILogger<MaterialsClientController> logger, IHttpClientProvider httpClientProvider)
        {
            _materialsRepository = materialsRepository;
            _logger = logger;
            _httpClientProvider = httpClientProvider;

        }

        /// <summary>
        /// GET action for the sign in  
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            var model = new AuthenticateModel();

            return View(model);
        }

        /// <summary>
        /// POST action for the sign in  
        /// </summary>
        /// <param name="model">Object of the Authenticate model</param>
        [HttpPost]
        public IActionResult Login(AuthenticateModel model)
        {
            if(model.UserName != null)
            {
                var response = _httpClientProvider.PostAsJsonAsync(model);

                if (response.IsSuccessStatusCode)
                {
                    var httpSuccessObject = response.Content.ReadAsStringAsync().Result;
                    var httpResult = JObject.Parse(httpSuccessObject);
                    string successResult = (string)httpResult[AppResource.Token];

                    HttpContext.Session.SetString(AppResource.Token, successResult);
                    return RedirectToAction(AppResource.SaveManufacturerAction);
                }
                var httpErrorObject = response.Content.ReadAsStringAsync().Result;
                var result = JObject.Parse(httpErrorObject);
                string errorResult = (string)result[AppResource.Message];

                TempData[AppResource.ErrorMessage] = errorResult ?? AppResource.ErrorMessageText;
            }
            return RedirectToAction(AppResource.LoginAction);
        }

        /// <summary>
        /// GET action to save delivery.
        /// </summary>
        [HttpGet]
        public IActionResult SaveDelivery()
        {
            try
            {
                var model = new ManufacturerViewModel();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View();
            }
        }

        /// <summary>
        /// POST action to add delivery.
        /// </summary>
        [HttpPost]
        public IActionResult SaveDelivery(ManufacturerViewModel viewModel)
        {
            try
            {
                viewModel.Token = HttpContext.Session.GetString(AppResource.Token);

                var response = _httpClientProvider.PostDeliveryAsJsonAsync(viewModel);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    TempData[AppResource.ErrorMessage] = AppResource.Unauthorized;
                    return RedirectToAction(AppResource.SaveManufacturerAction);
                }
                if (response.IsSuccessStatusCode)
                {
                    TempData[AppResource.ErrorMessage] = AppResource.SaveSuccessMessageText;
                    return RedirectToAction(AppResource.SaveManufacturerAction);
                }

                TempData[AppResource.ErrorMessage] = AppResource.SaveErrorMessageText;
                return RedirectToAction(AppResource.SaveManufacturerAction);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View();
            }
        }

        /// <summary>
        /// POST action to add serialized delivery details.
        /// </summary>
        [HttpPost]
        public IActionResult SaveDeliveryDetails(DeliveryViewModel viewModel)
        {
            try
            {

                viewModel.Token = HttpContext.Session.GetString(AppResource.Token);

                var response = _httpClientProvider.PostDeliveryDetailsAsJsonAsync(viewModel);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    TempData[AppResource.ErrorMessage] = AppResource.Unauthorized;
                    return RedirectToAction(AppResource.SaveManufacturerAction);
                }
                if (response.IsSuccessStatusCode)
                {
                    TempData[AppResource.ErrorMessage] = AppResource.SaveSuccessMessageText;
                    return RedirectToAction(AppResource.SaveManufacturerAction);
                }

                TempData[AppResource.ErrorMessage] = AppResource.SaveErrorMessageText;
                return RedirectToAction(AppResource.SaveManufacturerAction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View();
            }
        }
    }

}