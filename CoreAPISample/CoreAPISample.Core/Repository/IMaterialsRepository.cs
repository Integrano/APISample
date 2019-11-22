using CoreAPISample.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreAPISample.Core.Repository
{
    public interface IMaterialsRepository
    {

        /// <summary>
        /// Saves specified delivery details.
        /// </summary>
        /// <param name="manufacturer">Object of the manufacturer</param>
        /// <param name="materialItems">List of the material items</param>
        /// <returns>Manufacturer</returns>
        Manufacturer SaveDelivery(Manufacturer manufacturer, List<MaterialItems> materialItems);

        /// <summary>
        /// Saves specified delivery details.
        /// </summary>
        /// <param name="materialItems">List of the material items</param>
        /// <returns>Delivery</returns>
        Delivery SaveDeliveryDetails(List<MaterialItems> materialItems);

        /// <summary>
        /// Returns list of manufacturers from DB.
        /// </summary>
        /// <returns>Manufacturers</returns>
        List<Manufacturer> GetManufacturers();
    }
}
