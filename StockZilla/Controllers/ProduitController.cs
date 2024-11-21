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
            var ProduitjoinCategorieDetails = prod.Produits
                .Include("catego")
                .Include("detailsvente")
                .Select(p => new ProduitModel
                {
                    Id = p.Id,
                    NomProduit = p.nom_prod,
                    NomCategorie = p.catego.nom,
                    Quantite = p.qte_prod,
                    PrixMoyen = p.prix_moyen,
                    TVA = p.detailsvente.FirstOrDefault().tva,
                    Prix_Ht = p.detailsvente.FirstOrDefault().prix_ht,
                    Remise = p.detailsvente.FirstOrDefault().remise
                }).ToList();

            return View("ListeProduits", ProduitjoinCategorieDetails);
        }

    }
}