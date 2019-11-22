using CoreAPISample.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPISample.API.Models
{
    public class DeliveryViewModel
    {
        public int DeliveryId { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public List<MaterialItems> MaterialItems { get; set; }

        public List<DeliveryItemsViewModel> DeliveryItems { get; set; }

        public DeliveryItemsViewModel DeliveryItem { get; set; }

        public string Token { get; set; }

    }
}
