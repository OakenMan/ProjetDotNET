using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Bacchus.src.DAOs;

namespace Bacchus
{
    public partial class FormMain : Form
    {
        /********** ATTRIBUTS **********/

        // "Filtres" utilisés pour décider de quoi afficher dans la ListView
        // TODO : à remplacer par des Enums pour plus de propreté/sécurité
        private string ListViewDisplay = "ARTICLES";    // "Catégorie" à afficher : ARTICLES, MARQUES, FAMILLES ou SOUSFAMILLE
        private string ListViewCondition = "";          // "Condition" sur les éléments à afficher : MARQUE/SOUSFAMILLE si on veut afficher des articles, FAMILLE si on veut afficher des sous-familles
        private string ListViewValue = "";              // Valeur de la condition (ex: nom de la marque, de la famille...)
        private string ListViewValue2 = "";             // 2ème champ pour la valeur de la condition, utilisée pour contenir de nom de la famille lorsqu'on veut afficher les articles d'une sous-famille

        private ColumnHeader SortingColumn = null;      // Colonne sur lequelle est appliqué le tri de la ListView

        /// <summary>
        /// Constructeur
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            CenterToScreen();

            splitContainer1.Panel1MinSize = 200;    // Change la taille minimum de la section gauche (la TreeView)
            KeyPreview = true;                      // Pour pouvoir intercepter les inputs clavier

            RefreshDisplay();
        }

        /********** MÉTHODES **********/

        /// <summary>
        /// Actualise l'affichage de l'application (TreeView et ListView)
        /// </summary>
        private void RefreshDisplay()
        {
            RefreshTreeView();
            RefreshListView();
        }

        /// <summary>
        /// Actualise le contenu de la TreeView.
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

            // ----------- AFFICHAGE DES COLONNES -----------
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

            // ----------- AFFICHAGE DES ELEMENTS -----------
            // Si on veut afficher des articles
            if (ListViewDisplay == "ARTICLES")
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

                // On ajoute tous les articles à la ListView
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
            // Si on veut afficher toutes les marques
            else if(ListViewDisplay == "MARQUES")
            {
                DAOMarque daoMarque = new DAOMarque();

                List<string> ListeMarques = daoMarque.GetAllMarques();
                foreach(string Marque in ListeMarques)
                {
                    ListView.Items.Add(new ListViewItem(Marque));
                }
            }
            // Si on veut afficher toutes les familles
            else if(ListViewDisplay == "FAMILLES")
            {
                DAOFamille daoFamille = new DAOFamille();

                List<string> ListeFamilles = daoFamille.GetAllFamilles();
                foreach(string Famille in ListeFamilles)
                {
                    ListView.Items.Add(new ListViewItem(Famille));
                }
            }
            // Si on veut afficher les sous-familles d'une famille
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
        /// Ouvre le menu de création/modification d'élément (Article, Marque, Famille ou Sous-Famille)
        /// </summary>
        private void OpenCreateModifyMenu()
        {
            Form Form = null;

            TreeNode Node = TreeView.SelectedNode;

            // On veut créer/modifier un article
            if (Node.Text == "Tous les articles")
            {
                if (ListView.SelectedItems.Count == 0)
                {
                    Form = new ArticleForm();
                }
                else
                {
                    Form = new ArticleForm(ListView.SelectedItems[0].Text);
                }
            }
            // On veut créer/modifier une famille
            else if(Node.Text == "Familles")
            {
                if (ListView.SelectedItems.Count == 0)
                {
                    Form = new FamilleForm();
                }
                else
                {
                    Form = new FamilleForm(ListView.SelectedItems[0].Text);
                }
            }
            // On veut créer/modifier une marque
            else if (Node.Text == "Marques")
            {
                if (ListView.SelectedItems.Count == 0)
                {
                    Form = new MarqueForm();
                }
                else
                {
                    Form = new MarqueForm(ListView.SelectedItems[0].Text);
                }
            }
            // On veut créer/modifier une sous-famille
            else if (Node.Parent.Text == "Familles")
            {
                if (ListView.SelectedItems.Count == 0)
                {
                    Form = new SousFamilleForm();
                }
                else
                {
                    Form = new SousFamilleForm(Node.Text, ListView.SelectedItems[0].Text);
                }
            }
            // On veut créer/modifier un article
            else
            {
                if (ListView.SelectedItems.Count == 0)
                {
                    Form = new ArticleForm();
                }
                else
                {
                    Form = new ArticleForm(ListView.SelectedItems[0].Text);
                }
            }

            if(Form != null)
            {
                // Affiche la fenêtre
                Form.Show();
            }
        }

        /********** ÉVÉNEMENTS **********/

        /// <summary>
        /// Event déclenché à chaque clic de souris sur un élément de la TreeView.
        /// Affiche les éléments correspondants dans la ListView.
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
            else if (e.Node.Text == "Familles")
            {
                ListViewDisplay = "FAMILLES";
            }
            else if (e.Node.Text == "Marques")
            {
                ListViewDisplay = "MARQUES";
            }
            else if (e.Node.Parent.Text == "Familles")
            {
                ListViewDisplay = "SOUSFAMILLES";
                ListViewValue = e.Node.Text;
            }
            else if (e.Node.Parent.Text == "Marques")
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

        /// <summary>
        /// Event déclenché lorsque l'utilisateur appuie sur une touche.
        /// En fonction de la touche pressée, effectue diverses actions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            // TODO : trouver un moyen d'ignorer la répétition d'inputs
            switch(e.KeyCode)
            {
                // Touche "Entrée" ---> Ouverture de la fenêtre de création/modification
                case Keys.Enter:
                    OpenCreateModifyMenu();
                    break;
                // Touche "Suppr" ---> Ouverture d'un pop-up de confirmation de suppression
                case Keys.Delete:
                    Console.WriteLine("Suppr");
                    break;
                // Touche "F5" ---> Actualise l'affichage de la TreeView et de la ListView
                case Keys.F5:
                    RefreshDisplay();
                    break;
                default: break;
            }
            e.Handled = true;
        }

        /// <summary>
        /// Event déclenché lors d'un clic de souris sur une colonne de la ListView.
        /// Trie la ListView en fonction de la colonne sur laquelle on a cliqué.
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

        /// <summary>
        /// Event déclenché lors d'un double-clic sur un élément de la ListView.
        /// Ouvre la fenêtre de modification de cet élément.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenCreateModifyMenu();
        }

        /// <summary>
        /// Event déclenché en cliquant sur le menu "Importer".
        /// Affiche une fenêtre qui permet d'importer un fichier CSV dans la BDD.
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
        /// Affiche une fenêtre qui permet d'exporter la BDD dans un fichier CSV.
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
        /// Actualise la TreeView et la ListView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void actualiserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshDisplay();
        }
    }
}
