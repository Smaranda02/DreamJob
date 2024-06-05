using AutoMapper;
using DreamJob.BusinessLogic.Interactions.ViewModels;
using DreamJob.BusinessLogic.Users;
using DreamJob.DataAccess.EntityFramework;
using DreamJob.Entities.Entities;
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

        public void UpdateCandidateInteraction(InteractionViewModel interaction)
        {
            var currentUserId = _userService.GetCurrentUser().Id;
            var realInteraction = _context.Interactions.Where(i => i.CandidateId == currentUserId
                                                        && i.JobOfferId == interaction.JobOfferId).FirstOrDefault();

            if (realInteraction != null)
            {
                realInteraction.FeedbackCandidate= true;
                _context.Interactions.Update(realInteraction);
            }
            else
            {
                interaction.CandidateId = currentUserId;
                var newInteraction = _mapper.Map<InteractionViewModel, Interaction>(interaction);
                _context.Interactions.Add(newInteraction);
            }

            _context.SaveChanges();
        }
    }
}
