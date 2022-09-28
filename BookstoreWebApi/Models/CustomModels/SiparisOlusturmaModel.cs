using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookstoreWebApi.Models
{
    public class SiparisOlusturmaModel
    {
        public int SiparisNo { get; set; }
        public DateTime SiparisTarihi { get; set; }
        [Required]
        public int SiparisVeren { get; set; }
        [Required]
        public int TeslimatAdresi { get; set; }
        [Required]
        public ICollection<SiparisIcerigiModel> SiparisIcerigi { get; set; }
    }
}