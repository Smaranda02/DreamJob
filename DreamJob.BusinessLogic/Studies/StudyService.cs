using AutoMapper;
using DreamJob.BusinessLogic.Studies.ViewModels;
using DreamJob.BusinessLogic.Users;
using DreamJob.DataAccess.EntityFramework;
using DreamJob.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Studies
{
    public class StudyService
    {
        private DreamJobContext _context;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        public StudyService(DreamJobContext dreamJobContext, IMapper mapper, UserService userService)
        { 
            _context = dreamJobContext;
            _mapper = mapper;
            _userService = userService;
        }

        public List<Study> CreateStudies(List<StudyViewModel> studies, int candidateId)
        {
            var newStudies = new List<Study>();

            foreach (var study in studies)
            {
                var newStudy = _mapper.Map<StudyViewModel, Study>(study);
                newStudy.CandidateId = candidateId;
                newStudies.Add(newStudy);
            }

            return newStudies;
        }


        public List<StudyViewModel> GetCandidateStudies(int candidateId)
        {
            var studies = _context.Studies
                                    .Where(s => s.CandidateId == candidateId)
                                    .ToList();
            var studyListVM = new List<StudyViewModel>();
            foreach (var study in studies)
            {
                var studyVM = _mapper.Map<Study, StudyViewModel>(study);
                studyListVM.Add(studyVM);
            }

            return studyListVM;
        }

    }
}
