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
        WorkFile(string path, string rezpath)
        {
            Path = path;
            RezPath = rezpath;
        }
        public FileStream[] OpenFile()
        {
            FileStream fileR = new FileStream(Path, FileMode.Open);
            FileStream fileW = new FileStream(Path, FileMode.OpenOrCreate);
            FileStream[] massStream = new FileStream[2];
            massStream[0] = fileR; massStream[1] = fileW;
            return massStream;
        }
        public void CloseFile(FileStream[] massStream)
        {
            massStream[0].Close();
            massStream[1].Close();;
        }
    }
}
