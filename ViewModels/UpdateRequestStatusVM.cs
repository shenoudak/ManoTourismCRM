namespace ManoTourism.ViewModels
{
    public class UpdateRequestStatusVM
    {
        
        public int VisaRequestId { get; set; }
        public int VisaRequestStatusId { get; set; }
        public string RejectReason { get; set; }
        public int? RejectReasonId { get; set; }

    }
}
