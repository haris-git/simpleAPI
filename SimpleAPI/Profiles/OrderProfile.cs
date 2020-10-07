using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // Read
            CreateMap<Entities.Order, Models.OrderDto>();

            // Create
            CreateMap<Models.OrderForCreationDto, Entities.Order>();

            // Update (PUT)
            CreateMap<Models.OrderForUpdateDto, Entities.Order>();

            // Update (PATCH)
            CreateMap<Entities.Order, Models.OrderForUpdateDto>();
            
            /*
             * The following line is equivalent to both of the previous 2 'CreateMap' commands.
             * CreateMap<Models.OrderForUpdateDto, Entities.Order>().ReverseMap();
            */
        }
    }
}
