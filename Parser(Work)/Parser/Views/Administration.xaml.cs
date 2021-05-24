using Parser.Entities;
using Parser.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Parser.Views
{
    /// <summary>
    /// Логика взаимодействия для Administration.xaml
    /// </summary>
    public partial class Administration : Window
    {
        DataGridOperations GridOperations;
        UserManager manager;
        LogMenager LogMenager;
        User userselected;
        public Administration()
        {
            InitializeComponent();
            GridOperations = new DataGridOperations(TableUserMain);
            manager = new UserManager("UserFile.user");
            LogMenager = new LogMenager("Log.bd");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            User[] massrez = manager.SetUserAll();
            if (massrez == null)
            {
                MessageBox.Show("Пользователи не найдены");
                return;
            }
            for (int i = 0; i < massrez.Length; i++)
            {
                GridOperations.AddItem(massrez[i]);
            }
            GridOperations.UpdatingGrid();
        }

        private void Further_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(GetTextBox());
            manager.CreatedUser(user);
        }

        private void TableUserMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid data = (DataGrid)sender;
            userselected = data.SelectedItem as User;
            nameT.Text = userselected.Name;
            SurnameT.Text = userselected.Surname;
            middleNameT.Text = userselected.MiddleName;
            telephoneT.Text = userselected.Telephone;
            LoginT.Text = userselected.Login;
            PasswordT.Text = userselected.Password;
            position.Text = userselected.Position;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (userselected == null) { MessageBox.Show("Выберете пользователя"); }
            manager.DeleteUser(userselected);
            GridOperations.UpdatingGrid();
        }
        private string[] GetTextBox ()
        {
            return new string[] { nameT.Text, SurnameT.Text, middleNameT.Text, telephoneT.Text, LoginT.Text, PasswordT.Text, position.Text };
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            GridOperations.UpdatingGrid();
        }

        private void LogB_Click(object sender, RoutedEventArgs e)
        {
            Log log = new Log(LogMenager.DownloadLog());
            log.ShowDialog();
        }
    }
}
