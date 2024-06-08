using AutoMapper;
using DreamJob.BusinessLogic.JobOffers.ViewModels;
using DreamJob.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.JobOffers.Mappings
{
    public class JobOfferViewModelMapper : Profile
    {
        public JobOfferViewModelMapper() 
        {
            CreateMap<JobOffer, JobOfferViewModel>()
                .ForMember(a => a.EmployerName, a => a.MapFrom(s => s.Employer.EmployerName))
                .ForMember(a => a.Description, a => a.MapFrom(s => s.JobDescription))
                .ForMember(a => a.OfficeLocation, a => a.MapFrom(s => s.Employer.OfficeLocation))
                .ForMember(a => a.EmployerLinkedin, a => a.MapFrom(s => s.Employer.EmployerLinkedin))
                ;
        }
    }
}
