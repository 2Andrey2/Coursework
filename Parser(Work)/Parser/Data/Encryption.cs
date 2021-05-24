using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Parser.Data
{
    static class Encryption
    {
        const string key = "AVBL2000";
        static public void File_encryption_object(FileStream stream, byte[] user)
        {
            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

            cryptic.Key = ASCIIEncoding.ASCII.GetBytes(key);
            cryptic.IV = ASCIIEncoding.ASCII.GetBytes(key);

            CryptoStream crStream = new CryptoStream(stream, cryptic.CreateEncryptor(), CryptoStreamMode.Write);

            crStream.Write(user, 0, user.Length);
            crStream.Close();
        }
        static public byte[] File_decryption_object(FileStream stream)
        {
            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

            cryptic.Key = ASCIIEncoding.ASCII.GetBytes(key);
            cryptic.IV = ASCIIEncoding.ASCII.GetBytes(key);

            CryptoStream crStream = new CryptoStream(stream, cryptic.CreateDecryptor(), CryptoStreamMode.Read);
            BinaryReader reader = new BinaryReader(crStream);
            byte[] data = reader.ReadBytes((int)stream.Length);
            return data;
        }
    }
}
