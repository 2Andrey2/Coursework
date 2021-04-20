using ShopBook.Data.FileLogGrup;
using ShopBook.Entities;
using ShopBook.Entities.Users;
using ShopBook.Services;
using ShopBook.Services.Interface;
using ShopBook.Views;
using ShopBook.Views.CollectionTtable.People;
using ShopBook.Views.Working_form;
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

namespace ShopBook
{
    /// <summary>
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Window
    {
        Working_form_People working;
        DataGrid[] masstable;
        TextBox[] massTextbox;
        IUserManager user;
        CollectionClient itemClient;
        CollectionSeller itemSeller;
        CollectionModerator itemModerator;
        CollectionAdministrator itemAdministrator;
        public registration(string reff)
        {
            InitializeComponent();
            if(reff == "Client")
            {
                Change.Visibility = Visibility.Collapsed; Delete.Visibility = Visibility.Collapsed; Refresh.Visibility = Visibility.Collapsed; LogB.Visibility = Visibility.Collapsed; TypePeople.Visibility = Visibility.Collapsed;
                position.Text = "Клиент"; position.IsEnabled = false;
                user = new UserManager();
            }
        }
        public registration()
        {
            InitializeComponent();
            masstable = new DataGrid[4];
            masstable[0] = TableClientMain;
            masstable[1] = TableSellerMain;
            masstable[2] = TableModeratorMain;
            masstable[3] = TableAdministratorMain;
            working = new Working_form_People(masstable);
            massTextbox = new TextBox[] { nameT, SurnameT, middleNameT, addressT, telephoneT, LoginT, PasswordT };
            user = new UserManager();
        }

        private void Further_Click(object sender, RoutedEventArgs e)
        {
            string[] mass = { nameT.Text, SurnameT.Text, middleNameT.Text, addressT.Text, telephoneT.Text, LoginT.Text, PasswordT.Text, position.Text }; 
            user.UserAction("Created", mass);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string[] mass = { LoginT.Text, PasswordT.Text };
            user.UserAction("Delete", mass);
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            string[] mass = null;
            IUserManager user = new UserManager();
            if (position.Text == "Клиент")
            {
                mass = new string[] { itemClient.Логин, itemClient.Пароль };
            }
            if (position.Text == "Продавец")
            {
                mass = new string[] { itemSeller.Логин, itemSeller.Пароль };
            }
            if (position.Text == "Модератор")
            {
                mass = new string[] { itemModerator.Логин, itemModerator.Пароль };
            }
            if (position.Text == "Администратор")
            {
                mass = new string[] { itemAdministrator.Логин, itemAdministrator.Пароль };
            }
            user.UserAction("Delete", mass);
            string[] mass1 = { nameT.Text, SurnameT.Text, middleNameT.Text, addressT.Text, telephoneT.Text, LoginT.Text, PasswordT.Text, position.Text };
            user.UserAction("Created", mass1);
        }

        private void Registration_Loaded(object sender, RoutedEventArgs e)
        {
            if (masstable != null)
            {
                working.Update(masstable);
            }
        }

        private void TypePeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Sales.Visibility = Visibility.Collapsed;
                Purchases.Visibility = Visibility.Collapsed;
                ComboBoxItem Items = (ComboBoxItem)e.AddedItems[0];
                if (Items.Content.ToString() == "Клиенты")
                {
                    TableClientMain.Visibility = Visibility.Visible; TableSellerMain.Visibility = Visibility.Collapsed; TableModeratorMain.Visibility = Visibility.Collapsed; TableAdministratorMain.Visibility = Visibility.Collapsed; Purchases.Visibility = Visibility.Visible;
                }
                if (Items.Content.ToString() == "Продавцы")
                {
                    TableClientMain.Visibility = Visibility.Collapsed; TableSellerMain.Visibility = Visibility.Visible; TableModeratorMain.Visibility = Visibility.Collapsed; TableAdministratorMain.Visibility = Visibility.Collapsed; Sales.Visibility = Visibility.Visible;
                }
                if (Items.Content.ToString() == "Модераторы")
                {
                    TableClientMain.Visibility = Visibility.Collapsed; TableSellerMain.Visibility = Visibility.Collapsed; TableModeratorMain.Visibility = Visibility.Visible; TableAdministratorMain.Visibility = Visibility.Collapsed;
                }
                if (Items.Content.ToString() == "Администраторы")
                {
                    TableClientMain.Visibility = Visibility.Collapsed; TableSellerMain.Visibility = Visibility.Collapsed; TableModeratorMain.Visibility = Visibility.Collapsed; TableAdministratorMain.Visibility = Visibility.Visible;
                }
            }
            catch
            {

            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            working.Update(masstable);
        }

        private void TableSellerMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            itemClient = null;
            itemSeller = null;
            itemModerator = null;
            itemAdministrator = null;
            if (position.Text == "Клиент") { itemClient = TableClientMain.SelectedItem as CollectionClient; }
            if (position.Text == "Продавец") { itemSeller = TableSellerMain.SelectedItem as CollectionSeller; }
            if (position.Text == "Модератор") { itemModerator = TableModeratorMain.SelectedItem as CollectionModerator; }
            if (position.Text == "Администратор") { itemAdministrator = TableAdministratorMain.SelectedItem as CollectionAdministrator;}
            working.Synchronizing_table_fields(sender, massTextbox, TypePeople);
        }

        private void Sales_Click(object sender, RoutedEventArgs e)
        {
            working.Sales_Click();
            Seller seller = (Seller)User.Peoples;
            Journal journal = new Journal(seller.get_info_sell());
            journal.ShowDialog();
        }

        private void LogB_Click(object sender, RoutedEventArgs e)
        {
            FileLog log = new FileLog();
            Journal journal = new Journal(log.Action("Reading"));
            journal.ShowDialog();
        }

        private void Purchases_Click(object sender, RoutedEventArgs e)
        {
            working.Purchases_Click();
            Client client = (Client)User.Peoples;
            Journal journal = new Journal(client.get_ShoppingList());
            journal.ShowDialog();
        }
    }
}
