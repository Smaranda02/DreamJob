using AutoMapper;
using DreamJob.BusinessLogic.Users.ViewModels;
using DreamJob.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Users.Mappings
{
    public class CurrentUserMapper : Profile
    {
        public CurrentUserMapper()
        {
            CreateMap<User, CurrentUserViewModel>()
               .ForMember(a => a.Id, a => a.MapFrom(s => s.Id))
               .ForMember(a => a.Email, a => a.MapFrom(s => s.Email))
               .ForMember(a => a.IsAuthenticated, a => a.MapFrom(s => true))
               .ForMember(a => a.Role, a => a.MapFrom(s => s.RoleId))
               .ForMember(a => a.Username, a => a.MapFrom(s => s.Username));
        }
    }
}
