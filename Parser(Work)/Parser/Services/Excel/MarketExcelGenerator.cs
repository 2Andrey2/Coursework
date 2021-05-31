using OfficeOpenXml;
using OfficeOpenXml.Style;
using Parser.Entities.Subsidiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Services.Excel
{
    class MarketExcelGenerator
    {
        ExcelPackage package;
        public MarketExcelGenerator()
        {
            package = new ExcelPackage();
        }
        public byte[] Generate()
        {
            return package.GetAsByteArray();
        }
        public void Bildblok (MarketReport report, ParserSettings Settings, int title)
        {
            var sheet = package.Workbook.Worksheets.Add("Лист печати " + title);
            int columsexcel = 2;
            int lineexcel = 2;
            for (int i = 0; i < Settings.CountLine; i++)
            {
                for (int j = 0; j < Settings.CountColumns; j++)
                {
                    sheet.Row(lineexcel).Height = 100;
                    sheet.Cells[lineexcel + 1, columsexcel].Value = report.series[i, j];
                    sheet.Cells[lineexcel + 2, columsexcel].Value = report.number[i, j];
                    lineexcel += 3;
                }
                lineexcel = 2;
                columsexcel++;
            }
            sheet.Cells[lineexcel + (3 * Settings.CountColumns) + 1, 2].Value = report.title;
        }
    }
}
