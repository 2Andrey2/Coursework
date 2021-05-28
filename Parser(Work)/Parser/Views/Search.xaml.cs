using Parser.Entities;
using Parser.Services;
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
            LogMenager log = new LogMenager();
            log.CreateRecord(new string[] { "Поиск: " + ProductComboBox.Text + ", " + Settings.Path, " Выполнил: " + ActiveUser.user.Name + " " + ActiveUser.user.Surname + " " });
            filtre = null;
            SearchResultsT.Text = null;
            string tempreg;
            if (CheckBoxBox.IsChecked == true)
            {
                tempreg = @"\[" + NumberT.Text + @"/(\d+)\] " + BoxT.Text + " ";
                if (BoxT.Text == "0" || BoxT.Text == "")
                {
                    tempreg = @"\[" + NumberT.Text + @"/(\d+)\] ";
                }
                if (NumberT.Text == "0" || NumberT.Text == "")
                {
                    tempreg = @"\] " + BoxT.Text + " ";
                }
                filtre = new Regex(tempreg);
            }
            if (CheckBoxAllline.IsChecked == true)
            {
                tempreg = ":" + DormRoomT.Text + @"$";
                filtre = new Regex(tempreg);
            }
            if (CheckBoxline.IsChecked == true)
            {
                tempreg = ProductSerialT.Text + @"\s+" + ProductNumberT.Text;
                filtre = new Regex(tempreg);
            }
            if (filtre == null)
            {
                MessageBox.Show("Укажите параметры поиска");
                return;
            }
            string[] mass = SearchF(filtre, Settings, CheckBoxline.IsChecked.Value);
        }

        private string[] SearchF(Regex filtreT, ParserSettings Settings, bool line)
        {
            string[] massrez = new string[Settings.CountLine + 1];
            WorkFile workFile = new WorkFile(Settings.Path, Settings.PathRez);
            int fulllines = System.IO.File.ReadAllLines(Settings.Path).Length;
            StreamReader fileR = workFile.ReaderRezPathOpen();
            int flagtitle = Settings.CountLine+1;
            string title = "";
            while (true)
            {
                string temp = fileR.ReadLine();
                if (temp == null) { break; }
                if (flagtitle == Settings.CountLine+1)
                {
                    title = temp;
                    flagtitle = 0;
                }
                MatchCollection matches = filtreT.Matches(temp);
                if (matches.Count > 0)
                {
                    if (line == false)
                    {
                        massrez[0] = temp;
                        flagtitle = Settings.CountLine;
                        for (int j = 1; j < Settings.CountLine; j++)
                        {
                            massrez[j] = fileR.ReadLine();
                        }
                    }
                    else { massrez[0] = title; massrez[1] = temp; }
                    for (int i = 0; i < massrez.Length; i++)
                    {
                        SearchResultsT.Text += massrez[i] + Environment.NewLine;
                    }
                }
                else { flagtitle++; }
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

        private void SavingResult_Click(object sender, RoutedEventArgs e)
        {
            string path = view.FolderSelection();
            StreamWriter fileW = new StreamWriter(path + "/" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.TimeOfDay.Hours + "_" + DateTime.Now.TimeOfDay.Minutes + "_" + "Search_" + ".txt");
            fileW.Write(SearchResultsT.Text);
            fileW.Close();
        }
    }
}
