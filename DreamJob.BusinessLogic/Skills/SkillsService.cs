using AutoMapper;
using DreamJob.BusinessLogic.Candidates.ViewModels;
using DreamJob.BusinessLogic.Users;
using DreamJob.DataAccess.EntityFramework;
using DreamJob.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Skills
{
    public class SkillsService
    {

        private DreamJobContext _context;
        private UserService _userService;
        private IMapper _mapper;

        public SkillsService(DreamJobContext context, UserService userService, IMapper mapper)
        {
            _context= context;
            _userService = userService;
            _mapper = mapper;
        }

        public List<SelectListItem> GetDefaultSkills()
        {
            return _context.CandidateSkills
                .Include(s => s.Skill)
                .Where(s => s.CandidateId == null)
                .Select(s => new SelectListItem
                {
                    Text = s.Skill.SkillName,
                    Value = s.SkillId.ToString(),
                })
                .ToList();
        }


        public List<CandidateSkill> CreateCandidateSkill(List<int> skills, Candidate candidate)
        {
            var candidateSkills = new List<CandidateSkill>();
            foreach(var skill in skills)
            {
                var candidateSkillVM = new CandidateSkill
                {
                    SkillId = skill,
                    CandidateId = candidate.Id,
                    Candidate = candidate
                };
                candidateSkills.Add(candidateSkillVM);
            }

            return candidateSkills;
        }

    }
}
