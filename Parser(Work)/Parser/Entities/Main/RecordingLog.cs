using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Entities
{
    [Serializable]
    public class RecordingLog
    {
        public string Action;
        public string User;
        public string Data;
        public RecordingLog(string[] mass)
        {
            Action = mass[0];
            User = mass[1];
            Data = DateTime.Now.ToString();
        }
    }
}
