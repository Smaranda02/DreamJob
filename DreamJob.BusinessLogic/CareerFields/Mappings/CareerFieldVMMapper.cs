using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamJob.BusinessLogic.CareerFields.ViewModels;
using DreamJob.Entities.Entities;


namespace DreamJob.BusinessLogic.CareerFields.Mappings {
    public class  CareerFieldVMMapper : Profile 
    {
        public CareerFieldVMMapper() {
            CreateMap<CareerField, CareerFieldViewModel>()
                ;
        }
    }

}
