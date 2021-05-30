using Parser.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        WorkingView view = new WorkingView();
        public NewProducts(string Action)
        {
            InitializeComponent();
            if (Action == "Creature")
            {
                ProductCreationB.Visibility = Visibility.Visible;
                ProductRemovingB.Visibility = Visibility.Collapsed;
                GrupCreate.IsEnabled = true;
            }
            if(Action == "Deletion")
            {
                ProductCreationB.Visibility = Visibility.Collapsed;
                ProductRemovingB.Visibility = Visibility.Visible;
                GrupCreate.IsEnabled = false;
            }
        }
        private void ProductCreationB_Click(object sender, RoutedEventArgs e)
        {
            if(DataChecking() == false)
            {
                return;
            }
            string[] info = new string[] { ProductTypeT.Text, NumberLinesT.Text, MainColumnsT.Text, PathRezT.Text, TitleT.Text, FormattingT.Text, Convert.ToString(PresenceHeaders.IsChecked), PackT.Text, NumberLengthT.Text };
            ProductType product = new ProductType();
            ProductMenager work = new ProductMenager();
            product.infowrite(info);
            work.ProductRecord(product);
        }

        private void ProductRemovingB_Click(object sender, RoutedEventArgs e)
        {
            ProductMenager work = new ProductMenager();
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
            PathRezT.Text = view.FolderSelection();
        }
        private bool DataChecking()
        {
            try
            {
                Regex regex = new Regex(@"^(\d;)+\d$");
                Convert.ToInt32(NumberLinesT.Text);
                Convert.ToInt32(MainColumnsT.Text);
                Convert.ToInt32(PackT.Text);
                MatchCollection matches = regex.Matches(FormattingT.Text);
                if (matches.Count == 0)
                {
                    throw new Exception("Пробелы введены не верно");
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, ex.StackTrace);
                return false;
            }
        }
    }
}
