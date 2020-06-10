using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bacchus.src.DAOs;

namespace Bacchus
{
    public partial class ArticleForm : Form
    {
        private Article article;
        enum ModeEnum
        {
            Add,
            Edit
        }
        private ModeEnum Mode;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="RefArticle"></param>
        public ArticleForm(string RefArticle = "")
        {
            InitializeComponent();

            ConfirmButton.Enabled = false;

            article = new Article();

            if(RefArticle == "")
            {
                Text = "Créer un nouvel article";
                ConfirmButton.Text = "Ajouter l'article";
                RefTextBox.ReadOnly = false;
                Mode = ModeEnum.Add;
            }
            else
            {
                Text = "Modifier l'article [" + RefArticle + "]";
                ConfirmButton.Text = "Modifier l'article";
                Mode = ModeEnum.Edit;

                DAOArticle daoArticle = new DAOArticle();
                article = daoArticle.GetArticle(RefArticle);
                LoadArticle(RefArticle);
            }
            InitializeComboBox();

            // Si RefArticle est vide ==> création d'un nouvel article
            // Sinon, récupérer l'article avec un DAO et remplir les différents champs avec les valeurs de l'article
        }
        
        /// <summary>
        /// Charge les données de chaque ComboBox (ie : La liste des familles, La liste des sous familles de cette famille et la liste des marques)
        /// </summary>
        public void InitializeComboBox()
        {
            DAOFamille daoFamille = new DAOFamille();
            DAOSousFamille daoSousFamille = new DAOSousFamille();
            DAOMarque daoMarque = new DAOMarque();

            //On charge la liste des familles
            FamilleComboBox.Items.AddRange(daoFamille.GetAllFamilles().ToArray<object>());
            //Si on modifie un article, on initialise la valeur de la comboBox correspondante à la famille originale de l'article
            if (Mode == ModeEnum.Edit)
            {
                int index = GetIndexOfItem(article.Famille, FamilleComboBox.Items);
                if (index != -1)
                {
                    FamilleComboBox.SelectedIndex = index;
                }
                else
                {
                    Console.WriteLine("Erreur sur l'indice de la famille de l'article dans la FamilleComboBox");
                }

                //On charge la liste des sous-familles de la famille sélectionnée
                LoadSousFamilleComboBox(article.RefFamille);
                //Si la famille actuellement sélectionnée est celle de l'article pré-modification (originale), alors on initialise la valeur de la comboBox correspondante à la sous-famille
                //originale de l'article
                index = GetIndexOfItem(daoSousFamille.GetNomSousFamille(article.RefSousFamille), SousFamilleComboBox.Items);
                if (index != -1)
                {
                    SousFamilleComboBox.SelectedIndex = index;
                }
                else
                {
                    Console.WriteLine("Erreur sur l'indice de la sous famille de l'article dans la SousFamilleComboBox");
                }
            }
            
            //On charge la liste des marques
            MarqueComboBox.Items.AddRange(daoMarque.GetAllMarques().ToArray<object>());
            //Si on modifie un article, on initialise la valeur de la comboBox correspondante à la marque originale de l'article
            if (Mode == ModeEnum.Edit)
            {
                int index = GetIndexOfItem(article.Marque, MarqueComboBox.Items);
                if (index != -1)
                {
                    MarqueComboBox.SelectedIndex = index;
                }
                else
                {
                    Console.WriteLine("Erreur sur l'indice de la marque de l'article dans la MarqueComboBox");
                }
            }
        }

        /// <summary>
        /// On charge la liste des sous-famille d'une certaine famille (spécifiée par sa référence)
        /// </summary>
        /// <param name="RefFamille"></param>
        public void LoadSousFamilleComboBox(int RefFamille)
        {
            DAOSousFamille daoSousFamille = new DAOSousFamille();
            SousFamilleComboBox.Items.AddRange(daoSousFamille.GetAllSousFamilles(RefFamille).ToArray<object>());
        }

