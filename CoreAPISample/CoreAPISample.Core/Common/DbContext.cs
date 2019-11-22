using CoreAPISample.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreAPISample.Core.Common
{
    public class DbContext : IDbContext
    {
        private readonly CoreAPISampleDBContext _coreAPISampleDBContext = new CoreAPISampleDBContext();

        public void Dispose()
        {
            _coreAPISampleDBContext.Dispose();
        }

        /// <summary>
        /// Saves all changes made in the relevant contexts to the database.
        /// </summary>
        public void SaveChanges()
        {
            _coreAPISampleDBContext.SaveChanges();
        }

        /// <summary>
        /// Saves specified delivery details.
        /// </summary>
        /// <param name="delivery">Object of the delivery to be saved</param>
        /// <returns>Delivery</returns>
        public Delivery SaveDeliveryDetails(Delivery delivery)
        {
            _coreAPISampleDBContext.Delivery.Add(delivery);
            return delivery;
        }

        /// <summary>
        /// Saves specified delivery details.
        /// </summary>
        /// <param name="deliveryDetails">Object of the delivery details to be saved</param>
        /// <returns>Delivery</returns>
        public DeliveryDetails SaveDeliveryDetails(DeliveryDetails deliveryDetails)
        {
            _coreAPISampleDBContext.DeliveryDetails.Add(deliveryDetails);
            return deliveryDetails;
        }

        /// <summary>
        /// Saves specified delivery details.
        /// </summary>
        /// <param name="deliveryDetails">Object of the delivery serialize details to be saved</param>
        /// <returns>DeliverySerializeDetails</returns>
        public DeliverySerializeDetails SaveDeliverySerializeDetails(DeliverySerializeDetails deliveryDetails)
        {
            _coreAPISampleDBContext.DeliverySerializeDetails.Add(deliveryDetails);
            return deliveryDetails;
        }

        /// <summary>
        /// Returns list of manufacturers from DB.
        /// </summary>
        /// <returns>Manufacturers</returns>
        public List<Manufacturer> GetManufacturers()
        {
            var manufacturers = _coreAPISampleDBContext.Manufacturer.ToList();
            return manufacturers;
        }

    }

}
