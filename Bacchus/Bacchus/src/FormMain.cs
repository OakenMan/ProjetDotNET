using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Bacchus.src.DAOs;

namespace Bacchus
{
    public partial class FormMain : Form
    {
        private string ListViewDisplay = "ARTICLES";
        private string ListViewCondition = "";
        private string ListViewValue = "";
        private string ListViewValue2 = "";

        /// <summary>
        /// Constructeur
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            CenterToScreen();

            // Change la taille minimum de la section gauche (la TreeView)
            splitContainer1.Panel1MinSize = 200;
            KeyPreview = true;

            RefreshDisplay();
        }

        /// <summary>
        /// Event déclenché en cliquant sur le menu "Importer".
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
        /// Event déclenché en cliquant sur le menu "Exporter".
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
        /// Event déclenché en cliquant sur le menu "Actualiser".
        /// Actualise la treeView et la listView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void actualiserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshDisplay();
        }

        /// <summary>
        /// Actualise l'affichage de l'application (TreeView et ListView)
        /// </summary>
        private void RefreshDisplay()
        {
            RefreshTreeView();
            RefreshListView();
        }

        /// <summary>
        /// Actualise le contenu de la TreeView
        /// </summary>
        private void RefreshTreeView()
        {
            TreeView.BeginUpdate();

            // On vide les noeuds "Familles" et "Marques"
            TreeView.Nodes[1].Nodes.Clear();
            TreeView.Nodes[2].Nodes.Clear();

            DAOFamille daoFamille = new DAOFamille();
            DAOSousFamille daoSousFamille = new DAOSousFamille();
            DAOMarque daoMarque = new DAOMarque();

            // Ajout des noeuds "Familles"
            List<string> ListeFamilles = daoFamille.GetAllFamilles();
            foreach(string Famille in ListeFamilles)
            {
                TreeNode NodeFamille = new TreeNode(Famille);
                TreeView.Nodes[1].Nodes.Add(NodeFamille);

                // Ajout des noeuds "Sous-Famille"
                List<string> ListeSousFamilles = daoSousFamille.GetAllSousFamilles(daoFamille.GetRefFamille(Famille));
                foreach(string SousFamille in ListeSousFamilles)
                {
                    NodeFamille.Nodes.Add(SousFamille);
                }
            }

            // Ajout des noeuds "Marques"
            List<string> ListeMarques = daoMarque.GetAllMarques();
            foreach(string Marque in ListeMarques)
            {
                TreeView.Nodes[2].Nodes.Add(Marque);
            }

            TreeView.EndUpdate();
        }

        /// <summary>
        /// Actualise le contenu de la ListView
        /// </summary>
        private void RefreshListView()
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", ListViewDisplay, ListViewCondition, ListViewValue, ListViewValue2);

            // Paramètres de la ListView
            ListView.GridLines = true;
            ListView.FullRowSelect = true;
            ListView.Sorting = SortOrder.Ascending;

            // On nettoie la ListView
            ListView.Columns.Clear();
            ListView.Items.Clear();

            // Si on veut afficher des articles
            if (ListViewDisplay == "ARTICLES")
            {
                ListView.Columns.Add("Description", 150, HorizontalAlignment.Left);
                ListView.Columns.Add("Famille", 150, HorizontalAlignment.Left);
                ListView.Columns.Add("Sous-Famille", 150, HorizontalAlignment.Left);
                ListView.Columns.Add("Marque", 100, HorizontalAlignment.Left);
                ListView.Columns.Add("PrixHT", 100, HorizontalAlignment.Center);
                ListView.Columns.Add("Quantité", 100, HorizontalAlignment.Center);
            }
            // Si on veut afficher des marques, des familles ou des sous-familles
            else
            {
                ListView.Columns.Add("Description", -2, HorizontalAlignment.Left);
            }

            DAOArticle daoArticle = new DAOArticle();
            DAOMarque daoMarque = new DAOMarque();
            DAOFamille daoFamille = new DAOFamille();
            DAOSousFamille daoSousFamille = new DAOSousFamille();

            // Si on veut afficher tous les articles
            if(ListViewDisplay == "ARTICLES")
            {
                List<Article> ListeArticles = new List<Article>();
                
                if(ListViewCondition == "")
                {
                    ListeArticles = daoArticle.GetAllArticles();
                }
                else if(ListViewCondition == "MARQUE")
                {
                    //ListeArticles = dao.GetArticlesWhereMarque(ListViewValue);
                }
                else if(ListViewCondition == "SOUSFAMILLE")
                {
                    //ListeArticles = dao.GetArticlesWhereSousFamille(ListViewValue2, ListViewValue);
                }

                // Enfin, on ajoute tous les articles à la ListView
                foreach (Article NewArticle in ListeArticles)
                {
                    ListViewItem Item = new ListViewItem(NewArticle.Description);
                    Item.SubItems.Add(NewArticle.Famille);
                    Item.SubItems.Add(NewArticle.SousFamille);
                    Item.SubItems.Add(NewArticle.Marque);
                    Item.SubItems.Add(NewArticle.PrixHT.ToString());
                    Item.SubItems.Add(NewArticle.Quantite.ToString());
                    ListView.Items.Add(Item);
                }
            }
            else if(ListViewDisplay == "MARQUES")
            {
                List<string> ListeMarques = daoMarque.GetAllMarques();
                foreach(string Marque in ListeMarques)
                {
                    ListView.Items.Add(new ListViewItem(Marque));
                }
            }
            else if(ListViewDisplay == "FAMILLES")
            {
                List<string> ListeFamilles = daoFamille.GetAllFamilles();
                foreach(string Famille in ListeFamilles)
                {
                    ListView.Items.Add(new ListViewItem(Famille));
                }
            }
            else if(ListViewDisplay == "SOUSFAMILLES")
            {
                List<string> ListeSousFamilles = daoSousFamille.GetAllSousFamilles(daoFamille.GetRefFamille(ListViewValue));
                foreach(string SousFamille in ListeSousFamilles)
                {
                    ListView.Items.Add(new ListViewItem(SousFamille));
                }
            }
            else
            {
                Console.WriteLine("Erreur in RefreshListView");
            } 
        }

        /// <summary>
        /// Event déclenché à chaque clic de souris sur un élément de la TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Console.WriteLine("click");

            ListViewDisplay = "";
            ListViewCondition = "";
            ListViewValue = "";
            ListViewValue2 = "";

            if (e.Node.Text == "Tous les articles")
            {
                ListViewDisplay = "ARTICLES";
            }
            else if(e.Node.Text == "Familles")
            {
                ListViewDisplay = "FAMILLES";
            }
            else if(e.Node.Text == "Marques")
            {
                ListViewDisplay = "MARQUES";
            }
            else if(e.Node.Parent.Text == "Familles")
            {
                ListViewDisplay = "SOUSFAMILLES";
                ListViewValue = e.Node.Text;
            }
            else if(e.Node.Parent.Text == "Marques")
            {
                ListViewDisplay = "ARTICLES";
                ListViewCondition = "MARQUE";
                ListViewValue = e.Node.Text;
            }
            else
            {
                ListViewDisplay = "ARTICLES";
                ListViewCondition = "SOUSFAMILLE";
                ListViewValue = e.Node.Text;
                ListViewValue2 = e.Node.Parent.Text;
            }

            RefreshListView();
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            // TODO : trouver un moyen d'ignorer la répétition d'inputs
            switch(e.KeyCode)
            {
                case Keys.Enter:  Console.WriteLine("Enter"); break;
                case Keys.Delete: Console.WriteLine("Suppr"); break;
                case Keys.F5:     Console.WriteLine("F5");    break;
                default: break;
            }
            e.Handled = true;
        }
    }
}
