using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class WorkFile
    {
        string Path;
        string RezPath;
        StreamReader fileR;
        StreamWriter fileW;
        int NamberPage;
        string Title;
        public WorkFile(string path, string rezpath, string title)
        {
            Path = path;
            RezPath = rezpath;
            NamberPage = 1;
            Title = title;
        }
        public StreamReader OpenFile()
        {
            fileR = new StreamReader(Path);
            fileW = new StreamWriter(RezPath);
            return fileR;
        }
        public void CloseFile()
        {
            fileR.Close();
            fileW.Close();
        }
        public string[] ReadFile (int CountLine, bool Title)
        {
            if (Title == true)
            {
                fileR.ReadLine();
            }
            string[] massline = new string[CountLine];
            for (int i = 0; i < massline.Length; i++)
            {
                massline[i] = fileR.ReadLine();
            }
            return massline;
        }
        public void WriteFile(string[] massline)
        {
            for (int i = 0; i < massline.Length; i++)
            {
                fileW.WriteLine(massline[i]);
            }
        }
        public void WriteTitle ()
        {
            fileW.WriteLine(Title + NamberPage);
            NamberPage++;
        }
    }
}