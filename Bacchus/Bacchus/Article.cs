using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus
{
    class Article
    {
        /* ATTRIBUTS */
        public string RefArticle { get; set; }
        public string Description { get; set; }
        public string Famille { get; set; }
        public int RefFamille { get; set; }
        public string SousFamille { get; set; }
        public int RefSousFamille { get; set; }
        public string Marque { get; set; }
        public int RefMarque { get; set; }
        public float PrixHT { get; set; }
        public int Quantite { get; set; }

        /* MÉTHODES */
        public override string ToString()
        {
            return string.Format("Description : {0} || Ref : {1} || Marque : {2} || Famille : {3} || Sous-Famille : {4} || Prix : {5} ", Description, RefArticle, Marque, Famille, SousFamille, PrixHT);
        }
    }
}
