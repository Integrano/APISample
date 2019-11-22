using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreAPISample.API.Helpers;
using CoreAPISample.API.Models;
using CoreAPISample.API.Resources;
using CoreAPISample.Core.Models;
using CoreAPISample.Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreAPISample.API.Controllers
{
    [Authorize]
    [ApiController]
    public class MaterialsAPIController : ControllerBase
    {
        private readonly IMaterialsRepository _materialsRepository;
        private readonly IAppHelper _appHelper;
        private readonly ILogger<MaterialsAPIController> _logger;
        private readonly IMapper _iMapper;

        public MaterialsAPIController(IMaterialsRepository materialsRepository, IAppHelper appHelper, ILogger<MaterialsAPIController> logger, IMapper iMapper)
        {
            _materialsRepository = materialsRepository;
            _appHelper = appHelper;
            _logger = logger;
            _iMapper = iMapper;
        }

        /// <summary>
        /// POST action to authenticate user login
        /// </summary>
        /// <param name="model">Object of the Authenticate model</param>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromForm] AuthenticateModel model)
        {
            if (model.UserName != null)
            {
                var user = _appHelper.Authenticate(model.UserName, model.Password);

                if (user == null)
                    return BadRequest(new { message = AppResource.AuthenticationError });
                return Ok(user);
            }

            return BadRequest(new { message = AppResource.AuthenticationError });
        }

        /// <summary>
        /// POST action to add delivery details.
        /// </summary>
        [HttpPost("SaveDelivery")]
        public IActionResult SaveDelivery([FromBody]ManufacturerViewModel model)
        {
            try
            {
                var manufacturer = _iMapper.Map<ManufacturerViewModel, Manufacturer>(model);
                var savedManufacturerDetails = _materialsRepository.SaveDelivery(manufacturer, model.MaterialItems);

                return Ok(savedManufacturerDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }

        /// <summary>
        /// POST action to add serialized delivery details.
        /// </summary>
        [HttpPost("SaveDeliveryDetails")]
        public IActionResult SaveDeliveryDetails(DeliveryViewModel model)
        {
            try
            {
                var delivery = _iMapper.Map<DeliveryViewModel, Delivery>(model);
                var savedDeliveryDetails = _materialsRepository.SaveDeliveryDetails(model.MaterialItems);

                return Ok(savedDeliveryDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }

        }

    }
}