﻿using Bacchus.src.DAOs;
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
    public partial class MarqueForm : Form
    {
        string Marque = "";

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Marque"></param>
        public MarqueForm(string Marque = "")
        {
            InitializeComponent();

            this.Marque = Marque;

            // Si on veut créer une nouvelle marque
            if (Marque == "")
            {
                Text = "Créer une nouvelle marque";
                ConfirmButton.Text = "Ajouter la marque";
                RefTextBox.Text = "Réference générée automatiquement";
                ConfirmButton.Enabled = false;
            }
            // Si on veut modifier une marque existante
            else
            {
                DAOMarque daoMarque = new DAOMarque();

                Text = "Modifier la marque [" + Marque + "]";
                ConfirmButton.Text = "Modifier la marque";
                ConfirmButton.Enabled = true;

                RefTextBox.Text = daoMarque.GetRefMarque(Marque).ToString();
                NameTextBox.Text = Marque;
            }
        }

        /// <summary>
        /// Event déclenché lors d'un clic sur le bouton de confirmation.
        /// Ajoute ou modifie la marque
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            DAOMarque daoMarque = new DAOMarque();

            // Si on veut créer une marque
            if(Marque == "")
            {
                daoMarque.AddMarque(NameTextBox.Text);
                Close();
            }
            // Si on veut modifier une marque existante
            else
            {
                daoMarque.UpdateMarque(daoMarque.GetRefMarque(Marque), NameTextBox.Text);
                Close();
            }
        }

        /// <summary>
        /// Event déclenché lorsque le texte de la NameTextBox change.
        /// Utilisé pour vérifié la validité des champs entrés par l'utilisateur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
