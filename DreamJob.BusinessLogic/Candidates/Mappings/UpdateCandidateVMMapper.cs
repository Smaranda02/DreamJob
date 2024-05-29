using AutoMapper;
using DreamJob.BusinessLogic.Candidates.ViewModels;
using DreamJob.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Candidates.Mappings
{
    public class UpdateCandidateVMMapper : Profile
    {
        public UpdateCandidateVMMapper()
        {
            CreateMap<Candidate, UpdateCandidateViewModel>()
               .ForMember(a => a.Email, a => a.MapFrom(s => s.User.Email))
               .ForMember(a => a.Password, a => a.MapFrom(s => s.User.UserPassword))
               ;
        }
    }
}
