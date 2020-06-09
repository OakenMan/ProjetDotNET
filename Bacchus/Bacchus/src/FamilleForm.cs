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
    public partial class FamilleForm : Form
    {
        string Famille = "";

        public FamilleForm(string Famille = "")
        {
            InitializeComponent();

            this.Famille = Famille;

            // Si on veut créer une nouvelle famille
            if (Famille == "")
            {
                Text = "Créer une nouvelle famille";
                ConfirmButton.Text = "Ajouter la famille";
                RefTextBox.Text = "Réference générée automatiquement";
                ConfirmButton.Enabled = false;
            }
            // Si on veut modifier une famille existante
            else
            {
                DAOFamille daoFamille = new DAOFamille();

                Text = "Modifier la famille [" + Famille + "]";
                ConfirmButton.Text = "Modifier la famille";
                ConfirmButton.Enabled = true;

                RefTextBox.Text = daoFamille.GetRefFamille(Famille).ToString();
                NameTextBox.Text = Famille;
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            DAOFamille daoFamille = new DAOFamille();

            // Si on veut créer une famille
            if(Famille == "")
            {
                daoFamille.AddFamille(NameTextBox.Text);
                Close();
            }
            // Si on veut modifier une famille existante
            else
            {
                daoFamille.UpdateFamille(daoFamille.GetRefFamille(Famille), NameTextBox.Text);
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
