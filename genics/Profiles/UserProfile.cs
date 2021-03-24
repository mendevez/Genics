using AutoMapper;
using genics.Dtos;
using genics.Dtos.Users;
using genics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace genics.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserReadDto>();
            CreateMap<RegisterDto, ApplicationUser>();
        }
    }
}
