using AutoMapper;
using DreamJob.BusinessLogic.Studies.ViewModels;
using DreamJob.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Studies.Mappings
{
    public class StudyMapper : Profile
    {
        public StudyMapper()
        {
            CreateMap<StudyViewModel, Study>()
                ;
        }
    }
}
