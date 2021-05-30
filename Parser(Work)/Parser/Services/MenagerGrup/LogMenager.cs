using Parser.Data;
using Parser.Entities;
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
        public LogMenager ()
        {
            workLog = new WorkLog("Log.bd");
        }
        public RecordingLog[] DownloadLog()
        {
            return workLog.ReadFile();
        }
        private void Logging (RecordingLog recording)
        {
            RecordingLog[] logs = DownloadLog();
            RecordingLog[] newlogs;
            if (logs != null)
            {
                newlogs = new RecordingLog[logs.Length + 1];
                for (int i = 0; i < logs.Length; i++)
                {
                    newlogs[i] = logs[i];
                }
            }
            else
            {
                newlogs = new RecordingLog[1];
            }
            newlogs[newlogs.Length - 1] = recording;
            workLog.WriteFile(newlogs);
        }
        public void CreateRecord(string[] info)
        {
            Logging(new RecordingLog(info));
        }
    }
}
