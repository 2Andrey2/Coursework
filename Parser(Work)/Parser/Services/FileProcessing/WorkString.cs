using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static Parser.MainStream;
using static Parser.ParserSetup;

namespace Parser
{
    class WorkString
    {
        int countLine;
        int countColumns;
        string[] formatlong;
        static object lockerW = new object();
        public WorkString(string formatting, int CountLine, int CountColumns)
        {
            countLine = CountLine;
            countColumns = CountColumns;
            formatlong = formatting.Split(';');
        }
        public void BuildingBlock(string[] massline, ProductMenager workFile, ParserSetup parser, AccountHandler Notify)
        {
            string[,] mainmass = new string[countLine, countColumns];
            int LineNumber = 0;
            int ColumnNumber = 0;
            try
            {
                if (massline.Any(x => x == null) == true) { return; }
                for (int i = 0; i < massline.Length; i++)
                {
                    if (ColumnNumber != Convert.ToInt32(countColumns))
                    {
                        StringConversion(massline[i], LineNumber, ColumnNumber, mainmass);
                        if (LineNumber == Convert.ToInt32(countLine) - 1)
                        {
                            LineNumber = 0;
                            ColumnNumber++;
                        }
                        else
                        {
                            LineNumber++;
                        }
                    }
                    else
                    {
                        LineNumber = 0;
                        ColumnNumber = 0;
                    }
                }

                string[] finmass = new string[mainmass.GetLength(0)];
                for (int j = 0; j < mainmass.GetLength(0); j++)
                {
                    for (int z = 0; z < mainmass.GetLength(1); z++)
                    {
                        finmass[j] += mainmass[j, z];
                        if (mainmass.GetLength(1) - 1 != z)
                        {
                            finmass[j] += "|";
                        }
                        finmass[j] += "   ";
                    }
                }
                lock (lockerW)
                {
                    workFile.WriteTitle();
                    workFile.WriteLineMass(finmass);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.StackTrace);
                Notify?.Invoke();
            }
        }
        private void StringConversion(string text, int LineNumber, int ColumnNumber, string[,] mainmass)
        {
            string rez = "";
            string[] masstext = PartitioningColumns(text);
            if (masstext.Length == formatlong.Length || masstext.Length == 0)
            {
                for (int i = 0; i < masstext.Length; i++)
                {
                    rez += masstext[i];
                    for (int j = 0; j < Convert.ToInt32(formatlong[i]); j++)
                    {
                        rez += " ";
                    }
                }
            }
            else
            {
                throw new Exception("Указаное количество пробелов не верно");
            }
            mainmass[LineNumber, ColumnNumber] = rez;
        }
        private string[] PartitioningColumns(string text)
        {
            string[] masstext = text.Split(' ');
            masstext = masstext.Where<string>(x => x != "").ToArray<string>();
            return masstext;
        }
    }
}
