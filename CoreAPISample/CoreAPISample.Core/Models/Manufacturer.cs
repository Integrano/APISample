using System;
using System.Collections.Generic;

namespace CoreAPISample.Core.Models
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Items = new HashSet<Items>();
        }

        public int Id { get; set; }
        public string ManufacturerName { get; set; }
        public DateTime? DeliveryReceivedDate { get; set; }

        public virtual ICollection<Items> Items { get; set; }
    }
}
