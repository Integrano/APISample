using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreAPISample.Core.Models;
using CoreAPISample.UI.Helpers;
using CoreAPISample.UI.Models;
using CoreAPISample.UI.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreAPISample.UI.Controllers
{
    public class MaterialsClientController : Controller
    {
        private readonly ILogger<MaterialsClientController> _logger;
        private readonly IHttpClientProvider _httpClientProvider;

        public MaterialsClientController(ILogger<MaterialsClientController> logger, IHttpClientProvider httpClientProvider)
        {
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
        /// <param name="model">Object of the Login model</param>
        [HttpPost]
        public IActionResult Login(AuthenticateModel model)
        {

            var response = _httpClientProvider.PostAsJsonAsync(model);

            if (response.IsSuccessStatusCode)
            {
                //TempData[AppResource.Confirm] = AppResource.ConfirmationMessage;
                return RedirectToAction(AppResource.SaveManufacturerAction);
            }
            else
            {
                TempData[AppResource.ErrorMessage] = AppResource.ErrorMessageText;
                return RedirectToAction(AppResource.LoginAction);
            }
            //ViewBag.ReturnUrl = returnUrl;
            //var userAlreadyLoggedIn = !string.IsNullOrEmpty(_sessionManager.UserName);
            //string userRole = string.Empty;

            //if (!string.IsNullOrWhiteSpace(_sessionManager.SamAccountName))
            //{
            //    var userRoles = _authenticationService.GetUserRole(_sessionManager.SamAccountName.ToString());
            //    userRole = userRoles.ToString();
            //}

            //return userAlreadyLoggedIn ? RedirectToLocal(returnUrl, userRole.ToString()) : View();

            //return Ok();
        }

        /// <summary>
        /// GET action to add manufacturer.
        /// </summary>
        [HttpGet]
        public IActionResult SaveManufacturer()
        {
            try
            {
                var model = new ManufacturerViewModel();

                 model = new ManufacturerViewModel
                    {
                        SelectedItemIds = new[] { "0" }
                    };

                var manufacturerList = new List<Manufacturer> {
                    new Manufacturer
                    {
                        ManufacturerName = "-- select --",
                        Id = 0
                    },
                    new Manufacturer
                    {
                        ManufacturerName = "ManufacturerName 1",
                        Id = 1
                    },
                    new Manufacturer
                    {
                        ManufacturerName = "ManufacturerName 2",
                        Id = 2
                    }
                };

                ViewBag.ListOfManufacturers = manufacturerList;

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View();
            }
        }

        /// <summary>
        /// GET action to add manufacturer.
        /// </summary>
        [HttpPost]
        public IActionResult SaveManufacturer(ManufacturerViewModel viewModel)
        {
            try
            {

                var model = new ManufacturerViewModel();

                var result = viewModel.SelectedItemIds;

                model = new ManufacturerViewModel
                {
                    SelectedItemIds = new[] { "2" }
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View();
            }
        }


    }
}