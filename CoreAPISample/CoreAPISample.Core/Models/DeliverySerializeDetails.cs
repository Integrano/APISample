using System;
using System.Collections.Generic;

namespace CoreAPISample.Core.Models
{
    public partial class DeliverySerializeDetails
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public int DeliveryId { get; set; }
        public int ItemId { get; set; }
        public decimal? Qty { get; set; }
        public DateTime? Date { get; set; }

        public virtual Delivery Delivery { get; set; }
        public virtual Items Item { get; set; }
    }
}
