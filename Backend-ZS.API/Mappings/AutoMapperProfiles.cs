using AutoMapper;
using Backend_ZS.API.Models.Domain;
using Backend_ZS.API.Models.DTO;

namespace Backend_ZS.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Client Mappings
            CreateMap<Client, ClientDto>();
            CreateMap<ClientRequestDto, Client>();
        }
    }
}
