using Parser.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Services
{
    class LogMenager
    {
        WorkLog workLog;
        public LogMenager (string Path)
        {
            workLog = new WorkLog(Path);
        }
        public string[] DownloadLog()
        {
            return workLog.ReadFile();
        }
    }
}
