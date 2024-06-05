using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamJob.BusinessLogic.CareerFields.ViewModels;
using DreamJob.BusinessLogic.Employers.ViewModels;

namespace DreamJob.BusinessLogic.Employers.ViewModels {
    public class UpdateEmployerViewModel {
        public int Id { get; set; }
        public string EmployerName { get; set; } = null!;

        public string OfficeLocation { get; set; } = null!;

        public string EmployerDescription { get; set; } = null!;

        public string EmployerLinkedin { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string Password { get; set; } = null!;

        public List<CareerFieldViewModel> CareerFields { get; set; } = new List<CareerFieldViewModel>();
    }
}
