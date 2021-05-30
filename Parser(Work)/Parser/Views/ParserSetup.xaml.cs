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
        string[] Path;
        ProductMenager work = new ProductMenager();
        WorkingView view = new WorkingView();
        public ParserSetup(string[] path)
        {
            InitializeComponent();
            ParamG.IsEnabled = false;
            Path = path;
        }
        private void Run_Click(object sender, RoutedEventArgs e)
        {
            int temp = 0;
            for (int i = 0; i < Path.Length; i++)
            {
                temp += System.IO.File.ReadAllLines(Path[i]).Length;
            }
            ProgressB.Maximum = temp;
            ProgressB.Value = 0;
            BarLineAll.Content = ProgressB.Maximum;
            for (int i = 0; i < Path.Length; i++)
            {
                LogMenager log = new LogMenager();
                log.CreateRecord(new string[] { "Создание отчета: " + ProductComboBox.Text + ", " + Path[i], " Выполнил: " + ActiveUser.user.Name + " " + ActiveUser.user.Surname + " " });
                MainStream stream = new MainStream(FormationSettings(i));
                Task task = new Task(() => stream.RumWork());
                task.Start();
            }
        }
        public void UpdateProgressBar(int num)
        {
            ProgressB.Value = ProgressB.Value + num;
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
                NumberLengthT.Text = rezmass[7];
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
        private ParserSettings FormationSettings(int i)
        {
            string RezPath = PathRezT.Text + "/" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.TimeOfDay.Hours + "_" + DateTime.Now.TimeOfDay.Minutes + "_" + ProductComboBox.SelectedItem.ToString() + "("+ i + ")" + "_info.txt";
            return new ParserSettings(Path[i], Convert.ToInt32(NumberLinesT.Text), Convert.ToInt32(MainColumnsT.Text), RezPath, TitleT.Text, FormattingT.Text, this, PresenceHeaders.IsChecked.Value, Convert.ToInt32(PackT.Text), Convert.ToInt32(NumberLengthT.Text));
        }

        private void SearchB_Click(object sender, RoutedEventArgs e)
        {
            Search search = new Search(FormationSettings(0));
            search.ShowDialog();
        }
    }
}
