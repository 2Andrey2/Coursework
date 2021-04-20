using ShopBook.Data.FileConfigGrup;
using ShopBook.Data.FileOperation;
using System;
using System.IO;

namespace ShopBook.Data.FileLogGrup
{
    class FileLog: FileConfig
    {
        public void Action (string Action, string Actionold, string [] mass)
        {

            Array.Resize(ref mass,mass.Length+1);
            mass[mass.Length-1] = Actionold;
            if (Action == "Recording")
            {
                Recording(mass);
            }
        }
        public string[] Action(string Action)
        {
            if (Action == "Reading")
            {
                return Reading();
            }
            return null;
        }
        private void Recording(string [] mass)
        {
            FileStream Logstream;
            string text = "";
            if (File.Exists(PathLog) == true)
            {
                Logstream = new FileStream(PathLog, FileMode.OpenOrCreate, FileAccess.Read);
                text = Encryption.File_decryption_string(Logstream);
                Logstream.Close();
            }
            string rez = string.Join("~", mass);
            text += rez; 
            Logstream = new FileStream(PathLog, FileMode.OpenOrCreate, FileAccess.Write);
            Encryption.File_encryption_string(Logstream, dstEncoding.GetBytes(text));
            Logstream.Close();
        }
        private string[] Reading()
        {
            string text = "";
            using (FileStream fstream = File.OpenRead(PathLog))
            text = Encryption.File_decryption_string(fstream);
            string[] rez = text.Split('~');
            return rez;
        }
    }
}
