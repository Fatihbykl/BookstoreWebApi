using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookstoreWebApi.Models
{
    public class SiparisModel
    {
        public int SiparisNo { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public Nullable<double> siparisTutari { get; set; }
        public MusteriModel SiparisVeren { get; set; }
        public AdresModel TeslimatAdresi { get; set; }
        public ICollection<SiparisIcerigiModel> SiparisIcerigi { get; set; }

    }
}