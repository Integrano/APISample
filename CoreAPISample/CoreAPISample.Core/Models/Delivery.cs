using System;
using System.Collections.Generic;

namespace CoreAPISample.Core.Models
{
    public partial class Delivery
    {
        public Delivery()
        {
            DeliveryDetails = new HashSet<DeliveryDetails>();
        }

        public int DeliveryId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal? Quantity { get; set; }

        public virtual ICollection<DeliveryDetails> DeliveryDetails { get; set; }

        public virtual ICollection<DeliverySerializeDetails> DeliverySerializeDetails { get; set; }
    }
}
