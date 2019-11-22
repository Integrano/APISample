using CoreAPISample.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPISample.API.Models
{
    public class ManufacturerViewModel
    {
        public int Id { get; set; }
        public string ManufacturerName { get; set; }
        public DateTime? DeliveryReceivedDate { get; set; }
        public List<MaterialItems> MaterialItems { get; set; }
        public virtual ICollection<ManufacturerItems> ManufacturerItems { get; set; }

        public List<MaterialItems> MaterialItemss { get; set; }

        public List<SelectListItem> MaterialItemSelect { get; set; }

        public IEnumerable<string> SelectedItemIds { get; set; }
        public IEnumerable<MaterialItems> MaterialItemsList
        {
            get
            {
                return new[]
                {
                            new MaterialItems { ItemId = 0, SerialNumber = "-- Select --" }
                };
            }
        }

        public string Token { get; set; }
    }
}
