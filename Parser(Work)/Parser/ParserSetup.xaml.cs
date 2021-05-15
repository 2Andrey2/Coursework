using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Parser
{
    /// <summary>
    /// Логика взаимодействия для ParserSetup.xaml
    /// </summary>
    public partial class ParserSetup : Page
    {
        string Path;
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
            MainStream stream = new MainStream(Path, Convert.ToInt32(NumberLinesT.Text), Convert.ToInt32(MainColumnsT.Text), PathRezT.Text, TitleT.Text, FormattingT.Text, this);
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
            case
        }
    }
}
