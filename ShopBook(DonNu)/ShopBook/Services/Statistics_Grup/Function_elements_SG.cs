using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using ShopBook.Views.Working_form;

namespace ShopBook.Services.Statistics_Grup
{
    class Function_elements_SG: Working_form_Product
    {
        public string[] masstype { get; protected set; }
        public double[] masscount { get; protected set; }
        protected void Updating_data()
        {
            Update();
        }
        public LineSeries GenerateSeries(string title)
        {
            ChartValues<ObservablePoint> series = new ChartValues<ObservablePoint>();
            for (int i = 0; i < masscount.Length; i++)
            {
                if (masscount[i] != 0)
                {
                    series.Add(new ObservablePoint(i, masscount[i]));
                }
            }
            var testSeries = new LineSeries
            {
                Title = title,
                Values = series,
                PointGeometry = DefaultGeometries.Square,
                PointGeometrySize = 10
            };
            return testSeries;
        }
    }
}
