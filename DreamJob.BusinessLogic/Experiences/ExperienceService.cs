using AutoMapper;
using DreamJob.BusinessLogic.Experiences.ViewModels;
using DreamJob.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Experiences
{
    public class ExperienceService
    {
        private IMapper _mapper;
        public ExperienceService(IMapper mapper) 
        {
            _mapper = mapper;
        }

        public List<Experience> CreateExperinces(List<ExperienceViewModel> experiences, int candidateId)
        {
            var newExperiences = new List<Experience>();
            foreach(var experience in experiences)
            {
                var newExperience =  _mapper.Map<ExperienceViewModel, Experience>(experience);
                newExperience.CandidateId= candidateId;
                newExperiences.Add(newExperience);
            }

            return newExperiences;
        }

    }
}
