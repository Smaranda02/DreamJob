using AutoMapper;
using DreamJob.BusinessLogic.Experiences.ViewModels;
using DreamJob.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Experiences.Mappings
{
    public class ExperienceMapper : Profile
    {
        public ExperienceMapper()
        {
            CreateMap<ExperienceViewModel, Experience>()
                ;
        }
    }
}
