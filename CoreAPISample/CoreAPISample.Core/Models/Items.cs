using System;
using System.Collections.Generic;

namespace CoreAPISample.Core.Models
{
    public partial class Items
    {
        public Items()
        {
            DeliveryDetails = new HashSet<DeliveryDetails>();
        }

        public int ItemId { get; set; }
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<DeliveryDetails> DeliveryDetails { get; set; }
        public virtual ICollection<DeliverySerializeDetails> DeliverySerializeDetails { get; set; }

    }
}
