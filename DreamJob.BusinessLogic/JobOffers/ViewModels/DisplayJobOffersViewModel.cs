


namespace DreamJob.BusinessLogic.JobOffers.ViewModels {
    public class DisplayJobOffersViewModel {

        public List<JobOfferViewModel> JobOffersViewModel = new();

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

    }

}

