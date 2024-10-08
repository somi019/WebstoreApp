using AutoMapper;
using IdentityServer.DTOs;
using IdentityServer.Entities;

namespace IdentityServer.Mapper
{
    public class IdentityProfile : Profile
    {

        public IdentityProfile() 
        {
            CreateMap<NewUserDTO, User>().ReverseMap();
            CreateMap<User, UserDetails>().ReverseMap();
        
        }
    }
}
