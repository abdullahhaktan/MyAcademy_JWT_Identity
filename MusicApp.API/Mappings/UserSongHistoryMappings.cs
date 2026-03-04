using AutoMapper;
using MusicApp.API.Data.Entities;
using MusicApp.API.DTOs.UserSongHistoryDtos;

namespace MusicApp.API.Mappings
{
    public class UserSongHistoryMappings : Profile
    {
        public UserSongHistoryMappings()
        {
            CreateMap<CreateUserSongHistoryDto, UserSongHistory>();
        }
    }
}
