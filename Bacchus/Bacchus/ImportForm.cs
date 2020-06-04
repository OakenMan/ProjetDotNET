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

struct Article
{
    public string RefArticle;
    public string Description;
    public string Famille;
    public int RefFamille;
    public string SousFamille;
    public int RefSousFamille;
    public string Marque;
    public int RefMarque;
    public string PrixHT;
    public string Quantite;

    public override String ToString()
    {
        return String.Format("Description : {0} || Ref : {1} || Marque : {2} || Famille : {3} || Sous-Famille : {4} || Prix : {5} ", Description, RefArticle, Marque, Famille, SousFamille, PrixHT);
    }
}

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

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    FilePath = openFileDialog.FileName;

                    FileTextBox.Text = FilePath;

                    List<Article> ListeArticle = Parser(FilePath);
                    //foreach (Article article in ListeArticle)
                    //{
                    //    Console.WriteLine(article);
                    //}
                }
            }
        }
        
        /// <summary>
        /// 
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

                    Article article = new Article();
                    article.Description = values[0];
                    article.RefArticle = values[1];
                    article.Marque = values[2];
                    article.Famille = values[3];
                    article.SousFamille = values[4];
                    article.PrixHT = values[5];

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
            string DatabasePath = "Data Source = Bacchus.SQLite;";
            string SQLCommand = "INSERT INTO Articles (RefArticle, Description, RefSousFamille, RefMarque, PrixHT, Quantite) " +
                "VALUES('F6456', 'description', 456, 789, 2.99, 10);";

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                SQLiteCommand Command = new SQLiteCommand(SQLCommand, Connection);
                Connection.Open();

                int result = Command.ExecuteNonQuery();
            }
            //using (StreamReader reader = new StreamReader(fileStream))
            //{
            //    fileContent = reader.ReadToEnd();
            //    textBox1.Text = fileContent;
            //    Text = "Editeur de texte [" + openFileDialog.SafeFileName + "]";
            //    FileModified = false;
            //}



            //Algorithme :
            /*
             * A partir de la liste des articles obtenue grâce au parser :
             * Pour chaque article :
             * 
             *  Lecture de la marque dans la BDD
                Si elle existe:
                    On récupère l'ID de la marque et on le stock dans article.RefMarque
                Sinon: 
                    On créer l'entrée dans la table "Marque" de la BDD
                    On récupère l'ID de la marque et on le stock dans article.RefMarque

                Appliquer l'algorithme précédant pour Famille

                Lecture de la sous famille dans la BDD
                Si elle existe:
                    On récupère l'ID de la sous famille et on le stock dans article.RefSousFamille
                Sinon: 
                    On créer l'entrée dans la table "SousFamilles" de la BDD avec l'aide de la valeur article.RefFamille récupérée précédemment
                    On récupère l'ID de la sous famille et on le stock dans article.RefSousFamille

                On convertit le string du prix en float

                On ajoute l'article à la BDD
            */
        }

        /// <summary>
        /// Fonction appelée en cliquant sur le bouton "Ajouter les données"
        /// Ajoute les données en modifiant la base de donnée actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppendDataButton_Click(object sender, EventArgs e)
        {

        }
    }
}
