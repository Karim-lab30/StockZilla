using StockZilla.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StockZilla.Controllers
{
    public class ProduitController : Controller
    {
    produitContext prod =new produitContext();

        public ActionResult ListeProduit()

        {
            var ProduitjoinCategorie = prod.Produits.Include("catego").ToList();


            return View("ListeProduits", ProduitjoinCategorie);

            
        }
    }
}