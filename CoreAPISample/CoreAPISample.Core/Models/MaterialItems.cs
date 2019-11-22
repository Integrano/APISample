using System;
using System.Collections.Generic;

namespace CoreAPISample.Core.Models
{
    public partial class MaterialItems
    {
        public MaterialItems()
        {
            DeliveryItems = new HashSet<DeliveryItems>();
            ManufacturerItems = new HashSet<ManufacturerItems>();
        }

        public int ItemId { get; set; }
        public string SerialNumber { get; set; }
        public decimal? Quantity { get; set; }
        public DateTime? ForecastDate { get; set; }

        public virtual ICollection<DeliveryItems> DeliveryItems { get; set; }
        public virtual ICollection<ManufacturerItems> ManufacturerItems { get; set; }
    }
}
