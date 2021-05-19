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
        ParserSettings settings;
        public MainStream (ParserSettings parserSettings)
        {
            settings = parserSettings;
        }
        public void RumWork ()
        {
            WorkFile workFile = new WorkFile(settings.Path, settings.PathRez, settings.Title);
            Task[] masstask = new Task[3];
            try
            {
                StreamReader fileR = workFile.OpenFile();
                WorkString[] massstring = new WorkString[masstask.Length];
                int i = 0;
                int j = 0;
                while (true)
                {
                    massstring[i] = new WorkString(settings.Formatting, settings.CountLine, settings.CountColumns);
                    string[] line = workFile.ReadFile(settings.CountLine * settings.CountColumns, settings.TitleBlok);
                    if (line[0] == null)
                    {
                        break;
                    }
                    masstask[i] = new Task(() => massstring[i].BuildingBlock(line, workFile, settings.Parser));
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
