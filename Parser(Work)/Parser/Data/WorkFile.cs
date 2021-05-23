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
        public string RezPath;
        StreamReader fileR;
        StreamWriter fileW;
        int NamberPage;
        string Title;
        int Pack;
        int NumberPack;
        int AllBlok;
        int AllPage;
        public WorkFile(string path, string rezpath, string title, int pack, int allblok)
        {
            Path = path;
            RezPath = rezpath;
            NamberPage = 1;
            Title = title;
            Pack = pack;
            NumberPack = 1;
            AllBlok = allblok;
        }
        public WorkFile(string path, string rezpath)
        {
            Path = path;
            RezPath = rezpath;
        }
        public StreamReader OpenFilePath()
        {
            fileR = new StreamReader(Path);
            fileW = new StreamWriter(RezPath);
            return fileR;
        }
        public StreamReader ReaderRezPathOpen()
        {
            fileR = new StreamReader(RezPath);
            return fileR;
        }
        public void ReaderRezPathClose()
        {
            fileR.Close();
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
        public void WriteTitle()
        {
            if (AllBlok / Pack < NumberPack)
            {
                Pack = AllBlok - AllPage;
            }
            AllPage++;
            fileW.WriteLine(Title + "   [" + NamberPage + "/" + Pack + "]" + " " + NumberPack + " All:" + AllPage);
            NamberPage++;
            if (NamberPage > Pack)
            {
                NamberPage = 1;
                NumberPack++;
            }
        }
    }
}