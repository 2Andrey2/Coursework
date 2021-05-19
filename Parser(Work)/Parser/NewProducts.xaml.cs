using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Parser
{
    /// <summary>
    /// Логика взаимодействия для NewProducts.xaml
    /// </summary>
    public partial class NewProducts : Window
    {
        public NewProducts(string Action)
        {
            InitializeComponent();
            if (Action == "Creature")
            {
                ProductCreationB.Visibility = Visibility.Visible;
                ProductRemovingB.Visibility = Visibility.Collapsed;
            }
            if(Action == "Deletion")
            {
                ProductCreationB.Visibility = Visibility.Collapsed;
                ProductRemovingB.Visibility = Visibility.Visible;
            }
        }
        private void ProductCreationB_Click(object sender, RoutedEventArgs e)
        {
            string[] info = new string[] { ProductTypeT.Text, NumberLinesT.Text, MainColumnsT.Text, PathRezT.Text, TitleT.Text, FormattingT.Text, Convert.ToString(PresenceHeaders.IsChecked) };
            ProductType product = new ProductType();
            ProductWork work = new ProductWork();
            product.infowrite(info);
            work.ProductRecord(product);
        }

        private void ProductRemovingB_Click(object sender, RoutedEventArgs e)
        {
            ProductWork work = new ProductWork();
            ProductType[] mass = work.ReadingProduct();
            for (int i = 0; i < mass.Length; i++)
            {
                if(mass[i].NameProduct == ProductTypeT.Text)
                {
                    mass = mass.Where(val => val.NameProduct != ProductTypeT.Text).ToArray();
                    work.rewriting(mass);
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            PathRezT.Text = dialog.SelectedPath;
        }
    }
}
