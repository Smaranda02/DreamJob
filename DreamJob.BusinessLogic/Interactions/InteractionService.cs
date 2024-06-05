using AutoMapper;
using DreamJob.BusinessLogic.Interactions.ViewModels;
using DreamJob.BusinessLogic.Users;
using DreamJob.Common.Enums;
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

        public void UpdateInteraction(InteractionViewModel interaction, bool fromEmployer)
        {
            var currentUserId = _userService.GetCurrentUser().Id;
            var employerId = 0;
            var candidateId = 0;

            var realInteraction = new Interaction();
            if(fromEmployer)
            {
                employerId = _userService.GetCurrentEmployerId();

                realInteraction = _context.Interactions
                                           .Include(i => i.JobOffer)
                                           .Where(i => i.JobOffer.EmployerId == employerId && i.JobOfferId == interaction.JobOfferId)
                                           .FirstOrDefault();

                realInteraction.FeedbackEmployer = true;
                _context.Interactions.Update(realInteraction);

            }

            else
            {
                candidateId = _userService.GetCurrentCandidateId();
                realInteraction = _context.Interactions.Where(i => i.CandidateId == candidateId
                                                       && i.JobOfferId == interaction.JobOfferId).FirstOrDefault();
                if(realInteraction == null)
                {
                    interaction.CandidateId = candidateId;
                    var newInteraction = _mapper.Map<InteractionViewModel, Interaction>(interaction);
                    _context.Interactions.Add(newInteraction);
                }

                else
                {
                    //adaug logica de dat dislike
                }

            }
            
            _context.SaveChanges();
        }

        public List<MatchViewModel> GetMatches()
        {
            var matchesList = new List<MatchViewModel>();

            var candidateId = 0;
            var employerId = 0;
            var matches = new List<Interaction>();
            

            if(_userService.GetCurrentUser().Role == (int)Roles.Employer)
            {
                employerId = _userService.GetCurrentEmployerId();
                matches = _context.Interactions
                                 .Include(i => i.Candidate)
                                 .Include(i => i.JobOffer)
                                 .ThenInclude(i => i.Employer)
                                 .Where(i => i.FeedbackCandidate == true && i.FeedbackEmployer == true && i.JobOffer.EmployerId == employerId)
                                 .ToList();
            }

            else
            {
               candidateId =  _userService.GetCurrentCandidateId();
               matches = _context.Interactions
                                 .Include(i => i.Candidate)
                                 .Include(i => i.JobOffer)
                                 .ThenInclude(i => i.Employer)
                                 .Where(i => i.FeedbackCandidate == true && i.FeedbackEmployer == true && i.CandidateId == candidateId)
                                 .ToList();
            }
            

            foreach(var match in matches)
            {
                var matchViewModel = _mapper.Map<Interaction, MatchViewModel>(match);
                //var matchViewModel = new MatchViewModel { 
                //    FirstName = match.Candidate.FirstName,
                //    Surname= match.Candidate.Surname,
                //    EmployerName = match.JobOffer.Employer.EmployerName,
                //    EmployerId = match.JobOffer.EmployerId,
                //    JobDescription = match.JobOffer.JobDescription,
                //    CandidateId = match.Candidate.Id
                //};

                matchesList.Add(matchViewModel);
            }

            return matchesList;
        }
    }
}
