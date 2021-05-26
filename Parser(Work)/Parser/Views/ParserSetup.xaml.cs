using Parser.Entities;
using Parser.Services;
using Parser.Views;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Parser
{
    /// <summary>
    /// Логика взаимодействия для ParserSetup.xaml
    /// </summary>
    public partial class ParserSetup : Page
    {
        string Path;
        ProductWork work = new ProductWork();
        WorkingView view = new WorkingView();
        public ParserSetup(string path)
        {
            InitializeComponent();
            ParamG.IsEnabled = false;
            Path = path;
        }
        private void Run_Click(object sender, RoutedEventArgs e)
        {
            LogMenager log = new LogMenager();
            log.CreateRecord(new string[] { "Создание отчета: " + ProductComboBox.Text + ", " + Path, " Выполнил: " + ActiveUser.user.Name + " " + ActiveUser.user.Surname + " "});
            ProgressB.Value = 0;
            ProgressB.Maximum = System.IO.File.ReadAllLines(Path).Length;
            BarLineAll.Content = ProgressB.Maximum;
            MainStream stream = new MainStream(FormationSettings());
            Task task = new Task(() => stream.RumWork());
            task.Start();
        }
        public void UpdateProgressBar()
        {
            ProgressB.Value = ProgressB.Value + (Convert.ToInt32(NumberLinesT.Text) * Convert.ToInt32(MainColumnsT.Text));
            BarLineNau.Content = ProgressB.Value;
        }

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string[] rezmass = view.FillingTypes(e);
                NumberLinesT.Text = rezmass[0];
                MainColumnsT.Text = rezmass[1];
                PathRezT.Text = rezmass[2];
                TitleT.Text = rezmass[3];
                FormattingT.Text = rezmass[4];
                PresenceHeaders.IsChecked = Convert.ToBoolean(rezmass[5]);
                PackT.Text = rezmass[6];
                ParamG.IsEnabled = true;
            }
            catch { }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            NewProducts newProducts = new NewProducts("Creature");
            newProducts.ShowDialog();
            view.UpdatingTypes(ProductComboBox);
        }

        private void DeletionItem_Click(object sender, RoutedEventArgs e)
        {
            NewProducts newProducts = new NewProducts("Deletion");
            newProducts.ShowDialog();
            view.UpdatingTypes(ProductComboBox);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            view.UpdatingTypes(ProductComboBox);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PathRezT.Text = view.FolderSelection();
        }
        private ParserSettings FormationSettings()
        {
            string RezPath = PathRezT.Text + "/" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + ProductComboBox.SelectedItem.ToString() + "_info.txt";
            return new ParserSettings(Path, Convert.ToInt32(NumberLinesT.Text), Convert.ToInt32(MainColumnsT.Text), RezPath, TitleT.Text, FormattingT.Text, this, PresenceHeaders.IsChecked.Value, Convert.ToInt32(PackT.Text), Convert.ToInt32(NumberLengthT.Text));
        }

        private void SearchB_Click(object sender, RoutedEventArgs e)
        {
            Search search = new Search(FormationSettings());
            search.ShowDialog();
        }
    }
}
