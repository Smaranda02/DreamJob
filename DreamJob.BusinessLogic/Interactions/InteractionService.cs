using AutoMapper;
using DreamJob.BusinessLogic.Interactions.ViewModels;
using DreamJob.BusinessLogic.Users;
using DreamJob.DataAccess.EntityFramework;
using DreamJob.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Interactions
{
    public class InteractionService
    {
        private readonly DreamJobContext _context;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public InteractionService(DreamJobContext context, IMapper mapper, UserService userService) 
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        public void UpdateCandidateInteraction(InteractionViewModel interaction, bool fromEmployer)
        {
            var currentUserId = _userService.GetCurrentUser().Id;
            var employerId = 0;
            var candidateId = 0;

            var realInteraction = new Interaction();
            if(fromEmployer)
            {
                employerId = _context.Employers.Where(e => e.UserId == currentUserId).Select(e => e.Id).FirstOrDefault();
                realInteraction = _context.Interactions
                                           .Include(i => i.JobOffer)
                                           .Where(i => i.JobOffer.EmployerId == currentUserId && i.JobOfferId == interaction.JobOfferId)
                                           .FirstOrDefault();
            }
            else
            {
                candidateId = _context.Candidates.Where(c => c.UserId == currentUserId).Select(c => c.Id).FirstOrDefault();
                realInteraction = _context.Interactions.Where(i => i.CandidateId == candidateId
                                                       && i.JobOfferId == interaction.JobOfferId).FirstOrDefault();
            }
           

            if (realInteraction != null)
            {
                if(fromEmployer)
                {
                    realInteraction.FeedbackEmployer = true;
                }
                else 
                {
                    realInteraction.FeedbackCandidate = true;
                }

                _context.Interactions.Update(realInteraction);
            }
            else
            {
                if(fromEmployer == true)
                {
                    interaction.EmployerId = employerId;
                }
                else
                {
                    interaction.CandidateId = candidateId;
                }

                var newInteraction = _mapper.Map<InteractionViewModel, Interaction>(interaction);
                _context.Interactions.Add(newInteraction);
            }

            _context.SaveChanges();
        }
    }
}
