using AutoMapper;
using DreamJob.BusinessLogic.Employers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamJob.Entities.Entities;

namespace DreamJob.BusinessLogic.Employers.Mappings {
    public class EmployerMapper : Profile {
        public EmployerMapper() {
            CreateMap<UpdateEmployerViewModel, Employer>();
        }
    }
}
