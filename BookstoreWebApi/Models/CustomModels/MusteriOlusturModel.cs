using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookstoreWebApi.Models
{
    public class MusteriOlusturModel
    {
        [Required]
        [MaxLength(50)]
        public string MusteriAd { get; set; }
        [Required]
        [MaxLength(50)]
        public string MusteriSoyad { get; set; }
        [Required]
        [MaxLength(50)]
        public string MusteriSifre { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(15)]
        public string Rol { get; set; }
        [Required]
        [MaxLength(20)]
        public string Il { get; set; }
        [Required]
        [MaxLength(20)]
        public string Ilce { get; set; }
        [Required]
        [MaxLength(50)]
        public string Sokak { get; set; }
        [Required]
        [MaxLength(50)]
        public string Mahalle { get; set; }
        [Required]
        public int No { get; set; }
    }
}