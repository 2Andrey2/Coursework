using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Parser.ParserSetup;

namespace Parser
{
    class MainStream
    {
        string Path;
        int CountLine;
        int CountColumns;
        string PathRez;
        string Title;
        string Formatting;
        ParserSetup Parser;
        public MainStream (string path, int NumberLines, int MainColumns, string pathRez, string title, string formatting, ParserSetup parser)
        {
            Path = path;
            CountLine = NumberLines;
            CountColumns = MainColumns;
            PathRez = pathRez;
            Title = title;
            Formatting = formatting;
            Parser = parser;
        }
        public void RumWork ()
        {
            WorkFile workFile = new WorkFile(Path, PathRez, Title);
            Task[] masstask = new Task[3];
            try
            {
                StreamReader fileR = workFile.OpenFile();
                WorkString[] massstring = new WorkString[masstask.Length];
                int i = 0;
                int j = 0;
                while ((fileR.ReadLine()) != null)
                {
                    massstring[i] = new WorkString(Formatting, CountLine, CountColumns);
                    string[] line = workFile.ReadFile(CountLine * CountColumns);
                    masstask[i] = new Task(() => massstring[i].BuildingBlock(line, workFile, Parser));
                    masstask[i].Start();
                    if (masstask.Any(x => x == null) == false)
                    {
                        while (true)
                        {
                            if (j == masstask.Length)
                            {
                                j = 0;
                            }
                            if (masstask[j].Status == TaskStatus.RanToCompletion)
                            {
                                i = j;
                                break;
                            }
                            j++;
                        }
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Task.WaitAll(masstask);
                workFile.CloseFile();
            }
        }
    }
}
