using ShopBook.Entities.Products;
using ShopBook.Entities.Products.Additional_products;
using ShopBook.Services;
using ShopBook.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ShopBook.Views.CollectionTtable.Product;
using ShopBook.Views.Working_form;

namespace ShopBook.Views
{
    /// <summary>
    /// Логика взаимодействия для Delete.xaml
    /// </summary>
    public partial class Edit : Window
    {
        Working_form_Product workingtable;
        DataGrid[] masstable;
        string[][] mass = new string[3][];
        int flag = 0;
        TextBox[] massBookObject;
        TextBox[] massMagazineObject;
        TextBox[] massСhancelleryObject;
        public Edit()
        {
            InitializeComponent();
            GrupBook.Visibility = Visibility.Collapsed;
            GrupMagazine.Visibility = Visibility.Collapsed;
            GrupСhancellery.Visibility = Visibility.Collapsed;
            masstable = new DataGrid[3];
            masstable[0] = MainBookTable;
            masstable[1] = MainСhancelleryTable;
            masstable[2] = MainMagazineTable;
            massBookObject = new TextBox[] { AuthorBookT, NameBookT, GenreBookT, ManufacturerBookT, MaterialBookT, StorageBookT, PriceBookT };
            massMagazineObject = new TextBox[] { NameMagazineT, AuthorMagazineT, TopicMagazineT, StorageMagazineT, GenreMagazineT, ManufacturerMagazineT, AudienceMagazineT, PriceMagazineT };
            massСhancelleryObject = new TextBox[] { NameСhancelleryT, StorageСhancelleryT, ManufacturerСhancelleryT, AppointmentСhancelleryT, PriceСhancelleryT };
            workingtable = new Working_form_Product(masstable);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string[] temp = { };
            if (MainBookTable.Visibility == Visibility.Visible)
            {
                CollectionBook item = MainBookTable.SelectedItem as CollectionBook;
                string[] mass = { "Книги", item.Автор, item.Название, item.Жанр, item.Издатель, item.Материал, item.Расположение, Convert.ToString(item.Цена) };
                temp = mass;
            }
            if (MainMagazineTable.Visibility == Visibility.Visible)
            {
                CollectionMagazine item = MainMagazineTable.SelectedItem as CollectionMagazine;
                string[] mass = { "Журнал", item.Название, item.Автор, item.Тема, item.Расположение, item.Жанр, item.Издатель, item.Аудитория, Convert.ToString(item.Цена) };
                temp = mass;
            }
            if (MainСhancelleryTable.Visibility == Visibility.Visible)
            {
                CollectionChancellery item = MainСhancelleryTable.SelectedItem as CollectionChancellery;
                string[] mass = { "Концелярия", item.Название, item.Расположение, item.Производитель, item.Категория, Convert.ToString(item.Цена) };
                temp = mass;
            }
            IProductsManagement product = new ProductsManagement();
            temp = temp.Where(x => x != "").ToArray();
            product.ProductAction("Delete", temp);
            product.ProductAction("Created", workingtable.data_collection(TypeProduct, massBookObject, massMagazineObject, massСhancelleryObject));
        }
        private void SearchEditButton_Click(object sender, RoutedEventArgs e)
        {
            string[] massBook = { "Книги", AuthorBookT.Text, NameBookT.Text, GenreBookT.Text, ManufacturerBookT.Text, MaterialBookT.Text, StorageBookT.Text, PriceBookT.Text };
            string[] massMagazine = { "Журнал", NameMagazineT.Text, AuthorMagazineT.Text, TopicMagazineT.Text, StorageMagazineT.Text, GenreMagazineT.Text, ManufacturerMagazineT.Text, AudienceMagazineT.Text, PriceMagazineT.Text };
            string[] massСhancellery = { "Концелярия", NameСhancelleryT.Text, StorageСhancelleryT.Text, ManufacturerСhancelleryT.Text, AppointmentСhancelleryT.Text, PriceСhancelleryT.Text };
            string[][] massrez = { massBook, massMagazine, massСhancellery };
            for (int i = 0; i < 3; i++)
            {
                string[] temp = massrez[i].Where(x => x != "").ToArray();
                if (temp.Length != 1)
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
                    if (items.All(x => x is Book)) { workingtable.BookTable = (Product[])items; }
                    if (items.All(x => x is Magazine)) { workingtable.MagazineTable = (Product[])items; }
                    if (items.All(x => x is Сhancellery)) { workingtable.СhancelleryTable = (Product[])items; }
                }
            };

