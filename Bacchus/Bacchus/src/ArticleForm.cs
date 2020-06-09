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

            if(RefArticle == "")
            {
                Text = "Créer un nouvel article";
                ConfirmButton.Text = "Ajouter l'article";
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
        
        public void InitializeComboBox()
        {
            DAOFamille daoFamille = new DAOFamille();
            DAOSousFamille daoSousFamille = new DAOSousFamille();
            DAOMarque daoMarque = new DAOMarque();

            FamilleComboBox.Items.AddRange(daoFamille.GetAllFamilles().ToArray<object>());
            //Si on modifie un article, on initialise la valeur de la combo box sur la famille de l'article
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

                LoadSousFamilleComboBox(article.RefFamille);
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
            FamilleComboBox.SelectedIndexChanged += new EventHandler(FamilleComboBox_OnSelectedIndexChanged);
            
            MarqueComboBox.Items.AddRange(daoMarque.GetAllMarques().ToArray<object>());
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

        public void FamilleComboBox_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DAOFamille daoFamille = new DAOFamille();
            DAOSousFamille daoSousFamille = new DAOSousFamille();

            SousFamilleComboBox.Items.Clear();
            LoadSousFamilleComboBox(daoFamille.GetRefFamille(FamilleComboBox.SelectedItem.ToString()));
            //Si le on modifie un article et que on reselectionne sa famille initiale, on remet la valeur de la sous famille comme initiale
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

        public void LoadSousFamilleComboBox(int RefFamille)
        {
            DAOSousFamille daoSousFamille = new DAOSousFamille();
            SousFamilleComboBox.Items.AddRange(daoSousFamille.GetAllSousFamilles(RefFamille).ToArray<object>());
        }

        public int GetIndexOfItem(string Item, ComboBox.ObjectCollection Items)
        {
            for (uint i = 0; i < Items.Count; i++)
            {
                Console.WriteLine("On compare {0} et {1}", Item, Items[(int)i].ToString());
                if (string.Compare(Item, Items[(int)i].ToString()) == 0)
                {
                    return (int)i;
                }
            }

            return -1;
        }

        public void LoadArticle(string RefArticle)
        {
            RefTextBox.Text = article.RefArticle;
            DescTextBox.Text = article.Description;
            PrixTextBox.Text = article.PrixHT.ToString();
            QuantiteNumericUpDown.Value = article.Quantite;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            DAOArticle daoArticle = new DAOArticle();
            DAOFamille daoFamille = new DAOFamille();
            DAOSousFamille daoSousFamille = new DAOSousFamille();
            DAOMarque daoMarque = new DAOMarque();

            //Si oui :
                //Création de l'objet Article à partir des informations contenues dans les champs précédants
            article.Description = DescTextBox.Text;

            article.Famille = FamilleComboBox.SelectedItem.ToString();
            article.RefFamille = daoFamille.GetRefFamille(article.Famille);
            
            article.SousFamille = SousFamilleComboBox.SelectedItem.ToString();
            article.RefSousFamille = daoSousFamille.GetRefSousFamille(article.RefFamille, article.SousFamille);

            article.Marque = MarqueComboBox.SelectedItem.ToString();
            article.RefMarque = daoMarque.GetRefMarque(article.Marque);

            article.PrixHT = float.Parse(PrixTextBox.Text);
            article.Quantite = (int)QuantiteNumericUpDown.Value;

            Console.WriteLine("Article modifié : {0}", article);
            daoArticle.AddOrUpdateArticle(article);
            //Si non:
            //Affichage message d'erreur
        }

        /// <summary>
        /// On regarde si chaque champs a une valeur valide
        /// Desc         : Champ non null
        /// Famille      : Champ non null
        /// Sous Famille : Champ non null
        /// Marque       : Champ non null
        /// Prix         : float (virgule et pas point)
        /// </summary>
        /// <returns></returns>
        public bool AreFieldsValid()
        {
            if (string.Compare(DescTextBox.Text, "") == 0)
            {
                Console.WriteLine("DescTextBox pas valide");
                return false;
            }

            if (FamilleComboBox.SelectedItem == null)
            {
                Console.WriteLine("FamilleComboBox pas valide");
                return false;
            }

            if (SousFamilleComboBox.SelectedItem == null)
            {
                Console.WriteLine("SousFamilleComboBox pas valide");
                return false;
            }

            if (MarqueComboBox.SelectedItem == null)
            {
                Console.WriteLine("MarqueComboBox pas valide");
                return false;
            }

            try
            {
                float.Parse(PrixTextBox.Text);
            }

            catch (Exception)
            {
                Console.WriteLine("PrixTextBox pas valide");
                return false;
            }

            return true;
        }

        public void CheckFields()
        {
            if (AreFieldsValid())
            {
                Console.WriteLine("Fields are valid !");
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
    }
}
