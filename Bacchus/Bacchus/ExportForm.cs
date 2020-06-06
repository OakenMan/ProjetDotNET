using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bacchus
{
    public partial class ExportForm : Form
    {
        public ExportForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void CreateCSV(string FilePath, List<Article> ListeArticles)
        {
            Console.WriteLine("création du csv à {0} avec {1} articles", FilePath, ListeArticles.Count);

            FileStream Stream = new FileStream(FilePath, FileMode.OpenOrCreate);
            using (StreamWriter Writer = new StreamWriter(Stream, Encoding.UTF8))
            {
                Writer.WriteLine("Description;Ref;Marque;Famille;Sous-Famille;Prix H.T.");
                foreach(Article NewArticle in ListeArticles)
                {
                    Writer.WriteLine(NewArticle.Description + ";" + NewArticle.RefArticle + ";" + NewArticle.Marque + ";" + NewArticle.Famille + ";" + NewArticle.SousFamille + ";" + NewArticle.PrixHT);
                }
            }

        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            // On désactive le bouton pour éviter que l'utilisateur reclique dessus
            ExportButton.Enabled = false;

            List<Article> ListeArticles = new List<Article>();

            DAO dao = new DAO();

            // Récupère la liste de tous les RefArticle
            List<string> ListeRefArticles = dao.GetAllRefArticles();
            Console.WriteLine("nombre de ref articles = " + ListeRefArticles.Count);
            ProgressBar.Maximum = ListeRefArticles.Count();

            // Pour chaque RefArticle, on récupère l'article correspondant et on l'ajoute à la liste
            foreach(string RefArticle in ListeRefArticles)
            {
                Console.WriteLine("récupération de l'article " + RefArticle);
                ListeArticles.Add(dao.GetArticle(RefArticle));
                ProgressBar.PerformStep();
            }

            var FilePath = string.Empty;

            // On sauvegarde le fichier
            using (SaveFileDialog FileDialog = new SaveFileDialog())
            {
                // Paramètres du FileDialog
                FileDialog.InitialDirectory = "C:\\Users\\tom\\Downloads";
                FileDialog.Filter = "csv files (*.csv)|*.csv";                  // On autorise uniquement les fichiers .csv
                FileDialog.FilterIndex = 0;
                FileDialog.RestoreDirectory = true;

                // Si l'utilisateur choisit un fichier
                if (FileDialog.ShowDialog() == DialogResult.OK)
                {
                    // On créé le fichier CSV à cet endroit
                    CreateCSV(FileDialog.FileName, ListeArticles);

                    // On affiche un message informatif
                    string Message = ListeArticles.Count.ToString() + " article(s) exportés avec succès dans le fichier " + FileDialog.FileName + "!";
                    if (MessageBox.Show(Message, "Exportation réussie") == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
            }
        }
    }
}
