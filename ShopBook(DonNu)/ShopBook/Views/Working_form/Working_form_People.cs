using ShopBook.Entities.Users;
using ShopBook.Services;
using ShopBook.Views.CollectionTtable.People;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ShopBook.Views.Working_form
{
    class Working_form_People
    {
        public Client[] ClientTable;
        public Seller[] SellerTable;
        public Moderator[] ModeratorTable;
        public Administrator[] AdministratorTable;
        ObservableCollection<CollectionClient> connectionClient;
        ObservableCollection<CollectionSeller> connectionSeller;
        ObservableCollection<CollectionModerator> connectionModerator;
        ObservableCollection<CollectionAdministrator> connectionAdministrator;
        CollectionClient itemClient;
        CollectionSeller itemSeller;
        CollectionModerator itemModerator;
        CollectionAdministrator itemAdministrator;
        public Working_form_People()
        {

        }
        public Working_form_People(DataGrid[] mass)
        {
            connectionClient = new ObservableCollection<CollectionClient>();
            mass[0].ItemsSource = connectionClient;
            connectionSeller = new ObservableCollection<CollectionSeller>();
            mass[1].ItemsSource = connectionSeller;
            connectionModerator = new ObservableCollection<CollectionModerator>();
            mass[2].ItemsSource = connectionModerator;
            connectionAdministrator = new ObservableCollection<CollectionAdministrator>();
            mass[3].ItemsSource = connectionAdministrator;
        }
        public void Update(DataGrid[] mass)
        {
            Clearing_table();
            UserManager products = new UserManager();

            Action<IEnumerable<People>> searchHandler = (temprez) =>
            {
                Synchronization((People[])temprez);
            };
            products.LoadAllItem += searchHandler;

            string[] temp = new string[1];
            products.UserAction("Load_all", temp);

            products.LoadAllItem -= searchHandler;
            PopulatingTable(mass);
        }
        public void Synchronization(People[] mass)
        {
            ClientTable = new Client[mass.Length];
            SellerTable = new Seller[mass.Length];
            ModeratorTable = new Moderator[mass.Length];
            AdministratorTable = new Administrator[mass.Length];
            if (mass != null && mass.Length > 0)
            {
                for (int i = 0; i < mass.Length; i++)
                {
                    if (mass[i] is Client) { ClientTable[i] = (Client)mass[i]; }
                    if (mass[i] is Seller) { SellerTable[i] = (Seller)mass[i]; }
                    if (mass[i] is Moderator) { ModeratorTable[i] = (Moderator)mass[i]; }
                    if (mass[i] is Administrator) { AdministratorTable[i] = (Administrator)mass[i]; }
                }
            }
            ClientTable = ClientTable.Where(x => x != null).ToArray();
            SellerTable = SellerTable.Where(x => x != null).ToArray();
            ModeratorTable = ModeratorTable.Where(x => x != null).ToArray();
            AdministratorTable = AdministratorTable.Where(x => x != null).ToArray();
        }
        public void Clearing_table()
        {
            ClientTable = null;
            SellerTable = null;
            ModeratorTable = null;
            AdministratorTable = null;
        }
        public void PopulatingTable(DataGrid[] mass)
        {
            connectionClient.Clear();
            connectionSeller.Clear();
            connectionModerator.Clear();
            connectionAdministrator.Clear();
            if (ClientTable != null)
            {
                for (int i = 0; i < ClientTable.Length; i++)
                {
                    string[] temp = ClientTable[i].For_table();
                    connectionClient.Add(new CollectionClient { Имя = temp[0], Фамилия = temp[1], Отчество = temp[2], Логин = temp[5], Пароль = temp[6], Должность = temp[7], Адрес = temp[3], Телефон = temp[4] });
                    mass[0].Items.Refresh();
                }
            }
            if (SellerTable != null)
            {
                for (int i = 0; i < SellerTable.Length; i++)
                {
                    string[] temp = SellerTable[i].For_table();
                    connectionSeller.Add(new CollectionSeller { Имя = temp[0], Фамилия = temp[1], Отчество = temp[2], Логин = temp[5], Пароль = temp[6], Должность = temp[7], Продажи = Convert.ToDouble(temp[8]), Адрес = temp[3], Телефон = temp[4]} );
                    mass[1].Items.Refresh();
                }
            }
            if (ModeratorTable != null)
            {
                for (int i = 0; i < ModeratorTable.Length; i++)
                {
                    string[] temp = ModeratorTable[i].For_table();
                    connectionModerator.Add(new CollectionModerator { Имя = temp[0], Фамилия = temp[1], Отчество = temp[2], Логин = temp[5], Пароль = temp[6], Должность = temp[7], Адрес = temp[3], Телефон = temp[4]});
                    mass[2].Items.Refresh();
                }
            }
            if (AdministratorTable != null)
            {
                for (int i = 0; i < AdministratorTable.Length; i++)
                {
                    string[] temp = AdministratorTable[i].For_table();
                    connectionAdministrator.Add(new CollectionAdministrator { Имя = temp[0], Фамилия = temp[1], Отчество = temp[2], Логин = temp[3], Пароль = temp[4], Должность = temp[5], Адрес = temp[6], Телефон = temp[7] });
                    mass[3].Items.Refresh();
                }
            }
        }
        public void Synchronizing_table_fields(object sender, TextBox[] mass, ComboBox box)
        {
            itemClient = null;
            itemSeller = null;
            itemModerator = null;
            itemAdministrator = null;
            DataGrid data = (DataGrid)sender;
            if (data.SelectedIndex != -1)
            {
                if (data.Name == "TableClientMain")
                {
                    itemClient = data.SelectedItem as CollectionClient;
                    mass[0].Text = itemClient.Имя; mass[1].Text = itemClient.Фамилия; mass[2].Text = itemClient.Отчество; mass[3].Text = itemClient.Адрес; mass[4].Text = itemClient.Телефон; mass[5].Text = itemClient.Логин; mass[6].Text = itemClient.Пароль; box.SelectedIndex = 0 ;
                }
                if (data.Name == "TableSellerMain")
                {
                    itemSeller = data.SelectedItem as CollectionSeller;
                    mass[0].Text = itemSeller.Имя; mass[1].Text = itemSeller.Фамилия; mass[2].Text = itemSeller.Отчество; mass[3].Text = itemSeller.Адрес; mass[4].Text = itemSeller.Телефон; mass[5].Text = itemSeller.Логин; mass[6].Text = itemSeller.Пароль; box.SelectedIndex = 1 ;
                }
                if (data.Name == "TableModeratorMain")
                {
                    itemModerator = data.SelectedItem as CollectionModerator;
                    mass[0].Text = itemModerator.Имя; mass[1].Text = itemModerator.Фамилия; mass[2].Text = itemModerator.Отчество; mass[3].Text = itemModerator.Адрес; mass[4].Text = itemModerator.Телефон; mass[5].Text = itemModerator.Логин; mass[6].Text = itemModerator.Пароль; box.SelectedIndex = 2;
                }
                if (data.Name == "TableAdministratorMain")
                {
                    itemAdministrator = data.SelectedItem as CollectionAdministrator;
                    mass[0].Text = itemAdministrator.Имя; mass[1].Text = itemAdministrator.Фамилия; mass[2].Text = itemAdministrator.Отчество; mass[3].Text = itemAdministrator.Адрес; mass[4].Text = itemAdministrator.Телефон; mass[5].Text = itemAdministrator.Логин; mass[6].Text = itemAdministrator.Пароль; box.SelectedIndex = 3;
                }
            }
        }
        public void Sales_Click()
        {
            string[] mass = null;
            if (itemSeller != null)
            {
                mass = new string[] { itemSeller.Логин, itemSeller.Пароль };
                UserManager user = new UserManager();
                user.UserAction("Entrance", mass);
            }
        }
        public void Purchases_Click()
        {
            string[] mass = null;
            if (itemClient != null)
            {
                mass = new string[] { itemClient.Логин, itemClient.Пароль };
                UserManager user = new UserManager();
                user.UserAction("Entrance", mass);
            }
        }
    }
}
