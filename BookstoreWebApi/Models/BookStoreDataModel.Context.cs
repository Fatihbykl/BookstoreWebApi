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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BookStoreEntities : DbContext
    {
        public BookStoreEntities()
            : base("name=BookStoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Adres> Adres { get; set; }
        public virtual DbSet<exceptionlog> exceptionlog { get; set; }
        public virtual DbSet<Kitap> Kitap { get; set; }
        public virtual DbSet<Musteri> Musteri { get; set; }
        public virtual DbSet<Siparis> Siparis { get; set; }
        public virtual DbSet<SiparisIcerigi> SiparisIcerigi { get; set; }
        public virtual DbSet<Yazar> Yazar { get; set; }
    }
}
