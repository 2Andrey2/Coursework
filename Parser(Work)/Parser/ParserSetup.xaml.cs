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
        ProductType[] massType;
        public ParserSetup(string path)
        {
            InitializeComponent();
            ParamG.IsEnabled = false;
            run.IsEnabled = false;
            Path = path;
        }
        private void Run_Click(object sender, RoutedEventArgs e)
        {
            ProgressB.Value = 0;
            ProgressB.Maximum = System.IO.File.ReadAllLines(Path).Length;
            BarLineAll.Content = ProgressB.Maximum;
            string RezPath = PathRezT.Text + "/" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + ProductComboBox.SelectedItem.ToString() + "_info.txt" ;
            MainStream stream = new MainStream(new ParserSettings(Path, Convert.ToInt32(NumberLinesT.Text), Convert.ToInt32(MainColumnsT.Text), RezPath, TitleT.Text, FormattingT.Text, this, PresenceHeaders.IsChecked.Value));
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
                string Selection = e.AddedItems[0].ToString();
                for (int i = 0; i < massType.Length; i++)
                {
                    if (massType[i].NameProduct == Selection)
                    {
                        NumberLinesT.Text = Convert.ToString(massType[i].CountLine);
                        MainColumnsT.Text = Convert.ToString(massType[i].CountColumns);
                        PathRezT.Text = massType[i].PathRez;
                        TitleT.Text = massType[i].Title;
                        FormattingT.Text = massType[i].Formatting;
                        PresenceHeaders.IsChecked = massType[i].PresenceHeaders;
                    }
                }
                ParamG.IsEnabled = true;
                run.IsEnabled = true;
            }
            catch { }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            NewProducts newProducts = new NewProducts("Creature");
            newProducts.ShowDialog();
            UpdatingTypes();
        }

        private void DeletionItem_Click(object sender, RoutedEventArgs e)
        {
            NewProducts newProducts = new NewProducts("Deletion");
            newProducts.ShowDialog();
            UpdatingTypes();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatingTypes();
        }

        private void UpdatingTypes ()
        {
            ProductComboBox.Items.Clear();
            ProductWork work = new ProductWork();
            massType = work.ReadingProduct();
            if (massType != null)
            {
                for (int i = 0; i < massType.Length; i++)
                {
                    ProductComboBox.Items.Add(massType[i].NameProduct);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            PathRezT.Text = dialog.SelectedPath;
        }
    }
}
