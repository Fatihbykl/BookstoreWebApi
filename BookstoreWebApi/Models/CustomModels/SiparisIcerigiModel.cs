using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookstoreWebApi.Models
{
    public class SiparisIcerigiModel
    {
        public int ID { get; set; }
        public int KitapNo { get; set; }
        public string KitapBaslik { get; set; }
        public Nullable<double> KitapFiyat { get; set; }
    }
}