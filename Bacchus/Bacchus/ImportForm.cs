using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bacchus
{
    public partial class ImportForm : Form
    {
        
        public ImportForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fonction appelée en cliquant sur le bouton "Ouvrir"
        /// Ouvre un FileChooser qui permet de choisir un fichier SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            var FilePath = string.Empty;

            using (OpenFileDialog FileDialog = new OpenFileDialog())
            {
                // Paramètres du FileDialog
                FileDialog.InitialDirectory = "C:\\Users\\tom\\Downloads";
                FileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                FileDialog.FilterIndex = 0;
                FileDialog.RestoreDirectory = true;

                // Si l'utilisateur choisit un fichier
                if (FileDialog.ShowDialog() == DialogResult.OK)
                {
                    // On récupère le nom de fichier
                    FilePath = FileDialog.FileName;

                    // Et on l'affiche dans la TextBox
                    FileTextBox.Text = FilePath; 
                }
            }
        }
        
        /// <summary>
        /// Parse le fichier pour récupèrer la liste d'articles
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private List<Article> Parser(string FilePath)
        {
            List<Article> ListeArticle = new List<Article>();

            using (var reader = new StreamReader(FilePath))
            {
                //On lit la première ligne pour passer le nom des colonnes
                var line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    var values = line.Split(';');

                    Article article = new Article
                    {
                        Description = values[0],
                        RefArticle = values[1],
                        Marque = values[2],
                        Famille = values[3],
                        SousFamille = values[4],
                        PrixHT = float.Parse(values[5]),
                        Quantite = 0
                    };

                    ListeArticle.Add(article);
                }
            }

            return ListeArticle;
        }

        /// <summary>
        /// Fonction appelée en cliquant sur le bouton "Écraser les données"
        /// Ajoute les données en écrasant la base de donnée actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverwriteDataButton_Click(object sender, EventArgs e)
        {
            // TODO : vider la BDD

            List<Article> ListeArticle = Parser(FileTextBox.Text);
            Console.WriteLine("Lecture de {0} articles", ListeArticle.Count);

            DAO dao = new DAO();
            foreach(Article NewArticle in ListeArticle) 
            {
                dao.AddArticle(NewArticle.RefArticle, NewArticle.Description, NewArticle.Marque, NewArticle.Famille, NewArticle.SousFamille, NewArticle.PrixHT, NewArticle.Quantite);
            }
        }

        /// <summary>
        /// Fonction appelée en cliquant sur le bouton "Ajouter les données"
        /// Ajoute les données en modifiant la base de donnée actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppendDataButton_Click(object sender, EventArgs e)
        {
            List<Article> ListeArticle = Parser(FileTextBox.Text);
            Console.WriteLine("Lecture de {0} articles", ListeArticle.Count);

            DAO dao = new DAO();
            foreach (Article NewArticle in ListeArticle)
            {
                dao.AddArticle(NewArticle.RefArticle, NewArticle.Description, NewArticle.Marque, NewArticle.Famille, NewArticle.SousFamille, NewArticle.PrixHT, NewArticle.Quantite);
            }
        }
    }
}
