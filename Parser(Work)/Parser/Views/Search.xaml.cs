using Parser.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        WorkingView view = new WorkingView();
        ParserSettings Settings;
        Regex filtre;
        public Search(ParserSettings settings)
        {
            InitializeComponent();
            Settings = settings;
        }

        private void SearchB_Click(object sender, RoutedEventArgs e)
        {
            filtre = null;
            SearchResultsT.Text = null;
            string tempreg;
            if (CheckBoxBox.IsChecked == true)
            {
                tempreg = @"\[" + NumberT.Text + @"/(\d+)\] " + BoxT.Text + " ";
                filtre = new Regex(tempreg);
            }
            if (CheckBoxAllline.IsChecked == true)
            {
                tempreg = ":" + DormRoomT.Text + @"$";
                filtre = new Regex(tempreg);
            }
            if (CheckBoxline.IsChecked == true)
            {
                tempreg = LineElementT.Text;
                filtre = new Regex(tempreg);
            }
            if (filtre == null)
            {
                MessageBox.Show("Укажите параметры поиска");
                return;
            }
            string[] mass = SearchF(filtre, Settings, CheckBoxline.IsChecked.Value);
            for (int i = 0; i < mass.Length; i++)
            {
                SearchResultsT.Text += mass[i] + Environment.NewLine;
            }
        }

        private string[] SearchF(Regex filtreT, ParserSettings Settings, bool line)
        {
            string[] massrez = new string[Settings.CountLine * Settings.CountColumns + 1];
            WorkFile workFile = new WorkFile(Settings.Path, Settings.PathRez);
            int fulllines = System.IO.File.ReadAllLines(Settings.Path).Length;
            StreamReader fileR = workFile.ReaderRezPathOpen();
            while (true)
            {
                string temp = fileR.ReadLine();
                if (temp == null) { break; }
                MatchCollection matches = filtreT.Matches(temp);
                if (matches.Count > 0)
                {
                    massrez[0] = temp;
                    if (line == false)
                    {
                        for (int j = 1; j < Settings.CountLine; j++)
                        {
                            massrez[j] = fileR.ReadLine();
                        }
                    }
                }
            }
            workFile.ReaderRezPathClose();
            return massrez;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            view.UpdatingTypes(ProductComboBox);
        }
        int flag = 0;
        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] rezmass = view.FillingTypes(e);
            Settings.CountLine = Convert.ToInt32(rezmass[0]);
            Settings.CountColumns = Convert.ToInt32(rezmass[1]);
            if (flag == 0)
            {
                Settings.PathRez = rezmass[2];
            }
            Settings.Title = rezmass[3];
            Settings.Formatting = rezmass[4];
            Settings.TitleBlok = Convert.ToBoolean(rezmass[5]);
            Settings.Pack = Convert.ToInt32(rezmass[6]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings.PathRez = view.FileSelection();
            PathSearchT.Text = Settings.PathRez;
            flag = 1;
        }
    }
}
