using System;
using System.Collections.Generic;

namespace CoreAPISample.Core.Models
{
    public partial class DeliveryItems
    {
        public int Id { get; set; }
        public int DeliveryId { get; set; }
        public int MaterialItemId { get; set; }

        public virtual MaterialItems MaterialItem { get; set; }
    }
}
