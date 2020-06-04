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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Change la taille minimum de la section gauche (la TreeView)
            splitContainer1.Panel1MinSize = 200;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Fonction appelée en cliquant sur le menu "Importer"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportForm Form = new ImportForm();
            Form.Show();
        }
    }
}
