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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using ShopBook.Services.Statistics_Grup;

namespace ShopBook.Views
{
    /// <summary>
    /// Логика взаимодействия для Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public ChartValues<double> Points { get; set; }
        Statistics_Product randomSeries;
        public string[] Labels { get; set; }
        string mode;
        public Statistics()
        {
            InitializeComponent();
        }

        private void Building_Click(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            randomSeries = new Statistics_Product();
            SeriesCollection = new SeriesCollection();
            Points = new ChartValues<double>();
            if (mode == "Книги")
            {
                if (Pay.IsChecked == true)
                {
                    randomSeries.Genre_statistics_Count(mode);
                    SeriesCollection.AddRange(randomSeries.BuidChart("Кол-во"));
                }
                if (Count.IsChecked == true)
                {
                    randomSeries.Genre_statistics_average_price(mode);
                    SeriesCollection.AddRange(randomSeries.BuidChart("Средняя цена"));
                }
                Labels = randomSeries.masstype;
            }
            if (mode == "Концелярия")
            {
                if (Pay.IsChecked == true)
                {
                    randomSeries.Genre_statistics_Count(mode);
                    SeriesCollection.AddRange(randomSeries.BuidChart("Кол-во"));
                }
                if (Count.IsChecked == true)
                {
                    randomSeries.Genre_statistics_average_price(mode);
                    SeriesCollection.AddRange(randomSeries.BuidChart("Средняя цена"));
                }
                Labels = randomSeries.masstype;
            }
            if (mode == "Журнал")
            {
                if (Pay.IsChecked == true)
                {
                    randomSeries.Genre_statistics_Count(mode);
                    SeriesCollection.AddRange(randomSeries.BuidChart("Кол-во"));
                }
                if (Count.IsChecked == true)
                {
                    randomSeries.Genre_statistics_average_price(mode);
                    SeriesCollection.AddRange(randomSeries.BuidChart("Средняя цена"));
                }
                Labels = randomSeries.masstype;
            }
            DataContext = this;
        }

        private void TypeProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)e.AddedItems[0];
            mode = item.Content.ToString();
        }
    }
}
