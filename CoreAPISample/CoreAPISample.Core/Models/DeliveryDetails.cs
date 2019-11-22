using System;
using System.Collections.Generic;

namespace CoreAPISample.Core.Models
{
    public partial class DeliveryDetails
    {
        public int DeliveryDetailsId { get; set; }
        public int DeliveryId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int ItemId { get; set; }
        public decimal? Quantity { get; set; }

        public virtual Delivery Delivery { get; set; }
        public virtual Items Item { get; set; }
    }
}
