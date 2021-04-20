using ShopBook.Entities;
using ShopBook.Entities.Users;
using ShopBook.Services;
using ShopBook.Services.Interface;
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

namespace ShopBook.Views
{
    /// <summary>
    /// Логика взаимодействия для User_change.xaml
    /// </summary>
    public partial class User_change : Window
    {
        string textrez;
        public User_change(string text)
        {
            InitializeComponent();
            textrez = text;
        }
        private void Choice_Click(object sender, RoutedEventArgs e)
        {
            string[] mass = new string[] { LoginT.Text };
            IUserManager user = new UserManager();
            user.UserAction("Entrance", mass);
            user.UserAction("Change", textrez);
            this.Close();
        }

        private void Anonymous_Click(object sender, RoutedEventArgs e)
        {
            User.Peoples = null;
            this.Close();
        }
    }
}
