using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookstoreWebApi.Models
{
    public class MusteriModel
    {
        [Required]
        public int MusteriNo { get; set; }
        [Required]
        [MaxLength(50)]
        public string MusteriAd { get; set; }
        [Required]
        [MaxLength(50)]
        public string MusteriSoyad { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(15)]
        public string Rol { get; set; }
        [Required]
        [MaxLength(50)]
        public string Sifre { get; set; }

        public ICollection<AdresModel> Adresler;
        public ICollection<SiparisModel> Siparisler;
    }
}