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
        produitContext prod = new produitContext();

        public ActionResult ListeProduit(int? categorieId)
        {
            var produitQuery = prod.Produits
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
                    Remise = p.detailsvente.FirstOrDefault().remise,
                    IdCategorie = p.id_categorie,
                    Image = p.image
                });

            if (categorieId.HasValue)
            {
                produitQuery = produitQuery.Where(p => p.IdCategorie == categorieId.Value);
            }

            var ProduitjoinCategorieDetails = produitQuery.ToList();

            // Conversion de l'image en base64
            foreach (var produit in ProduitjoinCategorieDetails)
            {
                if (produit.Image != null)
                {
                    produit.ImageBase64 = Convert.ToBase64String(produit.Image);
                }
            }

            return View("ListeProduits", ProduitjoinCategorieDetails);
        }
        public ActionResult ProduitsParCategorie(int? IdCategorie, string categorieNom = null)
        {
            // Vérifier si l'ID ou le nom de la catégorie est fourni
            var produitsQuery = prod.Produits
                .Include("catego")
                .Select(p => new ProduitModel
                {
                    Id = p.Id,
                    NomProduit = p.nom_prod,
                    NomCategorie = p.catego.nom,
                    Quantite = p.qte_prod,
                    PrixMoyen = p.prix_moyen,
                    TVA = p.detailsvente.FirstOrDefault().tva,
                    Prix_Ht = p.detailsvente.FirstOrDefault().prix_ht,
                    Remise = p.detailsvente.FirstOrDefault().remise,
                    Image = p.image,
                    IdCategorie = p.id_categorie
                });

            // Filtrer par ID si fourni
            if (IdCategorie.HasValue)
            {
                produitsQuery = produitsQuery.Where(p => p.IdCategorie == IdCategorie.Value);
                ViewBag.CategorieNom = prod.Categories.FirstOrDefault(c => c.Id == IdCategorie)?.nom;
            }
            else if (!string.IsNullOrEmpty(categorieNom))
            {
                produitsQuery = produitsQuery.Where(p => p.NomCategorie == categorieNom);
                ViewBag.CategorieNom = categorieNom;
            }

            var produits = produitsQuery.ToList();

            // Convertir les images en base64
            foreach (var produit in produits)
            {
                if (produit.Image != null)
                {
                    produit.ImageBase64 = Convert.ToBase64String(produit.Image);
                }
            }

            return View("ProduitsParCategorie", produits);
        }

        public ActionResult DetailsProduits(int idprod)
        {
            var produit = prod.Produits.Include("catego").Include("detailsvente")
                .Where(p => p.Id == idprod)
                .Select(p => new ProduitModel
                {
                    Id = p.Id,
                    NomProduit = p.nom_prod,
                    NomCategorie = p.catego.nom,
                    Quantite = p.qte_prod,
                    PrixMoyen = p.prix_moyen,
                    TVA = p.detailsvente.FirstOrDefault().tva,
                    Prix_Ht = p.detailsvente.FirstOrDefault().prix_ht,
                    Remise = p.detailsvente.FirstOrDefault().remise,
                    IdCategorie = p.id_categorie,
                    Image = p.image
                })
                .FirstOrDefault();

            if (produit == null)
            {
                return HttpNotFound();
            }

            // Convertir l'image en base64 pour l'affichage
            if (produit.Image != null)
            {
                produit.ImageBase64 = Convert.ToBase64String(produit.Image);
            }

            return View(produit);
        }
        [HttpPost]
        public ActionResult Acheter(int idprod)
        {
            // Récupérer le produit depuis la base de données
            var produit = prod.Produits.FirstOrDefault(p => p.Id == idprod);

            if (produit == null)
            {
                return HttpNotFound(); // Si le produit n'est pas trouvé
            }

            // Vérifier si la quantité est disponible (plus grande que 0)
            if (produit.qte_prod > 0)
            {
                produit.qte_prod--; // Réduire la quantité de 1
                prod.SaveChanges(); // Sauvegarder les changements dans la base de données

                TempData["Message"] = "Achat effectué avec succès !"; // Message de succès
            }
            else
            {
                TempData["Message"] = "Désolé, ce produit est en rupture de stock."; // Message si rupture de stock
            }

            // Rediriger vers la page des détails du produit
            return RedirectToAction("DetailsProduits", new { idprod = produit.Id });
        }


        public ActionResult Details(int id)
        {

            var produit = prod.Produits.Include("catego").Include("detailsvente")
                .Where(p => p.Id == id)
                .Select(p => new ProduitModel
                {
                    Id = p.Id,
                    NomProduit = p.nom_prod,
                    NomCategorie = p.catego.nom,
                    Quantite = p.qte_prod,
                    PrixMoyen = p.prix_moyen,
                    TVA = p.detailsvente.FirstOrDefault().tva,
                    Prix_Ht = p.detailsvente.FirstOrDefault().prix_ht,
                    Remise = p.detailsvente.FirstOrDefault().remise,
                    IdCategorie = p.id_categorie
                })
                .FirstOrDefault();

            if (produit == null)
            {
                return HttpNotFound();
            }


            return View(produit);
        }
        // GET: Produit/Edit/5
        public ActionResult Edit(int id)
        {
            // Récupérer le produit à modifier depuis la base de données
            var produit = prod.Produits
                .Include("catego")
                .Include("detailsvente")
                .Where(p => p.Id == id)
                .Select(p => new ProduitModel
                {
                    Id = p.Id,
                    NomProduit = p.nom_prod,
                    IdCategorie = p.id_categorie,
                    NomCategorie = p.catego.nom,
                    Quantite = p.qte_prod,
                    Prix_Ht = p.detailsvente.FirstOrDefault().prix_ht,
                    TVA = p.detailsvente.FirstOrDefault().tva,
                    PrixMoyen = p.prix_moyen,
                    Remise = p.detailsvente.FirstOrDefault().remise
                })
                .FirstOrDefault();

            if (produit == null)
            {
                return HttpNotFound();
            }

            // Charger les catégories pour un dropdown, si nécessaire
            ViewBag.Categories = new SelectList(prod.Categories, "Id", "Nom", produit.IdCategorie);

            return View(produit);
        }

        // POST: Produit/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProduitModel model)
        {
            if (ModelState.IsValid)
            {
                // Récupérer le produit depuis la base de données
                var produit = prod.Produits.Include("detailsvente").FirstOrDefault(p => p.Id == model.Id);

                if (produit == null)
                {
                    return HttpNotFound();
                }

                // Mettre à jour les propriétés
                produit.nom_prod = model.NomProduit;
                produit.id_categorie = (int)model.IdCategorie;
                produit.qte_prod = model.Quantite;
                produit.prix_moyen = model.PrixMoyen;

                // Mettre à jour les détails de vente
                var detailsVente = produit.detailsvente.FirstOrDefault();
                if (detailsVente != null)
                {
                    detailsVente.prix_ht = model.Prix_Ht;
                    detailsVente.tva = model.TVA;
                    detailsVente.remise = (int?)model.Remise;
                }

                // Sauvegarder les modifications
                prod.SaveChanges();

                // Rediriger vers la liste des produits
                return RedirectToAction("ListeProduit");
            }

            // Recharger les catégories en cas d'erreur
            ViewBag.Categories = new SelectList(prod.Categories, "Id", "Nom", model.IdCategorie);

            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteProduit(int id)
        {
            var produit = prod.Produits.FirstOrDefault(p => p.Id == id);
            if (produit != null)
            {
                prod.Produits.Remove(produit);
                prod.SaveChanges();
            }

            // Redirigez vers la liste des produits après la suppression
            return RedirectToAction("ListeProduit");
        }
        public ActionResult Create()
        {
            // Si nécessaire, charger des données comme des catégories
            ViewBag.Categories = new SelectList(prod.Categories, "Id", "Nom");
            return View();
        }






    }
}