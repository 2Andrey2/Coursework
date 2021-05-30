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
        StreamReader fileR;
        StreamWriter fileW;
        public WorkFile()
        {

        }
        public void OpenFilePathReader(string Path)
        {
            fileR = new StreamReader(Path);
        }
        public void OpenFilePathWriter(string Path)
        {
            fileW = new StreamWriter(Path);
        }
        public string ReadLine()
        {
            return fileR.ReadLine();
        }
        public void WriteLineMass(string[] line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                fileW.WriteLine(line[i]);
            }
        }
        public void CloseFileR()
        {
            fileR.Close();
        }
        public void CloseFileW()
        {
            fileW.Close();
        }
    }
}