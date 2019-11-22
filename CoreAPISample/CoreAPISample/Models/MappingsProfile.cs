using AutoMapper;
using CoreAPISample.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPISample.API.Models
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            //-------------- Delivery -------------- 
            CreateMap<DeliveryItemsViewModel, DeliveryItems>();
            CreateMap<DeliveryViewModel, Delivery>();

            //-------------- Manufacturer -------------- 
            CreateMap<ManufacturerViewModel, Manufacturer>();
        }
    }
}
