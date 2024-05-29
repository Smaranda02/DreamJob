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
    public class CandidateMapper : Profile
    {
        public CandidateMapper()
        {
            CreateMap<UpdateCandidateViewModel, Candidate>()
                
               ;
        }
    }
}
