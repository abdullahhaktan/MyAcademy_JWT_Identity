using AutoMapper;
using MusicApp.API.Data.Entities;
using MusicApp.API.DTOs.UserDtos;

namespace MusicApp.API.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<RegisterDto, AppUser>();
        }
    }
}
