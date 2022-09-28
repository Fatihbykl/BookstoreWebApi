using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookstoreWebApi.Models
{
    public class OrderModel
    {
        public ICollection<AdresModel> Adresler { get; set; }
        public Dictionary<KitapModel, int> Kitaplar { get; set; }
    }
}