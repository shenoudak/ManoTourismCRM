using System.ComponentModel.DataAnnotations;
namespace ManoTourism.Models
{
    public class CompleteTransaction
    {
        [Key]
        public int CompleteTransactionId { get; set; }
        public string TransactionTitleAr { get; set; }
        public string TransactionTitleEn { get; set; }
        public string TransactionDescEn { get; set; }
        public string TransactionDescAr { get; set; }
        public string Pic { get; set; }
    }
}
