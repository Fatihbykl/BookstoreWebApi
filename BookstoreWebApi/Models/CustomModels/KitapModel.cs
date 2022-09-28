using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookstoreWebApi.Models
{
    public class KitapModel
    {
        public int KitapNo { get; set; }
        [Required]
        [MaxLength(50)]
        public string Baslik { get; set; }
        public Nullable<int> SayfaSayisi { get; set; }
        [MaxLength(25)]
        public string BasimTarihi { get; set; }
        [MaxLength(25)]
        public string Tur { get; set; }
        public Nullable<double> Fiyat { get; set; }
        public Nullable<int> StokSayisi { get; set; }
        public int YazarNo { get; set; }
        [Required]
        [MaxLength(255)]
        public string gorselURL { get; set; }

        public YazarModel Yazar { get; set; }

        public override int GetHashCode()
        {
            return KitapNo;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as KitapModel);
        }

        public bool Equals(KitapModel obj)
        {
            return obj != null && obj.KitapNo == this.KitapNo;
        }
    }
}