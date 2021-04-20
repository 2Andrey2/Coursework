using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ShopBook.Entities.Products;
using ShopBook.Entities.Products.Additional_products;
using ShopBook.Services;

namespace ShopBook.Views
{
    /// <summary>
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        //internal event Action<IEnumerable<Product>> Complited; 

        string[][] mass = new string[3][];
        int flag = 0;
        public Search()
        {
            InitializeComponent();
            GrupBook.Visibility = Visibility.Collapsed;
            GrupMagazine.Visibility = Visibility.Collapsed;
            GrupСhancellery.Visibility = Visibility.Collapsed;
        }

        IEnumerable<Product> ItemsBook;
        IEnumerable<Product> ItemsMagazine;
        IEnumerable<Product> ItemsСhancellery;


        private void SearchItem_Click(object sender, RoutedEventArgs e)
        {
            string[] massBook = { "Книги", NameBookT.Text, AuthorBookT.Text, GenreBookT.Text, ManufacturerBookT.Text, MaterialBookT.Text, StorageBookT.Text, PriceBookT.Text };
            string[] massMagazine = { "Журнал", NameMagazineT.Text, AuthorMagazineT.Text, TopicMagazineT.Text, StorageMagazineT.Text, GenreMagazineT.Text, ManufacturerMagazineT.Text, AudienceMagazineT.Text, PriceMagazineT.Text };
            string[] massСhancellery = { "Концелярия", NameСhancelleryT.Text, StorageСhancelleryT.Text, ManufacturerСhancelleryT.Text, AppointmentСhancelleryT.Text, PriceСhancelleryT.Text };
            string[][] massrez = { massBook, massMagazine, massСhancellery };
            for (int i = 0; i<3;i++)
            {
                string[] temp = massrez[i].Where(x => x != "").ToArray();
                if (massrez[i].Length != 1)
                {
                    mass[flag] = massrez[i];
                    flag++;
                }
            }

            ProductsManagement products = new ProductsManagement();

            Action<IEnumerable<Product>> searchHandler = (items) => 
            {
                if (items != null)
                {
                    if (items.All(x => x is Book)) { ItemsBook = items; }
                    if (items.All(x => x is Magazine)) { ItemsMagazine = items; }
                    if (items.All(x => x is Сhancellery)) { ItemsСhancellery = items; }
                }
            };

            products.SearchComplited += searchHandler;


            for (int i = 0; i < 3; i++)
            {
                string[] temp = mass[i].Where(x => x != "" ).ToArray();
                if (mass[i] != null && temp.Length > 1)
                {
                    products.ProductAction("Search", mass[i]);
                }
            }

            products.SearchComplited -= searchHandler;
            this.Close();
        }

        internal new (bool?, IEnumerable<Product>, IEnumerable<Product>, IEnumerable<Product>) ShowDialog()
        {
            var dialogResult = base.ShowDialog();

            return (dialogResult, ItemsBook, ItemsMagazine, ItemsСhancellery);
        }

        private void TypeProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem Items = (ComboBoxItem)e.AddedItems[0];
            if (Items.Content.ToString() == "Книги")
            {
                GrupMagazine.Visibility = Visibility.Collapsed;
                GrupBook.Visibility = Visibility.Visible;
                GrupСhancellery.Visibility = Visibility.Collapsed;
            }
            if (Items.Content.ToString() == "Журнал")
            {
                GrupBook.Visibility = Visibility.Collapsed;
                GrupMagazine.Visibility = Visibility.Visible;
                GrupСhancellery.Visibility = Visibility.Collapsed;
            }
            if (Items.Content.ToString() == "Концелярия")
            {
                GrupBook.Visibility = Visibility.Collapsed;
                GrupMagazine.Visibility = Visibility.Collapsed;
                GrupСhancellery.Visibility = Visibility.Visible;
            }
        }
    }
}
