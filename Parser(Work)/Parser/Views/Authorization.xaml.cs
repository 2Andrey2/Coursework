using Parser.Entities;
using Parser.Services;
using Parser.Views;
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

namespace Parser
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void entrance_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorizationAttempt() == true)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.ShowDialog();
            }
        }

        private void Administration_Click(object sender, RoutedEventArgs e)
        {
            if (LoginT.Text == "Прораб Андрей" && PasswordT.Password == "Уникальный код")
            {
                Administration administration = new Administration();
                administration.ShowDialog();
                return;
            }
            if (AuthorizationAttempt() == true)
            {
                if (ActiveUser.user.Position == "Администратор")
                {
                    Administration administration = new Administration();
                    administration.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Недостаточно прав");
                }
            }
        }
        private bool AuthorizationAttempt ()
        {
            UserManager userManager = new UserManager("UserFile.user");
            if (userManager.Authorization(new int[] { LoginT.Text.GetHashCode(), PasswordT.Password.GetHashCode() }) == false)
            {
                MessageBox.Show("Ошибка авторизации");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
