using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bacchus
{
    public partial class ArticleForm : Form
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="RefArticle"></param>
        public ArticleForm(string RefArticle = "")
        {
            InitializeComponent();

            ConfirmButton.Enabled = false;

            if(RefArticle == "")
            {
                Text = "Créer un nouvel article";
                ConfirmButton.Text = "Ajouter l'article";
            }
            else
            {
                Text = "Modifier l'article [" + RefArticle + "]";
                ConfirmButton.Text = "Modifier l'article";
            }

            // Si RefArticle est vide ==> création d'un nouvel article
            // Sinon, récupérer l'article avec un DAO et remplir les différents champs avec les valeurs de l'article
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {

        }
    }
}
