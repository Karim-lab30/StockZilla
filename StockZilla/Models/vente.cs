//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class vente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vente()
        {
            this.detailsvente = new HashSet<detailsvente>();
        }
    
        public int Id { get; set; }
        public System.DateTime date_vente { get; set; }
        public string num_commande { get; set; }
        public string adresse_livr { get; set; }
        public string adresse_fact { get; set; }
        public double prix_livraison { get; set; }
        public string mode_paiement { get; set; }
        public int id_vendeur { get; set; }
        public int id_client { get; set; }
        public System.DateTime date_livraison { get; set; }
        public bool livre { get; set; }
        public int id_livraison { get; set; }
        public int cod_post_fact { get; set; }
        public string ville_fact { get; set; }
        public string pays_fact { get; set; }
        public int cod_post_livr { get; set; }
        public string ville_livr { get; set; }
        public string pays_livr { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detailsvente> detailsvente { get; set; }
        public virtual livraison livraison { get; set; }
        public virtual users users { get; set; }
        public virtual users users1 { get; set; }
    }
}
