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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShopBook.Entities.Products.Interface;
using ShopBook.Services.Interface;
using ShopBook.Entities;
using ShopBook.Services;

namespace ShopBook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void entrance(object sender, RoutedEventArgs e)
        {
            if (LoginT.Text == "Власть") {
                registration registration = new registration();
                registration.Show();
            }
            User.Authorization = false;
            IUserManager user = new UserManager();
            string[] mass = { LoginT.Text, PasswordT.Password };
            user.UserAction("Entrance", mass);
            if (User.Authorization == false)
            {
                MessageBox.Show("Пользователь не найден");
                MessageBoxResult m = MessageBox.Show("Хотите создать клиента?", "Предупреждение", MessageBoxButton.OKCancel);
                if (m == System.Windows.MessageBoxResult.OK)
                {
                    registration registration = new registration("Client");
                    registration.Show();
                }
                return;
            }
            DatabaseWindow window = new DatabaseWindow();
            window.Show();
        }
    }
}
