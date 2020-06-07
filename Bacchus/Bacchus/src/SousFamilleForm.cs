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

            // Si on veut créer une nouvelle famille
            if (SousFamille == "")
            {
                Text = "Créer une nouvelle sous-famille";
                ConfirmButton.Text = "Ajouter la sous-famille";
                RefTextBox.Text = "Réference générée automatiquement";
                ConfirmButton.Enabled = false;
            }
            // Si on veut modifier une sous-famille existante
            else
            {
                Text = "Modifier la sous-famille [" + SousFamille + "]";
                ConfirmButton.Text = "Modifier la sous-famille";
                ConfirmButton.Enabled = true;
                FamilleComboBox.Enabled = false;

                FamilleComboBox.SelectedIndex = FamilleComboBox.FindString(Famille);

                RefTextBox.Text = daoSousFamille.GetRefSousFamille(daoFamille.GetRefFamille(Famille), SousFamille).ToString();
                NameTextBox.Text = SousFamille;
            }
        }

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
                //daoSousFamille.UpdateSousFamille(daoSousFamille.GetRefSousFamille(SousFamille), NameTextBox.Text);
                Close();
            }
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if(NameTextBox.Text == "")
            {
                ConfirmButton.Enabled = false;
            }
            else
            {
                ConfirmButton.Enabled = true;
            }
        }
    }
}
