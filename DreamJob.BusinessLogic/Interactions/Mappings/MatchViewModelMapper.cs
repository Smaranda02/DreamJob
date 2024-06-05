using AutoMapper;
using DreamJob.BusinessLogic.Interactions.ViewModels;
using DreamJob.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Interactions.Mappings
{
    public class MatchViewModelMapper : Profile   
    {
        public MatchViewModelMapper()
        {
            CreateMap<Interaction, MatchViewModel>()
               .ForMember(a => a.FirstName , a => a.MapFrom(s => s.Candidate.FirstName))
               .ForMember(a => a.Surname, a => a.MapFrom(s => s.Candidate.Surname))
               .ForMember(a => a.EmployerName, a => a.MapFrom(s => s.JobOffer.Employer.EmployerName))
               .ForMember(a => a.EmployerId, a => a.MapFrom(s => s.JobOffer.EmployerId))
               .ForMember(a => a.JobDescription, a => a.MapFrom(s => s.JobOffer.JobDescription))
                ;

        }
    }
}
