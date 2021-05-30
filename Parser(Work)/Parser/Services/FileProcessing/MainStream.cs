using Parser.Services;
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
    class MainStream : MainStream_dop
    {
        string error;
        public delegate void AccountHandler();
        public event AccountHandler Notify;
        public MainStream(ParserSettings parserSettings) : base(parserSettings)
        {
            Notify += KillThreads;
        }
        public void RumWork()
        {
            int tempnum = 0;
            if (RunDataChecks() == true) { return; }
            ProductMenager workFile = new ProductMenager(settings.Title, settings.Pack, GetNumberBlocks());
            Task[] masstask = new Task[3]; // Ссколько будет использоваться потоков, пока синхронизиции нет 3
            try
            {
                workFile.OpenFilePathR(settings.Path);
                workFile.OpenFilePathW(settings.PathRez);
                WorkString[] massstring = new WorkString[masstask.Length];
                int i = 0;
                int j = 0;
                while (true)
                {
                    massstring[i] = new WorkString(settings.Formatting, settings.CountLine, settings.CountColumns);
                    string[] line = workFile.ReadBlok(settings.CountLine * settings.CountColumns, settings.TitleBlok);
                    if (line[0] == null || error == "stop")
                    {
                        break;
                    }
                    masstask[i] = new Task(() => massstring[i].BuildingBlock(line, workFile, settings.Parser, Notify));
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
                    if (tempnum == 50)
                    {
                        settings.Parser.Dispatcher.BeginInvoke((Action)(() => settings.Parser.UpdateProgressBar((settings.CountLine * settings.CountColumns)*50)));
                        tempnum = 0;
                    }
                    else
                    {
                        tempnum++;
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
                workFile.CloseFilePathR();
                workFile.CloseFilePathW();
            }
        }
        private void KillThreads()
        {
            error = "stop";
        }
    }
}
