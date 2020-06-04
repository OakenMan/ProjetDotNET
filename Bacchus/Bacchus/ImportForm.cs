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
                }
            }
        }

        /// <summary>
        /// Fonction appelée en cliquant sur le bouton "Écraser les données"
        /// Ajoute les données en écrasant la base de donnée actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverwriteDataButton_Click(object sender, EventArgs e)
        {
            //Read the contents of the file into a stream
            //var fileStream = openFileDialog.OpenFile();

            //using (StreamReader reader = new StreamReader(fileStream))
            //{
            //    fileContent = reader.ReadToEnd();
            //    textBox1.Text = fileContent;
            //    Text = "Editeur de texte [" + openFileDialog.SafeFileName + "]";
            //    FileModified = false;
            //}
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
