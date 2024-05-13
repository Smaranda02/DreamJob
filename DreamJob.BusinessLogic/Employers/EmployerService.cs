using DreamJob.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamJob.Entities.Entities;
using DreamJob.BusinessLogic.Employer.ViewModels;


namespace DreamJob.BusinessLogic.Employers
{
    public class EmployerService
    {
        private readonly DreamJobContext _context;
        public EmployerService(DreamJobContext context) 
        {
            _context = context;
        }

        public RegisterViewModel CreateRegisterVM()
        {
            var model = new RegisterViewModel();
            return model;
        }

        public void Register(RegisterViewModel model)
        {
            var employer = new Entities.Entities.Employer
            {
                EmployerName = model.EmployerName,
                OfficeLocation = model.OfficeLocation,
                EmployerLinkedin = model.EmployerLinkedin,
                EmployerDescription = model.EmployerDescription
            };

            _context.Employers.Add(employer);
            _context.SaveChanges();
        }
    }
}
