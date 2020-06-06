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
    public partial class FormMain : Form
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            CenterToScreen();

            // Change la taille minimum de la section gauche (la TreeView)
            splitContainer1.Panel1MinSize = 200;

            RefreshDisplay();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Fonction appelée en cliquant sur le menu "Importer".
        /// Affiche une fenêtre qui permet d'importer un fichier CSV dans la BDD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportForm Form = new ImportForm();
            Form.Show();
        }

        /// <summary>
        /// Fonction appelée en cliquant sur le menu "Exporter".
        /// Affiche une fenêtre qui permet d'exporter la BDD dans un fichier CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportForm Form = new ExportForm();
            Form.Show();
        }

        /// <summary>
        /// Fonction appelée en cliquant sur le menu "Actualiser".
        /// Actualise la treeView et la listView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void actualiserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            RefreshTreeView();
        }

        private void RefreshTreeView()
        {
            TreeView.BeginUpdate();

            // On vide les noeuds "Familles" et "Marques"
            TreeView.Nodes[1].Nodes.Clear();
            TreeView.Nodes[2].Nodes.Clear();

            DAO dao = new DAO();

            // Ajout des noeuds "Familles"
            List<string> ListeFamilles = dao.GetAllFamilles();
            foreach(string Famille in ListeFamilles)
            {
                TreeNode NodeFamille = new TreeNode(Famille);
                TreeView.Nodes[1].Nodes.Add(NodeFamille);

                // Ajout des noeuds "Sous-Famille"
                List<string> ListeSousFamilles = dao.GetAllSousFamilles(dao.GetRefFamille(Famille));
                foreach(string SousFamille in ListeSousFamilles)
                {
                    NodeFamille.Nodes.Add(SousFamille);
                }
            }

            // Ajout des noeuds "Marques"
            List<string> ListeMarques = dao.GetAllMarques();
            foreach(string Marque in ListeMarques)
            {
                TreeView.Nodes[2].Nodes.Add(Marque);
            }

            TreeView.EndUpdate();
        }
    }
}
