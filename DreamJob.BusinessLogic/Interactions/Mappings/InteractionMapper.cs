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
    public class InteractionMapper: Profile
    {
        public InteractionMapper() 
        {
            CreateMap<InteractionViewModel, Interaction>()
                ;
        }
    }
}
