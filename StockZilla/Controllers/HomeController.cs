using StockZilla.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StockZilla.Controllers
{
    public class HomeController : Controller
    {
        categoContext catego = new categoContext();


        public ActionResult Index()
        {
         
            var categorie = catego.Categories
                .Select(c => new CategoModel
                {
                    Id = c.Id,
                    NomCategorie = c.nom,
                    TVA = c.tva,
                    Image = c.image

                }).ToList();

            foreach (var produit in categorie)
            {
                if (produit.Image != null)
                {
                    produit.ImageBase64 = Convert.ToBase64String(produit.Image);
                }
            }
            return View("Index", categorie);
        }


    }
}