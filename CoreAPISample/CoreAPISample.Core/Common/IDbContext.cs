using CoreAPISample.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreAPISample.Core.Common
{
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// Saves all changes made in the relevant contexts to the database.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Saves specified delivery details.
        /// </summary>
        /// <param name="delivery">Object of the delivery to be saved</param>
        /// <returns>Delivery</returns>
        Delivery SaveDeliveryDetails(Delivery delivery);

        /// <summary>
        /// Saves specified delivery details.
        /// </summary>
        /// <param name="deliveryDetails">List of the delivery details to be saved</param>
        /// <returns>Delivery Details</returns>
        DeliveryDetails SaveDeliveryDetails(DeliveryDetails deliveryDetails);

        /// <summary>
        /// Saves specified delivery serialized details.
        /// </summary>
        /// <param name="deliveryDetails">List of the delivery serialized details to be saved</param>
        /// <returns>Delivery Serialize Details</returns>
        DeliverySerializeDetails SaveDeliverySerializeDetails(DeliverySerializeDetails deliveryDetails);

        /// <summary>
        /// Returns list of manufacturers from DB.
        /// </summary>
        /// <returns>Manufacturers</returns>
        List<Manufacturer> GetManufacturers();
    }
}
