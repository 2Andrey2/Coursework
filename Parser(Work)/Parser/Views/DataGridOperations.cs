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

namespace Parser.Views
{
    class DataGridOperations
    {
        ObservableCollection<User> connectionUser;
        DataGrid DataGrid;
        public DataGridOperations(DataGrid dataGrid)
        {
            connectionUser = new ObservableCollection<User>();
            DataGrid = dataGrid;
            DataGrid.ItemsSource = connectionUser;
        }
        public void AddItem(User user)
        {
            connectionUser.Add(user);
        }
        public void UpdatingGrid(UserManager manager)
        {

            connectionUser.Clear();
            User[] massrez = manager.SetUserAll();
            if (massrez == null)
            {
                MessageBox.Show("Пользователи не найдены");
                return;
            }
            for (int i = 0; i < massrez.Length; i++)
            {
                AddItem(massrez[i]);
            }
            DataGrid.Items.Refresh();
        }
    }
}
