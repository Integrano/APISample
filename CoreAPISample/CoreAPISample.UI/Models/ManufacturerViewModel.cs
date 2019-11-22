using CoreAPISample.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPISample.UI.Models
{
    public class ManufacturerViewModel
    {
        public int Id { get; set; }
        public string ManufacturerName { get; set; }
        public DateTime? DeliveryReceivedDate { get; set; }
        public List<MaterialItems> MaterialItemss { get; set; }

        public List<SelectListItem> MaterialItemSelect { get; set; }
        public virtual ICollection<ManufacturerItems> ManufacturerItems { get; set; }

        public IEnumerable<string> SelectedItemIds { get; set; }
        public IEnumerable<MaterialItems> MaterialItems
        {
            get
            {
                return new[]
                {
                            new MaterialItems { ItemId = 0, SerialNumber = "-- Select --" }
                            //,new MaterialItems { ItemId = 1, SerialNumber = "SerialNumber 1" },
                            //new MaterialItems { ItemId = 2, SerialNumber = "SerialNumber 2" },
                            //new MaterialItems { ItemId = 3, SerialNumber = "SerialNumber 3" },
                            };
            }
        }
    }
}
