using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ShopBook.Data.FileOperation
{
    class Encryption
    {
        const string key = "AVBL2000";
        static public void File_encryption_string (FileStream stream, byte[] text)
        {
            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

            cryptic.Key = ASCIIEncoding.ASCII.GetBytes(key);
            cryptic.IV = ASCIIEncoding.ASCII.GetBytes(key);

            CryptoStream crStream = new CryptoStream(stream, cryptic.CreateEncryptor(), CryptoStreamMode.Write);

            crStream.Write(text, 0, text.Length);
            crStream.Close();
        }
        static public void File_encryption_object(FileStream stream, byte[] text)
        {
            stream.Write(text,0,text.Length);
        }
        static public string File_decryption_string(FileStream stream)
        {
            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

            cryptic.Key = ASCIIEncoding.ASCII.GetBytes(key);
            cryptic.IV = ASCIIEncoding.ASCII.GetBytes(key);

            CryptoStream crStream = new CryptoStream(stream, cryptic.CreateDecryptor(), CryptoStreamMode.Read);

            StreamReader readerAmounts = new StreamReader(crStream);
            string data = readerAmounts.ReadToEnd();
            return data;
        }
        static public byte[] File_decryption_object(FileStream stream, int poz, int size_class)
        {
            BinaryReader reader = new BinaryReader(stream);
            //byte[] datatemp1 = reader.ReadBytes(Convert.ToInt32(stream.Length));
            reader.ReadBytes(poz); // костыль
            byte[] data = reader.ReadBytes(size_class);
            return data;
        }
    }
}
