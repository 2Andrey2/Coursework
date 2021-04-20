using ShopBook.Data.FileOperation;
using System;
using System.IO;
using System.Text;

namespace ShopBook.Data.FileConfigGrup
{
    class FileConfig
    {
        // Для User
        protected string Path = @"database\User.datt";
        protected string PathAmounts = @"database\UserMap.datt";
        protected string PathInfo = @"database\Config.datt";
        protected int Luject_size = 0;
        protected int Object_start = 0;
        protected Encoding dstEncoding = Encoding.UTF8;
        // Для Product
        protected string PathBook = @"database\Book.datt";
        protected string PathAddProducts = @"database\AddProducts.datt";
        protected string MapBook = @"database\MapBook.map";
        protected string MapAddProducts = @"database\AddProducts.map";
        protected bool firstcreation = false;
        protected string Pathnow = "";
        protected string PathMapnow = "";
        // Для Log
        protected string PathLog = @"database\Log.datt";
        protected int Changing_total_number_records(string type,string actor)
        {
            bool existPathInfo = File.Exists(PathInfo);
            FileStream config;
            string temp = "";
            string nambersting = "";
            if (existPathInfo == true)
            {
                config = new FileStream(PathInfo, FileMode.OpenOrCreate, FileAccess.Read);
                nambersting = Encryption.File_decryption_string(config);
                config.Close();
                string[] typemass = nambersting.Split(',');
                if (actor == "get")
                {
                    if (type == "Number of users") { return Convert.ToInt32(typemass[0]); }
                    if (type == "Number of records") { return Convert.ToInt32(typemass[1]); }
                }
                if (type == "Number of users") { typemass[0] = Change(actor, Convert.ToInt32(typemass[0])); }
                if (type == "Number of records") { typemass[1] = Change(actor, Convert.ToInt32(typemass[1])); }
                temp = string.Join(",", typemass);
            }
            else { temp = "0,0"; }
            config = new FileStream(PathInfo, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] Amounts = dstEncoding.GetBytes(temp);
            Encryption.File_encryption_string(config, Amounts);
            config.Close();
            return 0;
        }
        private string Change(string actor, int namber)
        {
            if (actor == "+1") { namber++; }
            if (actor == "-1") { namber--; }
            return Convert.ToString(namber);
        }
    }
}
