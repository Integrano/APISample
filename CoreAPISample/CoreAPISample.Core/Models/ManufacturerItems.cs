using System;
using System.Collections.Generic;

namespace CoreAPISample.Core.Models
{
    public partial class ManufacturerItems
    {
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public int MaterialItemId { get; set; }

        public virtual MaterialItems MaterialItem { get; set; }
    }
}
