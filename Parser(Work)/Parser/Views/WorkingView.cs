using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Parser.Views
{
    class WorkingView
    {
        ProductType[] massType;
        ProductWork work = new ProductWork();
        public string FolderSelection()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            return dialog.SelectedPath;
        }
        public string FileSelection()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            return dialog.FileName;
        }
        public void UpdatingTypes(System.Windows.Controls.ComboBox ProductComboBox)
        {
            ProductComboBox.Items.Clear();
            massType = work.ReadingProduct();
            if (massType != null)
            {
                for (int i = 0; i < massType.Length; i++)
                {
                    ProductComboBox.Items.Add(massType[i].NameProduct);
                }
            }
        }
        public string[] FillingTypes(SelectionChangedEventArgs e)
        {
            try
            {
                string Selection = e.AddedItems[0].ToString();
                for (int i = 0; i < massType.Length; i++)
                {
                    if (massType[i].NameProduct == Selection)
                    {
                        return new string[] { Convert.ToString(massType[i].CountLine), Convert.ToString(massType[i].CountColumns), massType[i].PathRez,
                        massType[i].Title, massType[i].Formatting, Convert.ToString(massType[i].PresenceHeaders), Convert.ToString(massType[i].Pack)};
                    }
                }
            }
            catch { }
            return null;
        }

    }
}
