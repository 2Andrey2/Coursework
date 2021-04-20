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
    /// Логика взаимодействия для Journal.xaml
    /// </summary>
    public partial class Journal : Window
    {
        public Journal(string[] mass)
        {
            InitializeComponent();
            if (mass != null)
            {
                for (int i = 0; i < mass.Length; i++)
                {
                    JournalL.Items.Add(mass[i]);
                }
            }
        }
    }
}
