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

        private ColumnHeader SortingColumn = null;

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
            // Paramètres de la ListView
            ListView.GridLines = true;
            ListView.FullRowSelect = true;              // Sélection d'une ligne tout entière
            ListView.MultiSelect = false;               // Pas possible de sélectionner plusieurs lignes
            ListView.Sorting = SortOrder.Ascending;     // Mode de tri par défaut
            
            //ListView.ContextMenu = ??? aller voir ListView.ContextMenu sur la doc

            // On nettoie la ListView
            ListView.Columns.Clear();
            ListView.Items.Clear();

            // Si on veut afficher des articles
            if (ListViewDisplay == "ARTICLES")
            {
                ListView.Columns.Add("Réference", 75, HorizontalAlignment.Center);
                ListView.Columns.Add("Description", 150, HorizontalAlignment.Left);
                ListView.Columns.Add("Famille", 150, HorizontalAlignment.Left);
                ListView.Columns.Add("Sous-Famille", 150, HorizontalAlignment.Left);
                ListView.Columns.Add("Marque", 100, HorizontalAlignment.Left);
                ListView.Columns.Add("PrixHT", 75, HorizontalAlignment.Center);
                ListView.Columns.Add("Quantité", 75, HorizontalAlignment.Center);
            }
            // Si on veut afficher des marques, des familles ou des sous-familles
            else
            {
                ListView.Columns.Add("Description", -2, HorizontalAlignment.Left);
            }

            // Si on veut afficher des articles
            if(ListViewDisplay == "ARTICLES")
            {
                DAOArticle daoArticle = new DAOArticle();

                List<Article> ListeArticles = new List<Article>();

                // Tous les articles
                if(ListViewCondition == "")
                {
                    ListeArticles = daoArticle.GetAllArticles();
                }
                // Les articles d'une certaine marque
                else if(ListViewCondition == "MARQUE")
                {
                    ListeArticles = daoArticle.GetArticlesWhereMarque(ListViewValue);
                }
                // Les articles d'une certaine sous-famille
                else if(ListViewCondition == "SOUSFAMILLE")
                {
                    ListeArticles = daoArticle.GetArticlesWhereSousFamille(ListViewValue2, ListViewValue);
                }

                // Enfin, on ajoute tous les articles à la ListView
                foreach (Article NewArticle in ListeArticles)
                {
                    ListViewItem Item = new ListViewItem(NewArticle.RefArticle);
                    Item.SubItems.Add(NewArticle.Description);
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
                DAOMarque daoMarque = new DAOMarque();

                List<string> ListeMarques = daoMarque.GetAllMarques();
                foreach(string Marque in ListeMarques)
                {
                    ListView.Items.Add(new ListViewItem(Marque));
                }
            }
            else if(ListViewDisplay == "FAMILLES")
            {
                DAOFamille daoFamille = new DAOFamille();

                List<string> ListeFamilles = daoFamille.GetAllFamilles();
                foreach(string Famille in ListeFamilles)
                {
                    ListView.Items.Add(new ListViewItem(Famille));
                }
            }
            else if(ListViewDisplay == "SOUSFAMILLES")
            {
                DAOSousFamille daoSousFamille = new DAOSousFamille();
                DAOFamille daoFamille = new DAOFamille();

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
                case Keys.Enter:
                    ArticleForm Form;
                    // Ouverture en mode "Création d'article"
                    if (ListView.SelectedItems.Count == 0)
                    {
                        Form = new ArticleForm();
                    }
                    // Ouverture en mode "Modification d'article"
                    else
                    {
                        Form = new ArticleForm(ListView.SelectedItems[0].Text);
                    }
                    Form.Show();
                    break;
                case Keys.Delete:
                    Console.WriteLine("Suppr");
                    break;
                case Keys.F5:
                    RefreshDisplay();
                    break;
                default: break;
            }
            e.Handled = true;
        }

        /// <summary>
        /// Event déclenché lors d'un clic de souris sur une colonne de la ListView.
        /// Trie la ListView en fonction de la colonne sur laquelle on a cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // On récupère la colonne à trier
            ColumnHeader NewSortingColumn = ListView.Columns[e.Column];

            // On récupère l'ordre de tri
            System.Windows.Forms.SortOrder SortOrder;
            if(SortingColumn == null)
            {
                SortOrder = SortOrder.Ascending;
            }
            else
            {
                // Si c'est la même colonne
                if(NewSortingColumn == SortingColumn)
                {
                    // On inverse l'ordre de tri
                    if(SortingColumn.Text.StartsWith("> "))
                    {
                        SortOrder = SortOrder.Descending;
                    }
                    else
                    {
                        SortOrder = SortOrder.Ascending;
                    }
                }
                // Sinon on tri dans l'ordre alphabétique
                else
                {
                    SortOrder = SortOrder.Ascending;
                }

                SortingColumn.Text = SortingColumn.Text.Substring(2);
            }

            // On affiche le nouvel ordre de tri
            SortingColumn = NewSortingColumn;
            if(SortOrder == SortOrder.Ascending)
            {
                SortingColumn.Text = "> " + SortingColumn.Text;
            }
            else
            {
                SortingColumn.Text = "< " + SortingColumn.Text;
            }

            // On applique le Comparer
            ListView.ListViewItemSorter = new ListViewComparer(e.Column, SortOrder);

            // Et on trie
            ListView.Sort();
        }
    }
}
