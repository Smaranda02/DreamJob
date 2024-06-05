using DreamJob.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DreamJob.BusinessLogic.Users;
using DreamJob.Entities.Entities;
using DreamJob.BusinessLogic.CareerFields.ViewModels;
using DreamJob.BusinessLogic.CareerFields;
using Microsoft.EntityFrameworkCore.Update.Internal;


namespace DreamJob.BusinessLogic.CareerFields {
    public class CareerFieldsService {

        private DreamJobContext _context;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public CareerFieldsService(DreamJobContext dreamJobContext, IMapper mapper, UserService userService) {
            _context = dreamJobContext;
            _mapper = mapper;
            _userService = userService;
        }

        public List<CareerField> CreateNewCareerFields(List<CareerFieldViewModel> careerFields, int candidateId) {
            var newCareerFields = new List<CareerField>();

            foreach (var careerField in careerFields) {
                var newCareerField = _mapper.Map<CareerFieldViewModel, CareerField>(careerField);
                newCareerField.Id = candidateId;
                newCareerFields.Add(newCareerField);
            }

            return newCareerFields;
        }

        public List<CareerField> GetCurrentCareerFields(int candidateId) {
            var oldCareerFields = _context.CareerFields
                                    .Where(s => s.Id == candidateId)
                                    .ToList();
            return oldCareerFields;
        }

        public List<CareerFieldViewModel> GetCandidateCareerFields(int candidateId) {
            var careerFields = _context.CareerFields
                                    .Where(s => s.Id == candidateId)
                                    .ToList();
            var careerFieldListVM = new List<CareerFieldViewModel>();
            foreach (var careerField in careerFields) {
                var careerFieldVM = _mapper.Map<CareerField, CareerFieldViewModel>(careerField);
                careerFieldListVM.Add(careerFieldVM);
            }

            return careerFieldListVM;
        }

        public List<CareerFieldViewModel> GetEmployerCareerFields(int employerId) {
            var careerFields = _context.CareerFields
                                    .Where(s => s.Id == employerId)
                                    .ToList();
            var careerFieldListVM = new List<CareerFieldViewModel>();
            foreach (var careerField in careerFields) {
                var careerFieldVM = _mapper.Map<CareerField, CareerFieldViewModel>(careerField);
                careerFieldListVM.Add(careerFieldVM);
            }

            return careerFieldListVM;

        }
            

        public CareerFieldViewModel GetCareerFieldById(int id) {
            var careerField = _context.CareerFields
                                    .FirstOrDefault(s => s.Id == id);
            var careerFieldVM = _mapper.Map<CareerField, CareerFieldViewModel>(careerField);
            return careerFieldVM;
        }

        
    }
}