            products.SearchComplited += searchHandler;


            for (int i = 0; i < 3; i++)
            {
                if (mass[i] != null)
                {
                    //mass[i] = mass[i].Where(x => x != "").ToArray();
                    products.ProductAction("Search", mass[i]);
                }
            }

            products.SearchComplited -= searchHandler;
            workingtable.PopulatingTable(masstable);
        }

        private void TypeProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem Items = (ComboBoxItem)e.AddedItems[0];
            if (Items.Content.ToString() == "Книги")
            {
                GrupMagazine.Visibility = Visibility.Collapsed;
                GrupBook.Visibility = Visibility.Visible;
                GrupСhancellery.Visibility = Visibility.Collapsed;
                MainBookTable.Visibility = Visibility.Visible; MainMagazineTable.Visibility = Visibility.Collapsed; MainСhancelleryTable.Visibility = Visibility.Collapsed;
            }
            if (Items.Content.ToString() == "Журнал")
            {
                GrupBook.Visibility = Visibility.Collapsed;
                GrupMagazine.Visibility = Visibility.Visible;
                GrupСhancellery.Visibility = Visibility.Collapsed;
                MainBookTable.Visibility = Visibility.Collapsed; MainMagazineTable.Visibility = Visibility.Visible; MainСhancelleryTable.Visibility = Visibility.Collapsed;
            }
            if (Items.Content.ToString() == "Концелярия")
            {
                GrupBook.Visibility = Visibility.Collapsed;
                GrupMagazine.Visibility = Visibility.Collapsed;
                GrupСhancellery.Visibility = Visibility.Visible;
                MainBookTable.Visibility = Visibility.Collapsed; MainMagazineTable.Visibility = Visibility.Collapsed; MainСhancelleryTable.Visibility = Visibility.Visible;
            }
        }

        private void MainBookTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid data = (DataGrid)sender;
            if (data.SelectedIndex != -1)
            {
                if (data.Name == "MainBookTable")
                {
                    CollectionBook item = data.SelectedItem as CollectionBook;
                    NameBookT.Text = item.Название; AuthorBookT.Text = item.Автор; GenreBookT.Text = item.Жанр; ManufacturerBookT.Text = item.Издатель; MaterialBookT.Text = item.Материал; StorageBookT.Text = item.Расположение; PriceBookT.Text = Convert.ToString(item.Цена);
                }
                if (data.Name == "MainMagazineTable")
                {
                    CollectionMagazine item = data.SelectedItem as CollectionMagazine;
                    NameMagazineT.Text = item.Название; AuthorMagazineT.Text = item.Автор; TopicMagazineT.Text = item.Тема; StorageMagazineT.Text = item.Расположение; GenreMagazineT.Text = item.Жанр; ManufacturerMagazineT.Text = item.Издатель; AudienceMagazineT.Text = item.Аудитория; PriceMagazineT.Text = Convert.ToString(item.Цена);
                }
                if (data.Name == "MainСhancelleryTable")
                {
                    CollectionChancellery item = data.SelectedItem as CollectionChancellery;
                    NameСhancelleryT.Text = item.Название; StorageСhancelleryT.Text = item.Расположение; ManufacturerСhancelleryT.Text = item.Производитель; AppointmentСhancelleryT.Text = item.Категория; PriceСhancelleryT.Text = Convert.ToString(item.Цена);
                }
            }
        }
    }
}
