using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Parser
{
    /// <summary>
    /// Логика взаимодействия для ParserSetup.xaml
    /// </summary>
    public partial class ParserSetup : Page
    {
        string[,] mainmass;
        string[] finmass;
        string Path;
        int LineNumber = 0;
        int ColumnNumber = 0;
        string[] formatlong;
        public ParserSetup(string path)
        {
            InitializeComponent();
            Path = path;
        }
        private void Run_Click(object sender, RoutedEventArgs e)
        {
            Сalculations();
        }
        private void Сalculations()
        {
            LineNumber = 0;
            ColumnNumber = 0;
            formatlong = FormattingT.Text.Split(';');
            string line;
            int NamberPage = 1;
            try
            {
                using (StreamReader fileR = new StreamReader(Path))
                {
                    using (StreamWriter fileW = new StreamWriter(PathRezT.Text, true, System.Text.Encoding.Default))
                    {
                        fileW.WriteLine(TitleT.Text + NamberPage);
                        NamberPage++;
                        mainmass = new string[Convert.ToInt32(NumberLinesT.Text), Convert.ToInt32(MainColumnsT.Text)];
                        line = fileR.ReadLine();
                        while ((line = fileR.ReadLine()) != null)
                        {
                            if (ColumnNumber != Convert.ToInt32(MainColumnsT.Text))
                            {
                                StringConversion(line);
                                if (LineNumber == Convert.ToInt32(NumberLinesT.Text) - 1)
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

                                if (ColumnNumber != 0)
                                {
                                    finmass = new string[mainmass.GetLength(0)];
                                    for (int i = 0; i < mainmass.GetLength(0); i++)
                                    {
                                        for (int j = 0; j < mainmass.GetLength(1); j++)
                                        {
                                            finmass[i] += mainmass[i, j];
                                            if (mainmass.GetLength(1) - 1 != j)
                                            {
                                                finmass[i] += "|";
                                            }
                                            finmass[i] += "   ";
                                        }
                                    }
                                    for (int i = 0; i < finmass.Length; i++)
                                    {
                                        fileW.WriteLine(finmass[i]);
                                    }
                                }
                                fileW.WriteLine(TitleT.Text + NamberPage);
                                NamberPage++;
                                LineNumber = 0;
                                ColumnNumber = 0;
                                mainmass = new string[Convert.ToInt32(NumberLinesT.Text), Convert.ToInt32(MainColumnsT.Text)];
                            }
                        }
                        finmass = new string[mainmass.GetLength(0)];
                        for (int i = 0; i < mainmass.GetLength(0); i++)
                        {
                            for (int j = 0; j < mainmass.GetLength(1); j++)
                            {
                                finmass[i] += mainmass[i, j];
                                if (mainmass.GetLength(1) - 1 != j)
                                {
                                    finmass[i] += "|";
                                }
                                finmass[i] += "   ";
                            }
                        }
                        for (int i = 0; i < finmass.Length; i++)
                        {
                            fileW.WriteLine(finmass[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void StringConversion(string text)
        {
            string rez = "";
            string[] masstext = PartitioningColumns(text);
            if (masstext.Length == formatlong.Length)
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
