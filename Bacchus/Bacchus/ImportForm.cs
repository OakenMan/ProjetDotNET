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
                FileDialog.Filter = "csv files (*.csv)|*.csv";                  // On autorise uniquement les fichiers .csv
                FileDialog.FilterIndex = 0;
                FileDialog.RestoreDirectory = true;

                // Si l'utilisateur choisit un fichier
                if (FileDialog.ShowDialog() == DialogResult.OK)
                {
                    // On récupère le nom de fichier et on l'affiche dans la TextBox
                    FileTextBox.Text = FileDialog.FileName;

                    // On active les boutons d'import
                    OverwriteDataButton.Enabled = true;
                    AppendDataButton.Enabled = true;

                    // ### TODO : parser le fichier directement et détecter puis afficher les erreurs, le nombre d'articles lus, etc
                }
            }
        }
        
        /// <summary>
        /// Parse le fichier pour récupèrer la liste d'articles
        /// </summary>
        /// <param name="FilePath">Le fichier à parser</param>
        /// <returns>Une liste d'articles contenus dans le fichier ciblé par "FilePath"</returns>
        private List<Article> Parser(string FilePath)
        {
            List<Article> ListeArticle = new List<Article>();

            using (var reader = new StreamReader(FilePath))
            {
                // On lit la première ligne pour passer le nom des colonnes
                var line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    // Pour chaque ligne, on sépare les éléments avec le caractère ";"
                    line = reader.ReadLine();
                    var values = line.Split(';');

                    // Et on créé un nouvel article
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
            // On désactive les boutons pour éviter que l'utilisateur reclique dessus
            OverwriteDataButton.Enabled = false;
            AppendDataButton.Enabled = false;

            // On vide la BDD
            // ???

            // On parse le fichier choisi par l'utilisateur pour récupérer la liste d'articles
            List<Article> ListeArticle = Parser(FileTextBox.Text);
            ProgressBar.Maximum = ListeArticle.Count;

            DAO dao = new DAO();

            // On ajoute chaque article à la BDD
            foreach (Article NewArticle in ListeArticle)
            {
                dao.AddArticle(NewArticle.RefArticle, NewArticle.Description, NewArticle.Marque, NewArticle.Famille, NewArticle.SousFamille, NewArticle.PrixHT, NewArticle.Quantite);
                ProgressBar.PerformStep();
            }

            // On affiche un message informatif
            string Message = ListeArticle.Count.ToString() + " ajoutés avec succès à la base de donnée !";
            if(MessageBox.Show(Message, "Succès") == DialogResult.OK)
            {
                this.Close();
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
            // On désactive les boutons pour éviter que l'utilisateur reclique dessus
            AppendDataButton.Enabled = false;
            OverwriteDataButton.Enabled = false;

            // On parse le fichier choisi par l'utilisateur pour récupérer la liste d'articles
            List<Article> ListeArticle = Parser(FileTextBox.Text);
            ProgressBar.Maximum = ListeArticle.Count;

            DAO dao = new DAO();

            // On ajoute chaque article à la BDD (le DAO s'occupe d'ajouter OU de mettre à jour les articles)
            foreach (Article NewArticle in ListeArticle)
            {
                // ### TODO : si on a le temps, faire que AddArticle renvoie un int en fonction de si l'article a été ajouté ou modifié
                // ### de cette manière, on peut afficher dans le MessageBox la proportion d'articles ajoutés/modifiés.
                dao.AddArticle(NewArticle.RefArticle, NewArticle.Description, NewArticle.Marque, NewArticle.Famille, NewArticle.SousFamille, NewArticle.PrixHT, NewArticle.Quantite);
                ProgressBar.PerformStep();
            }

            // On affiche un message informatif
            string Message = ListeArticle.Count.ToString() + " ajoutés avec succès à la base de donnée !";
            if (MessageBox.Show(Message, "Succès") == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
