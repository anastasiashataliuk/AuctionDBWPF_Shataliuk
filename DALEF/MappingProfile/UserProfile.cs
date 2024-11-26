using AutoMapper;
using DALEF.Models;
using DTO;

namespace DALEF.MappingProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Map from TblUser (EF Model) to User (DTO)
            CreateMap<TblUser, User>();
            // Map from User (DTO) to TblUser (EF Model)
            CreateMap<User, TblUser>();
        }
    }
}
