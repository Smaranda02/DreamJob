using AutoMapper;
using DreamJob.BusinessLogic.Candidates.ViewModels;
using DreamJob.BusinessLogic.Users.ViewModels;
using DreamJob.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Users.Mappings
{
    public class UserMapper : Profile
    {
        public UserMapper() 
        {
            CreateMap<CurrentUserViewModel, User>()
                .ForMember(a => a.Id, a => a.MapFrom(s => s.Id))
               .ForMember(a => a.Email, a => a.MapFrom(s => s.Email))
               .ForMember(a => a.Username, a => a.MapFrom(s => s.Username))
               .ForMember(a => a.RoleId, a => a.MapFrom(s => s.Role))
               .ForMember(a => a.UserPassword, a => a.MapFrom(s => s.Password))
                ;
        }

    }
}
