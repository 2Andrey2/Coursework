using Parser.Entities;
using Parser.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

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
            LogMenager = new LogMenager();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridOperations.UpdatingGrid(manager);
        }

        private void Further_Click(object sender, RoutedEventArgs e)
        {
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Вы действиетельно хотите создать пользователя", "Создание пользователя", MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                LogMenager log = new LogMenager();
                User user = new User(GetTextBox());
                manager.CreatedUser(user);
                log.CreateRecord(new string[] { "Создание пользователя: " + nameT.Text + ", " + SurnameT.Text, " Выполнил: " + ActiveUser.user.Name + " " + ActiveUser.user.Surname + " " });
            }
        }

        private void TableUserMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                System.Windows.Controls.DataGrid data = (System.Windows.Controls.DataGrid)sender;
                userselected = data.SelectedItem as User;
                nameT.Text = userselected.Name;
                SurnameT.Text = userselected.Surname;
                middleNameT.Text = userselected.MiddleName;
                telephoneT.Text = userselected.Telephone;
                LoginT.Text = userselected.Login;
                PasswordT.Text = userselected.Password;
                position.Text = userselected.Position;
            }
            catch { }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Вы действиетельно хотите удалить выбраного пользователя", "Удаление пользователя", MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                if (userselected == null) { System.Windows.Forms.MessageBox.Show("Выберете пользователя"); }
                LogMenager log = new LogMenager();
                manager.DeleteUser(userselected);
                GridOperations.UpdatingGrid(manager);
                log.CreateRecord(new string[] { "Удаление пользователя: " + nameT.Text + ", " + SurnameT.Text, " Выполнил: " + ActiveUser.user.Name + " " + ActiveUser.user.Surname + " " });
            }
        }
        private string[] GetTextBox ()
        {
            return new string[] { nameT.Text, SurnameT.Text, middleNameT.Text, telephoneT.Text,
            LoginT.Text, PasswordT.Text, position.Text, LoginT.Text.GetHashCode().ToString(), PasswordT.Text.GetHashCode().ToString() };
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            GridOperations.UpdatingGrid(manager);
        }

        private void LogB_Click(object sender, RoutedEventArgs e)
        {
            RecordingLog[] mass = LogMenager.DownloadLog();
            if (mass != null)
            {
                Log log = new Log(mass);
                log.ShowDialog();
            }
            else
            {
                return;
            }
        }
    }
}
