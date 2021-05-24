using Parser.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void UpdatingGrid()
        {
            DataGrid.Items.Refresh();
        }
    }
}
