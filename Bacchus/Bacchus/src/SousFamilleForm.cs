using Bacchus.src.DAOs;
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
    public partial class SousFamilleForm : Form
    {
        string Famille = "";
        string SousFamille = "";

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Famille"></param>
        /// <param name="SousFamille"></param>
        public SousFamilleForm(string Famille = "", string SousFamille = "")
        {
            InitializeComponent();

            this.Famille = Famille;
            this.SousFamille = SousFamille;

            // On remplit la combo box
            DAOFamille daoFamille = new DAOFamille();
            DAOSousFamille daoSousFamille = new DAOSousFamille();
            List<string> ListeFamilles = daoFamille.GetAllFamilles();
            foreach(string Nom in ListeFamilles)
            {
                FamilleComboBox.Items.Add(Nom);
            }
            FamilleComboBox.SelectedIndex = 0;

            // Si on veut créer une nouvelle famille
            if (SousFamille == "")
            {
                Text = "Créer une nouvelle sous-famille";
                ConfirmButton.Text = "Ajouter la sous-famille";
                RefTextBox.Text = "Réference générée automatiquement";
            }
            // Si on veut modifier une sous-famille existante
            else
            {
                Text = "Modifier la sous-famille [" + SousFamille + "]";
                ConfirmButton.Text = "Modifier la sous-famille";
                FamilleComboBox.Enabled = false;

                FamilleComboBox.SelectedIndex = FamilleComboBox.FindString(Famille);

                RefTextBox.Text = daoSousFamille.GetRefSousFamille(daoFamille.GetRefFamille(Famille), SousFamille).ToString();
                NameTextBox.Text = SousFamille;
            }

            UpdateConfirmButton();
        }

        /// <summary>
        /// Event déclenché lors d'un clic sur le bouton de confirmation.
        /// Ajoute ou modifie la sous-famille.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            DAOFamille daoFamille = new DAOFamille();
            DAOSousFamille daoSousFamille = new DAOSousFamille();

            // Si on veut créer une sous-famille
            if(SousFamille == "")
            {
                string SelectedItem = FamilleComboBox.Items[FamilleComboBox.SelectedIndex].ToString();
                daoSousFamille.AddSousFamille(daoFamille.GetRefFamille(SelectedItem), NameTextBox.Text);
                Close();
            }
            // Si on veut modifier une sous-famille existante
            else
            {
                int RefFamille = daoFamille.GetRefFamille(FamilleComboBox.Items[FamilleComboBox.SelectedIndex].ToString());
                daoSousFamille.UpdateSousFamille(daoSousFamille.GetRefSousFamille(RefFamille, SousFamille), NameTextBox.Text);
                Close();
            }
        }

        /// <summary>
        /// Event déclenché lorsque le texte de la NameTextBox change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateConfirmButton();
        }

        /// <summary>
        /// Event déclenché lorsque l'item sélectionné dans la ComboBox change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FamilleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateConfirmButton();
        }

        /// <summary>
        /// Fonction qui active ou non le bouton de confirmation en fonction de la validité des champs.
        /// </summary>
        private void UpdateConfirmButton()
        {
            if(NameTextBox.Text != "" && FamilleComboBox.SelectedIndex != -1)
            {
                ConfirmButton.Enabled = true;
            }
            else
            {
                ConfirmButton.Enabled = false;
            }
        }

        
    }
}
