using Parser.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Data
{
    class WorkUser
    {
        string Path;
        public WorkUser(string path)
        {
            Path = path;
        }
        public void WriteFile(User[] usermass)
        {
            FileStream streamW = new FileStream(Path, FileMode.Create, FileAccess.Write);
            Encryption.File_encryption_object(streamW, Data.Byte.ObjectToByteArray(usermass));
            streamW.Close();
        }
        public User[] ReadFile()
        {
            FileStream streamR = new FileStream("UserFile.user", FileMode.OpenOrCreate, FileAccess.Read);
            if (streamR.Length == 0)
            {
                streamR.Close();
                throw new Exception("not");
            }
            User[] rezmass = (User[])Data.Byte.ByteArrayToObject(Encryption.File_decryption_object(streamR));
            streamR.Close();
            return rezmass;
        }
    }
}
