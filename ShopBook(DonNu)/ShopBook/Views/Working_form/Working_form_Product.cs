using Bogus;
using ShopBook.Entities.Products;
using ShopBook.Entities.Products.Additional_products;
using ShopBook.Services;
using ShopBook.Views.CollectionTtable.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShopBook.Views.Working_form
{
    class Working_form_Product
    {
        public Product[] BookTable;
        public Product[] СhancelleryTable;
        public Product[] MagazineTable;
        ObservableCollection<CollectionBook> connectionBook;
        ObservableCollection<CollectionChancellery> connectionСhancellery;
        ObservableCollection<CollectionMagazine> connectionMagazine;
        private readonly Faker<string[]> faker;
        public Working_form_Product()
        {

        }
        public Working_form_Product(DataGrid[] mass)
        {
            connectionBook = new ObservableCollection<CollectionBook>();
            mass[0].ItemsSource = connectionBook;
            connectionСhancellery = new ObservableCollection<CollectionChancellery>();
            mass[1].ItemsSource = connectionСhancellery;
            connectionMagazine = new ObservableCollection<CollectionMagazine>();
            mass[2].ItemsSource = connectionMagazine;
        }
        public void PopulatingTable(DataGrid[] mass)
        {
            connectionBook.Clear();
            connectionСhancellery.Clear();
            connectionMagazine.Clear();
            if (BookTable != null)
            {
                for (int i = 0; i < BookTable.Length; i++)
                {
                    connectionBook.Add(new CollectionBook { Номер = BookTable[i].Maptemp[1], Название = BookTable[i].Maptemp[3], Автор = BookTable[i].Maptemp[2], Материал = BookTable[i].Maptemp[6], Жанр = BookTable[i].Maptemp[4], Издатель = BookTable[i].Maptemp[5], Расположение = BookTable[i].Maptemp[7], Цена = Convert.ToDouble(BookTable[i].Maptemp[8]) });
                    mass[0].Items.Refresh();
                }
            }
            if (СhancelleryTable != null)
            {
                for (int i = 0; i < СhancelleryTable.Length; i++)
                {
                    connectionСhancellery.Add(new CollectionChancellery { Номер = СhancelleryTable[i].Maptemp[1], Название = СhancelleryTable[i].Maptemp[2], Категория = СhancelleryTable[i].Maptemp[5], Расположение = СhancelleryTable[i].Maptemp[3], Производитель = СhancelleryTable[i].Maptemp[4], Цена = Convert.ToDouble(СhancelleryTable[i].Maptemp[6]) });
                    mass[1].Items.Refresh();
                }
            }
            if (MagazineTable != null)
            {
                for (int i = 0; i < MagazineTable.Length; i++)
                {
                    connectionMagazine.Add(new CollectionMagazine { Номер = MagazineTable[i].Maptemp[1], Название = MagazineTable[i].Maptemp[2], Жанр = MagazineTable[i].Maptemp[6], Тема = MagazineTable[i].Maptemp[4], Аудитория = MagazineTable[i].Maptemp[8], Издатель = MagazineTable[i].Maptemp[7], Автор = MagazineTable[i].Maptemp[3], Расположение = MagazineTable[i].Maptemp[5], Цена = Convert.ToDouble(MagazineTable[i].Maptemp[9]) });
                    mass[2].Items.Refresh();
                }
            }
        }
        public void Update_form(DataGrid[] mass)
        {
            Clearing_table();
            Update();
            PopulatingTable(mass);
        }

        public void Update()
        {
            ProductsManagement products = new ProductsManagement();

            Action<IEnumerable<Product>> searchHandler = (temprez) =>
            {
                Synchronization((Product[])temprez);
            };
            products.LoadAllItem += searchHandler;

            string[] temp = new string[1];
            products.ProductAction("Load_all", temp);

            products.LoadAllItem -= searchHandler;
        }
        public void Synchronization(Product[] mass)
        {
            if (mass != null && mass.Length > 0)
            {
                if (mass.All(x => x is Book)) { BookTable = (Product[])mass; }
                if (mass.All(x => x is Magazine)) { MagazineTable = (Product[])mass; }
                if (mass.All(x => x is Сhancellery)) { СhancelleryTable = (Product[])mass; }
            }
        }
        public string[] data_collection(ComboBox Box, TextBox[] massBoxBook, TextBox[] massBoxMagazine, TextBox[] massBoxСhancellery)
        {

            string[] temp = { };
            if (Box.Text == "Книги")
            {
                string[] mass = { Box.Text, massBoxBook[0].Text, massBoxBook[1].Text, massBoxBook[2].Text, massBoxBook[3].Text, massBoxBook[4].Text, massBoxBook[5].Text, massBoxBook[6].Text };
                temp = mass;
            }
            if (Box.Text == "Журнал")
            {
                string[] mass = { Box.Text, massBoxMagazine[0].Text, massBoxMagazine[1].Text, massBoxMagazine[2].Text, massBoxMagazine[3].Text, massBoxMagazine[4].Text, massBoxMagazine[5].Text, massBoxMagazine[6].Text, massBoxMagazine[7].Text };
                temp = mass;
            }
            if (Box.Text == "Концелярия")
            {
                string[] mass = { Box.Text, massBoxСhancellery[0].Text, massBoxСhancellery[1].Text, massBoxСhancellery[2].Text, massBoxСhancellery[3].Text, massBoxСhancellery[4].Text };
                temp = mass;
            }
            return temp;
        }
        public string[] random_data_collection(ComboBox Box, TextBox[] massBoxBook, TextBox[] massBoxMagazine, TextBox[] massBoxСhancellery)
        {
            try
            {
                var faker = new Faker(locale: "ru");
                string[] temp = { };
                if (Box.Text == "Книги")
                {
                    string[] mass = { Box.Text, faker.Commerce.ProductName(), faker.Name.FirstName(), faker.Hacker.Noun(), faker.Company.CompanyName(), faker.Commerce.ProductMaterial(), faker.Commerce.Department(), faker.Commerce.Price() };
                    temp = mass;
                }
                if (Box.Text == "Журнал")
                {
                    string[] mass = { Box.Text, faker.Commerce.ProductName(), faker.Name.FirstName(), faker.Hacker.Noun(), faker.Commerce.Department(), faker.Hacker.Noun(), faker.Company.CompanyName(), faker.Commerce.Color(), faker.Commerce.Price() };
                    temp = mass;
                }
                if (Box.Text == "Концелярия")
                {
                    string[] mass = { Box.Text, faker.Commerce.ProductName(), faker.Finance.Account(), faker.Company.CompanyName(), faker.Commerce.ProductAdjective(), faker.Commerce.Price() };
                    temp = mass;
                }
                return temp;
            }
            catch
            {
                MessageBox.Show("Провверьте вводимые данные");
                return null;
            }
        }
        public void Clearing_table()
        {
            BookTable = null;
            СhancelleryTable = null;
            MagazineTable = null;
        }
    }
}
