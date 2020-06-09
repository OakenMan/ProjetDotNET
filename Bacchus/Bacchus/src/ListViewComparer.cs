using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bacchus
{
    class ListViewComparer : System.Collections.IComparer
    {
        private readonly int ColumnNumber;
        private readonly SortOrder SortOrder;

        public ListViewComparer(int ColumnNumber, SortOrder SortOrder)
        {
            this.ColumnNumber = ColumnNumber;
            this.SortOrder = SortOrder;
        }

        public int Compare(object x, object y)
        {
            // On récupère les 2 objets en "ListViewItem"
            ListViewItem ItemX = x as ListViewItem;
            ListViewItem ItemY = y as ListViewItem;

            // On récupère les sub-items
            string StringX;
            if(ItemX.SubItems.Count <= ColumnNumber)
            {
                StringX = "";
            }
            else
            {
                StringX = ItemX.SubItems[ColumnNumber].Text;
            }

            string StringY;
            if(ItemY.SubItems.Count <= ColumnNumber)
            {
                StringY = "";
            }
            else
            {
                StringY = ItemY.SubItems[ColumnNumber].Text;
            }

            // On les compare
            int Result;
            double DoubleX, DoubleY;

            // Si on essaye de comparer les prix ou la quanité
            if(double.TryParse(StringX, out DoubleX) && double.TryParse(StringY, out DoubleY))
            {
                Result = DoubleX.CompareTo(DoubleY);
            }
            // Sinon (c'est des strings)
            else
            {
                Result = StringX.CompareTo(StringY);
            }

            // Retourne le bon résultat en fonction du SortOrder
            if(SortOrder == SortOrder.Ascending)
            {
                return Result;
            }
            else
            {
                return -Result;
            }
        }
    }
}
