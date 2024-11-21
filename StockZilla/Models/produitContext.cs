using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StockZilla.Models
{
    public class produitContext : DbContext
    {
        public produitContext() : base("name=Database1Entities") { }

        public DbSet<produits> Produits { get; set; }
       

    }

   
}
