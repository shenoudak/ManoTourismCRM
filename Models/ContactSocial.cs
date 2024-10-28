using System.ComponentModel.DataAnnotations;
namespace ManoTourism.Models
{
    public class ContactSocial
    {
        public int ContactSocialId { get; set; }
        public string ContactUsImage { get; set; }
        public string FirstPhoneNumber { get; set; }
        public string SecondPhoneNumber { get; set; }
        public string FirstEmail { get; set; }
        public string SecondEmail { get; set; }
        public string LocationTitleAr { get; set; }
        public string LocationTitleEn { get; set; }
        public string Facebook { get; set; }
        public string WhatsApp { get; set; }
        public string Instagram { get; set; }
        public string Twiter { get; set; }
        public string Youtube { get; set; }
        public string Telegram { get; set; }
        public string OpentingTimeAr { get; set; }
        public string OpentingTimeEn { get; set; }
    }
}
