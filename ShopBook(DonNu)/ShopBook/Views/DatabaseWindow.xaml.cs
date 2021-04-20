using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ShopBook.Entities;
using ShopBook.Entities.Products;
using ShopBook.Entities.Users;
using ShopBook.Services;
using ShopBook.Services.Interface;
using ShopBook.Views;
using ShopBook.Views.CollectionTtable.Product;
using ShopBook.Views.Working_form;

namespace ShopBook
{
    /// <summary>
    /// Логика взаимодействия для DatabaseWindow.xaml
    /// </summary>
    public partial class DatabaseWindow : Window
    {
        Working_form_Product table;
        DataGrid[] masstable;
        People people;
        CollectionBook itemBook;
        CollectionMagazine itemMagazine;
        CollectionChancellery itemChancellery;
        IProductsManagement product;
        IUserManager user;
        public DatabaseWindow()
        {
            InitializeComponent();
            masstable = new DataGrid[3];
            masstable[0] = MainBookTable;
            masstable[1] = MainСhancelleryTable;
            masstable[2] = MainMagazineTable;
            table = new Working_form_Product(masstable);
            product = new ProductsManagement();
            user = new UserManager();
        }


        private void AddItem_Clik(object sender, RoutedEventArgs e)
        {
            AddItem window = new AddItem();
            window.ShowDialog();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            table.Clearing_table();
            Search window = new Search();
            var result = window.ShowDialog();
            table.Synchronization((Product[])result.Item2);
            table.Synchronization((Product[])result.Item3);
            table.Synchronization((Product[])result.Item4);
            table.PopulatingTable(masstable);
        }
        private void Table_mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem item = (ComboBoxItem)e.AddedItems[0];
                table_mode(item.Content.ToString());
            }
            catch
            {

            }
        }
        private void table_mode (string mode)
        {
            if (mode == "Книги")
            { MainBookTable.Visibility = Visibility.Visible; MainMagazineTable.Visibility = Visibility.Collapsed; MainСhancelleryTable.Visibility = Visibility.Collapsed; }
            if (mode == "Журналы")
            { MainBookTable.Visibility = Visibility.Collapsed; MainMagazineTable.Visibility = Visibility.Visible; MainСhancelleryTable.Visibility = Visibility.Collapsed; }
            if (mode == "Концелярия")
            { MainBookTable.Visibility = Visibility.Collapsed; MainMagazineTable.Visibility = Visibility.Collapsed; MainСhancelleryTable.Visibility = Visibility.Visible; }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (User.Peoples is Seller) { SellB.Visibility = Visibility.Visible;            }
            if (User.Peoples is Moderator) { Edit.Visibility = Visibility.Visible; AddItem.Visibility = Visibility.Visible; AddItemD.Visibility = Visibility.Visible;
                Edit1.Visibility = Visibility.Visible; AddItem1.Visibility = Visibility.Visible; AddItemD1.Visibility = Visibility.Visible;}
            if (User.Peoples is Administrator) { Edit.Visibility = Visibility.Visible; AddItem.Visibility = Visibility.Visible; AddItemD.Visibility = Visibility.Visible; Administration.Visibility = Visibility.Visible;
                Edit1.Visibility = Visibility.Visible; AddItem1.Visibility = Visibility.Visible; AddItemD1.Visibility = Visibility.Visible; Administration1.Visibility = Visibility.Visible;}
            table.Update_form(masstable);
            Table_mode.Text = "Книги";
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Edit window = new Edit();
            window.ShowDialog();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            table.Update_form(masstable);
        }

        private void SellB_Click(object sender, RoutedEventArgs e)
        {
            User_change user = null;
            //Seller seller = (Seller)people;
            Seller seller = (Seller)User.Peoples;
            string[] mass = null;
            if (itemBook != null) { user = new User_change(itemBook.Название); seller.set_info_sell(itemBook.Цена, itemBook.Название); mass = new string[] { Table_mode.Text, itemBook.Автор, itemBook.Название, itemBook.Жанр, itemBook.Издатель, itemBook.Материал, itemBook.Расположение, Convert.ToString(itemBook.Цена) }; }
            if (itemMagazine != null) { user = new User_change(itemMagazine.Название); seller.set_info_sell(itemMagazine.Цена, itemMagazine.Название); mass = new string[] { Table_mode.Text, itemMagazine.Название, itemMagazine.Автор, itemMagazine.Тема, itemMagazine.Расположение, itemMagazine.Жанр, itemMagazine.Издатель, Convert.ToString(itemBook.Цена) }; }
            if (itemChancellery != null) { user = new User_change(itemChancellery.Название); seller.set_info_sell(itemChancellery.Цена, itemChancellery.Название); mass = new string[] { Table_mode.Text, itemChancellery.Название, itemChancellery.Расположение, itemChancellery.Производитель, itemChancellery.Категория , Convert.ToString(itemBook.Цена) }; }
            product.ProductAction("Delete", mass);
            user.Show();
            table.Update_form(masstable);
        }

        private void MainMagazineTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid data = (DataGrid)sender;
            if (data.SelectedIndex != -1)
            {
                itemBook = null;
                itemMagazine = null;
                itemChancellery = null;
                if (data.Name == "MainBookTable")
                {
                    itemBook = data.SelectedItem as CollectionBook;
                }
                if (data.Name == "MainMagazineTable")
                {
                    itemMagazine = data.SelectedItem as CollectionMagazine;
                }
                if (data.Name == "MainСhancelleryTable")
                {
                    itemChancellery = data.SelectedItem as CollectionChancellery;
                }    
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (people is Seller)
            {
                Seller seller = (Seller)people;
                string[] mass = seller.get_info_sell();
                string[] mass1 = new string[mass.Length - 1];
                for (int i = 1; i < mass.Length; i++)
                {
                    mass1[i - 1] = mass[i];
                }
                user.UserAction("Change", mass1, Convert.ToDouble(mass[0]), people);
            }
        }
        private string[] user_definition ()
        {
            string[] tempS = null;
            if (people is Client) { Client temp = (Client)people; tempS = temp.For_table(); }
            if (people is Seller) { Seller temp = (Seller)people; tempS = temp.For_table(); }
            if (people is Moderator) { Moderator temp = (Moderator)people; tempS = temp.For_table(); }
            if (people is Administrator) { Administrator temp = (Administrator)people; tempS = temp.For_table(); }
            return tempS;
        }

        private void StatisticsB_Click(object sender, RoutedEventArgs e)
        {
            Statistics statistics = new Statistics();
            statistics.Show();
        }
        private void registration(object sender, RoutedEventArgs e)
        {
            registration registration = new registration();
            registration.Show();
        }
        bool flag = false;

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            if (flag == false)
            {
                MenuM.Width = 269;
            }
            else
            {
                MenuM.Width = 63;
            }
            flag = !flag;
        }

        private void Information_Click(object sender, RoutedEventArgs e)
        {
            people = User.Peoples;
            string[] tempS = user_definition();
            Journal journal = new Journal(tempS);
            journal.ShowDialog();
        }
    }
}
