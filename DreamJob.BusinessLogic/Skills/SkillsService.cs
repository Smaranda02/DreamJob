using DreamJob.DataAccess.EntityFramework;
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

        public SkillsService(DreamJobContext context)
        {
            _context= context;
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
    }
}
