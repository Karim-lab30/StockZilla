﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockZilla.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Database1Entities : DbContext
    {
        public Database1Entities()
            : base("name=Database1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<achats> achats { get; set; }
        public virtual DbSet<adresse> adresse { get; set; }
        public virtual DbSet<catego> catego { get; set; }
        public virtual DbSet<detailsachat> detailsachat { get; set; }
        public virtual DbSet<detailsvente> detailsvente { get; set; }
        public virtual DbSet<livraison> livraison { get; set; }
        public virtual DbSet<produits> produits { get; set; }
        public virtual DbSet<role> role { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<vente> vente { get; set; }
    }
}
