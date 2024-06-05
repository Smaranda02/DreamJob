using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamJob.Entities.Entities;
using DreamJob.BusinessLogic.Employers.ViewModels;

namespace DreamJob.BusinessLogic.Employers.Mappings {
    public class UpdateEmployerVMMapper : Profile {
        public UpdateEmployerVMMapper() {
            CreateMap<Employer, UpdateEmployerViewModel>()
                .ForMember(a => a.Password, a => a.MapFrom(s => s.User.UserPassword));
        }
    }
}
