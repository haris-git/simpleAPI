using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            // Read
            CreateMap<Entities.Client, Models.ClientDto>().
                ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            // Create
            CreateMap<Models.ClientForCreationDto, Entities.Client>();

            // Update
            CreateMap<Models.ClientForUpdateDto, Entities.Client>()
                .ReverseMap();  // See the comments in OrderProfile for more information about this method.
        }
    }
}
