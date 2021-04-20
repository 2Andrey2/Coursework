using LiveCharts;
using System;
using System.Linq;

namespace ShopBook.Services.Statistics_Grup
{
    class Statistics_Product: Function_elements_SG
    {
        public void Genre_statistics_Count(string type)
        {
            Update();
            string[] rez = null;
            if (type == "Книги")
            {
                rez = new string[BookTable.Length];
                for (int i = 0; i < BookTable.Length; i++)
                {
                    rez[i] = BookTable[i].Maptemp[4];
                }
            }
            if (type == "Концелярия")
            {
                rez = new string[СhancelleryTable.Length];
                for (int i = 0; i < СhancelleryTable.Length; i++)
                {
                    rez[i] = СhancelleryTable[i].Maptemp[5];
                }
            }
            if (type == "Журнал")
            {
                rez = new string[MagazineTable.Length];
                for (int i = 0; i < MagazineTable.Length; i++)
                {
                    rez[i] = MagazineTable[i].Maptemp[6];
                }
            }
            masstype = rez.Distinct().ToArray();
            masscount = new double[masstype.Length];
            for (int i = 0; i < masstype.Length; i++)
            {
                for (int j = 0; j < rez.Length; j++)
                {
                    if (masstype[i] == rez[j])
                    {
                        masscount[i]++;
                    }
                }
            }
        }
        public void Genre_statistics_average_price(string type)
        {
            Update();
            string[] rez = null;
            if (type == "Книги")
            {
                rez = new string[BookTable.Length];
                for (int i = 0; i < BookTable.Length; i++)
                {
                    rez[i] = BookTable[i].Maptemp[4];
                }
                masstype = rez.Distinct().ToArray();
                masscount = new double[masstype.Length];
                int temp = 0;
                int flag = 0;
                for (int i = 0; i < masstype.Length; i++)
                {
                    for (int j = 0; j < BookTable.Length; j++)
                    {
                        if (masstype[i] == BookTable[j].Maptemp[4])
                        {
                            temp += Convert.ToInt32((Convert.ToDouble(BookTable[j].Maptemp[8])));
                            flag++;
                        }
                    }
                    masscount[i] = temp / flag;
                }
            }
            if (type == "Концелярия")
            {
                rez = new string[СhancelleryTable.Length];
                for (int i = 0; i < СhancelleryTable.Length; i++)
                {
                    rez[i] = СhancelleryTable[i].Maptemp[5];
                }
                masstype = rez.Distinct().ToArray();
                masscount = new double[masstype.Length];
                int temp = 0;
                int flag = 0;
                for (int i = 0; i < masstype.Length; i++)
                {
                    for (int j = 0; j < СhancelleryTable.Length; j++)
                    {
                        if (masstype[i] == СhancelleryTable[j].Maptemp[5])
                        {
                            temp += Convert.ToInt32((Convert.ToDouble(СhancelleryTable[j].Maptemp[6])));
                            flag++;
                        }
                    }
                    masscount[i] = temp / flag;
                }
            }
            if (type == "Журнал")
            {
                rez = new string[MagazineTable.Length];
                for (int i = 0; i < MagazineTable.Length; i++)
                {
                    rez[i] = MagazineTable[i].Maptemp[6];
                }
                masstype = rez.Distinct().ToArray();
                masscount = new double[masstype.Length];
                int temp = 0;
                int flag = 0;
                for (int i = 0; i < masstype.Length; i++)
                {
                    for (int j = 0; j < MagazineTable.Length; j++)
                    {
                        if (masstype[i] == MagazineTable[j].Maptemp[6])
                        {
                            temp += Convert.ToInt32((Convert.ToDouble(MagazineTable[j].Maptemp[9])));
                            flag++;
                        }
                    }
                    masscount[i] = temp / flag;
                }
            }
        }
        public SeriesCollection Series { get; private set; }
        public SeriesCollection BuidChart(string title)
        {
            Random randomSeries = new Random();

            Series = new SeriesCollection
            {
                GenerateSeries(title)
            };
            return Series;
        }
    }
}
