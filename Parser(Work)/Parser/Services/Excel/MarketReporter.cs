using Parser.Entities.Subsidiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Parser.Services.Excel
{
    class MarketReporter
    {
        public MarketReport GetReport(string[] mass, ParserSettings Settings)
        {
            MarketReport report = new MarketReport();
            report.series = new string[Settings.CountLine,Settings.CountColumns];
            report.number = new string[Settings.CountLine, Settings.CountColumns];
            string[,] masstemp = new string[Settings.CountLine,Settings.CountColumns];
            report.title = mass[0];
            for (int i = 1; i < Settings.CountLine+1; i++)
            {
                string[] temp = mass[i].Split(' ').ToArray();
                temp = temp.Where(x => x != "" && x != " ").ToArray();
                int flag = 0;
                int longlinr = temp.Length / Settings.CountColumns;
                for (int j = 0; j < Settings.CountColumns; j++)
                {
                    if (temp.Length <= flag)
                    {
                        break;
                    }
                    report.series[i-1, j] = temp[flag];
                    report.number[i-1, j] = temp[flag+1];
                    flag += longlinr+1;
                }
            }
            return report;
        }
    }
}