        /// <summary>
        /// Recherche un objet au sein d'une liste d'objet appartenant à une comboBox et renvoie son indice si il existe.
        /// </summary>
        /// <param name="Item">L'objet à rechercher.</param>
        /// <param name="Items">La liste d'objet dans la ComboBox.</param>
        /// <returns>
        /// L'indice de l'objet "Item" au sein de la liste "Items" si il s'y trouve, -1 sinon.
        /// </returns>
        public int GetIndexOfItem(string Item, ComboBox.ObjectCollection Items)
        {
            for (uint i = 0; i < Items.Count; i++)
            {
                if (string.Compare(Item, Items[(int)i].ToString()) == 0)
                {
                    return (int)i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Initialise les champs "Référence", "Description", "Prix HT" et quantité en fonction d'un article
        /// </summary>
        /// <param name="RefArticle">Article à partir duquel initialiser les champs énoncés plus haut.</param>
        public void LoadArticle(string RefArticle)
        {
            RefTextBox.Text = article.RefArticle;
            DescTextBox.Text = article.Description;
            PrixTextBox.Text = article.PrixHT.ToString();
            QuantiteNumericUpDown.Value = article.Quantite;
        }

        /// <summary>
        /// Valide la création ou la modification d'un article dans la BDD.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            DAOArticle daoArticle = new DAOArticle();
            DAOFamille daoFamille = new DAOFamille();
            DAOSousFamille daoSousFamille = new DAOSousFamille();
            DAOMarque daoMarque = new DAOMarque();
            
            article.RefArticle = RefTextBox.Text;
            article.Description = DescTextBox.Text;

            article.Famille = FamilleComboBox.SelectedItem.ToString();
            article.RefFamille = daoFamille.GetRefFamille(article.Famille);
            
            article.SousFamille = SousFamilleComboBox.SelectedItem.ToString();
            article.RefSousFamille = daoSousFamille.GetRefSousFamille(article.RefFamille, article.SousFamille);

            article.Marque = MarqueComboBox.SelectedItem.ToString();
            article.RefMarque = daoMarque.GetRefMarque(article.Marque);

            article.PrixHT = float.Parse(PrixTextBox.Text);
            article.Quantite = (int)QuantiteNumericUpDown.Value;
            
            daoArticle.AddOrUpdateArticle(article);
            Close();
        }

        /// <summary>
        /// Vérifie si chaque champs a une valeur valide
        /// Desc         : Champ non null
        /// Famille      : Champ non null
        /// Sous Famille : Champ non null
        /// Marque       : Champ non null
        /// Prix         : float (virgule et pas point)
        /// </summary>
        /// <returns>Vrai si tout les champs sont valides, faux sinon.</returns>
        public bool AreFieldsValid()
        {
            if (string.Compare(DescTextBox.Text, "") == 0)
            {
                return false;
            }

            if (FamilleComboBox.SelectedItem == null)
            {
                return false;
            }

            if (SousFamilleComboBox.SelectedItem == null)
            {
                return false;
            }

            if (MarqueComboBox.SelectedItem == null)
            {
                return false;
            }

            try
            {
                float.Parse(PrixTextBox.Text);
            }

            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Active ou désactive le bouton de validation des modifications en fonction de la validité de chacun des champs.
        /// </summary>
        public void CheckFields()
        {
            if (AreFieldsValid())
            {
                ConfirmButton.Enabled = true;
            }
            else
            {
                ConfirmButton.Enabled = false;
            }
        }

        private void DescTextBox_TextChanged(object sender, EventArgs e)
        {
            CheckFields();
        }

        private void SousFamilleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckFields();
        }

        private void MarqueComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckFields();
        }

        private void PrixTextBox_TextChanged(object sender, EventArgs e)
        {
            CheckFields();
        }

        /// <summary>
        /// Recharge la liste de sous-famille de la comboBox correspondante lorsque la famille sélectionnée change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FamilleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DAOFamille daoFamille = new DAOFamille();
            DAOSousFamille daoSousFamille = new DAOSousFamille();

            SousFamilleComboBox.Items.Clear();
            LoadSousFamilleComboBox(daoFamille.GetRefFamille(FamilleComboBox.SelectedItem.ToString()));
            //Si on modifie un article et qu'on resélectionne sa famille initiale, on initialise l'index de la comboBox à celui de la sous famille initiale.
            if (Mode == ModeEnum.Edit && string.Compare(article.Famille, FamilleComboBox.SelectedItem.ToString()) == 0)
            {
                int index = GetIndexOfItem(daoSousFamille.GetNomSousFamille(article.RefSousFamille), SousFamilleComboBox.Items);
                if (index != -1)
                {
                    SousFamilleComboBox.SelectedIndex = index;
                }
                else
                {
                    Console.WriteLine("Erreur sur l'indice de la sous famille de l'article dans la SousFamilleComboBox");
                }
            }
            //Sinon, on met juste la valeur de l'indice de la combo box à 0
            else
            {
                SousFamilleComboBox.SelectedIndex = 0;
            }

            CheckFields();
        }
    }
}
