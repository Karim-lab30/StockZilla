using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StockZilla.Models
{
    public class categoContext : DbContext
    {
        public categoContext() : base("name=Database1Entities") { }

        public DbSet<catego> Categories { get; set; }
        public DbSet<produits> Produits { get; set; }

    }


}
