using OfficeOpenXml;
using Parser.Entities;
using Parser.Services;
using Parser.Services.Excel;
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
        public Search()
        {
            InitializeComponent();
            Settings = new ParserSettings();
        }
        private void SearchB_Click(object sender, RoutedEventArgs e)
        {
            LogMenager log = new LogMenager();
            log.CreateRecord(new string[] { "Поиск: " + ProductComboBox.Text + ", " + Settings.Path, " Выполнил: " + ActiveUser.user.Name + " " + ActiveUser.user.Surname + " " });
            filtre = null;
            SearchResultsL.Items.Clear();
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
            SearchF(filtre, Settings, CheckBoxline.IsChecked.Value);

        }
        private string[] SearchF(Regex filtreT, ParserSettings Settings, bool line)
        {
            string[] massrez = new string[Settings.CountLine + 1];
            WorkFile workFile = new WorkFile();
            int fulllines = System.IO.File.ReadAllLines(Settings.PathRez).Length;
            workFile.OpenFilePathReader(Settings.PathRez);
            int flagtitle = Settings.CountLine+1;
            string title = "";
            while (true)
            {
                string temp = workFile.ReadLine();
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
                        for (int j = 1; j < Settings.CountLine+1; j++)
                        {
                            massrez[j] = workFile.ReadLine();
                        }
                    }
                    else { massrez[0] = title; massrez[1] = temp; }
                    for (int i = 0; i < massrez.Length; i++)
                    {
                        SearchResultsL.Items.Add(massrez[i]);
                    }
                }
                else { flagtitle++; }
            }
            workFile.CloseFileR();
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
            if (path != "")
            {
                if (GraphicsModeC.IsChecked == true)
                {
                    try
                    {
                        int temp = 0;
                        MarketExcelGenerator generator = new MarketExcelGenerator();
                        int countbloks = SearchResultsL.Items.Count / (Settings.CountLine+1);
                        for (int i = 0; i < countbloks; i++)
                        {
                            string[] mass = new string[Settings.CountLine+1];
                            for (int j = 0; j < Settings.CountLine+1; j++)
                            {
                                mass[j] = SearchResultsL.Items[temp].ToString();
                                temp++;
                            }
                            var reportData = new MarketReporter().GetReport(mass, Settings);
                            generator.Bildblok(reportData, Settings, i);
                        }
                        var reportExcel = generator.Generate();
                        File.WriteAllBytes(path + "/" + "Report.xlsx", reportExcel);
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка! Блоки не обнаружены, или они не полны");
                    }
                }
                else
                {
                    StreamWriter fileW = new StreamWriter(path + "/" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.TimeOfDay.Hours + "_" + DateTime.Now.TimeOfDay.Minutes + "_" + "Search_" + ".txt");
                    for (int i = 0; i < SearchResultsL.Items.Count; i++)
                    {
                        fileW.WriteLine(SearchResultsL.Items[i]);
                    }
                    fileW.Close();
                    LogMenager log = new LogMenager();
                    log.CreateRecord(new string[] { "Вывод поиска в папку: " + path + " Выполнил: " + ActiveUser.user.Name + " " + ActiveUser.user.Surname + " ", "" });
                }
            }
        }
    }
}
