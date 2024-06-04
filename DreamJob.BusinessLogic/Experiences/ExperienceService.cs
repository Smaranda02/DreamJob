using AutoMapper;
using DreamJob.BusinessLogic.Experiences.ViewModels;
using DreamJob.DataAccess.EntityFramework;
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
        private readonly DreamJobContext _context;

        public ExperienceService(IMapper mapper, DreamJobContext context) 
        {
            _mapper = mapper;
            _context = context;
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

        public List<Experience> GetCurrentExperiences(int candidateId)
        {
            return _context.Experiences.Where(e => e.CandidateId == candidateId).ToList();  
        }


        public List<ExperienceViewModel> GetCandidateExperiences(int candidateId)
        {
            var experiences = _context.Experiences
                                      .Where(e => e.CandidateId == candidateId)
                                      .ToList();

            var experienceList = new List<ExperienceViewModel>();
            foreach(var experience in experiences)
            {
                var experienceViewModel = _mapper.Map<Experience, ExperienceViewModel>(experience);
                experienceList.Add(experienceViewModel);
            }

            return experienceList;
        }

    }
}
