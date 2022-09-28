using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookstoreWebApi.Models
{
    public class YazarModel
    {
        public int YazarNo { get; set; }
        [Required]
        [MaxLength(50)]
        public string YazarAdi { get; set; }

        public ICollection<KitapModel> Kitaplar;
    }
}