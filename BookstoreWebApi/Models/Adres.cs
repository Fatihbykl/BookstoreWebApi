//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookstoreWebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Adres
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Adres()
        {
            this.Siparis = new HashSet<Siparis>();
        }
    
        public int adresNo { get; set; }
        public int musteriNo { get; set; }
        public string il { get; set; }
        public string ilce { get; set; }
        public string sokak { get; set; }
        public string mahalle { get; set; }
        public int no { get; set; }
    
        public virtual Musteri Musteri { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparis> Siparis { get; set; }
    }
}
