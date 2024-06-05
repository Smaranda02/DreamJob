


namespace DreamJob.BusinessLogic.JobOffers.ViewModels {
    public class DisplayJobOffersViewModel {

        public List<JobOfferViewModel> JobOffersViewModel = new();
        public int? Id { get; set; }
        public decimal? Salary { get; set; }

        public string? Description { get; set; } = null!;

        public string? Employer { get; set; } = null!;
        public string? OfficeLocation { get; set; } = null!;
        public string? EmployerLinkedin { get; set; } = null!;

    }

}

