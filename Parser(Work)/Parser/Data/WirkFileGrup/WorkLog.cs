using Parser.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Parser.Data
{
    class WorkLog
    {
        string Path;
        public WorkLog(string path)
        {
            Path = path;
        }
        public void WriteFile(RecordingLog[] linemass)
        {
            FileStream streamW = new FileStream(Path, FileMode.Create, FileAccess.Write);
            Encryption.File_encryption_object(streamW, Data.Byte.ObjectToByteArray(linemass));
            streamW.Close();
        }
        public RecordingLog[] ReadFile()
        {
            FileStream streamR = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Read);
            if (streamR.Length == 0)
            {
                streamR.Close();
                MessageBox.Show("Лог пуст");
                return null;
            }
            RecordingLog[] rezmass = (RecordingLog[])Data.Byte.ByteArrayToObject(Encryption.File_decryption_object(streamR));
            streamR.Close();
            return rezmass;
        }
    }
}
