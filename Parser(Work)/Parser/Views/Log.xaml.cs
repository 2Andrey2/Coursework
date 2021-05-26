using Parser.Entities;
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

namespace Parser.Views
{
    /// <summary>
    /// Логика взаимодействия для Log.xaml
    /// </summary>
    public partial class Log : Window
    {
        public Log(RecordingLog[] mass)
        {
            InitializeComponent();
            for (int i = 0; i < mass.Length; i++)
            {
                LogBox.Text += mass[i].Action + mass[i].User + mass[i].Data + Environment.NewLine;
            }
        }
    }
}
