using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DreamJob.BusinessLogic.CareerFields.ViewModels;
using DreamJob.Entities.Entities;

namespace DreamJob.BusinessLogic.CareerFields.Mappings {
    public class CareerFieldMapper : Profile {
        public CareerFieldMapper() { 
            CreateMap<CareerFieldViewModel, CareerField>()
                ;
        }

    }
}
