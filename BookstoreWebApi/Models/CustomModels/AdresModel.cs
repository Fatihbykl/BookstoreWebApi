using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookstoreWebApi.Models
{
    public class AdresModel
    {
        public int AdresNo { get; set; }
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
        public int MusteriNo { get; set; }
    }
}