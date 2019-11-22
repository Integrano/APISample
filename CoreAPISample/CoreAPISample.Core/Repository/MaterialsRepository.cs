using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CoreAPISample.Core.Common;
using CoreAPISample.Core.Logging;
using CoreAPISample.Core.Models;
using Microsoft.Extensions.Logging;

namespace CoreAPISample.Core.Repository
{
    public class MaterialsRepository : IMaterialsRepository
    {
        private readonly ILogger _logger;
        private readonly IMethodLoggerFactory _loggerFactory;
        private readonly IDbContextFactory _dbContextFactory;
        public MaterialsRepository(IDbContextFactory dbContextFactory, ILogger<MaterialsRepository> logger, IMethodLoggerFactory loggerFactory)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
            _loggerFactory = loggerFactory;
        }


        /// <summary>
        /// Saves specified delivery details.
        /// </summary>
        /// <param name="manufacturer">Object of the manufacturer</param>
        /// <param name="materialItems">List of the Material Items</param>
        /// <returns>Manufacturer</returns>
        public Manufacturer SaveDelivery(Manufacturer manufacturer, List<MaterialItems> materialItems)
        {
            using (_loggerFactory.CreateMethodLogger(MethodBase.GetCurrentMethod(), _logger))
            {
                using (var context = _dbContextFactory.CreateDbContext())
                {
                    if (manufacturer != null)
                    {
                        var delivery = new Delivery()
                        {
                            DeliveryDate = DateTime.Now
                        };
                        context.SaveDeliveryDetails(delivery);

                        if (delivery != null)
                        {
                            var itemDetails = materialItems?.GroupBy(x => x.ItemId)
                           .Select(item =>
                           {
                               var returnQTY = item.First();
                               returnQTY.Quantity = item.Sum(xt => xt.Quantity);
                               return returnQTY;
                           }).ToList();

                            itemDetails?.ForEach(item =>
                            {
                                var deliveryDetails = new DeliveryDetails()
                                {
                                    DeliveryId = delivery.DeliveryId,
                                    DeliveryDate = DateTime.Now,
                                    ItemId = item.ItemId,
                                    Quantity = item.Quantity
                                };

                                context.SaveDeliveryDetails(deliveryDetails);

                            });
                        }

                        context.SaveChanges();
                    }

                    return manufacturer;
                }
            }
        }

        /// <summary>
        /// Saves specified Delivery details.
        /// </summary>
        /// <param name="materialItems">Object of the material items</param>
        /// <returns>Delivery</returns>
        public Delivery SaveDeliveryDetails(List<MaterialItems> materialItems)
        {
            using (_loggerFactory.CreateMethodLogger(MethodBase.GetCurrentMethod(), _logger))
            {
                using (var context = _dbContextFactory.CreateDbContext())
                {
                    var delivery = new Delivery()
                    {
                        DeliveryDate = DateTime.Now
                    };
                    context.SaveDeliveryDetails(delivery);

                    if (delivery != null)
                    {
                        materialItems?.ForEach(item =>
                        {
                            var deliveryDetails = new DeliverySerializeDetails()
                            {
                                Date = item.ForecastDate,
                                DeliveryId = delivery.DeliveryId,
                                ItemId = item.ItemId,
                                Qty = item.Quantity,
                                SerialNumber = item.SerialNumber
                            };

                            context.SaveDeliverySerializeDetails(deliveryDetails);

                        });
                    }

                    context.SaveChanges();
                    return delivery;
                }
            }
        }

        /// <summary>
        /// Returns list of manufacturers from DB.
        /// </summary>
        /// <returns>Manufacturers</returns>
        public List<Manufacturer> GetManufacturers()
        {
            using (_loggerFactory.CreateMethodLogger(MethodBase.GetCurrentMethod(), _logger))
            {
                using (var context = _dbContextFactory.CreateDbContext())
                {
                    var manufacturers = context.GetManufacturers();
                    return manufacturers;
                }
            }
        }
    }
}
