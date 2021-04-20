using ShopBook.Data.FileConfigGrup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ShopBook.Data.FileOperation
{
    class Common_functions : FileConfig
    {
        protected string openMap()
        {
            string data = "";
            FileStream streamMap = null;
            try
            {
                streamMap = new FileStream(PathMapnow, FileMode.Open, FileAccess.Read);
                data = Encryption.File_decryption_string(streamMap);
            }
            catch
            {
                return data;
            }
            finally
            {
                streamMap.Close();
            }
            return data;
        }
        protected List<int> All_Map()
        {
            List<int> massrez = new List<int>();
            string data = openMap();
            string temp = "";
            int lon = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == Convert.ToChar("$"))
                {
                    temp = "";
                    i++;
                }
                if (data[i] == Convert.ToChar("~"))
                {
                    massrez.Add(lon);
                    massrez.Add(Convert.ToInt32(temp));
                    lon += Convert.ToInt32(temp);
                    temp = "";
                    i++;
                    if (i == data.Length)
                    {
                        return massrez;
                    }
                }
                temp = temp + data[i];
            }
            return massrez;
        }

        protected void Adding_database(object objectt, string text)
        {
            FileStream streamMap;
            string AmountsS = "";
            if (firstcreation == false)
            {
                AmountsS = openMap();
            }
            byte[] data = FileOperation.Byte.ObjectToByteArray(objectt);
            streamMap = new FileStream(PathMapnow, FileMode.Create, FileAccess.Write);
            FileStream streamFile = new FileStream(Pathnow, FileMode.Append, FileAccess.Write);
            AmountsS = AmountsS + text + "$" + Convert.ToString(data.Length) + "~";
            byte[] Amounts = dstEncoding.GetBytes(AmountsS);
            Encryption.File_encryption_string(streamMap, Amounts);
            Encryption.File_encryption_object(streamFile, data);
            streamFile.Close();
            streamMap.Close();
        }
        protected bool Delete_database(List<int> mass)
        {
            if (firstcreation == true)
            {
                MessageBox.Show("Файлов для удаления не найдено");
                return false;
            }
            if (mass.Count == 0)
            {
                MessageBox.Show("Аккаунт для удаления не найден");
                return false;
            }
            FileStream stream = new FileStream(Pathnow, FileMode.Open, FileAccess.Read);
            byte[] dataB = Encryption.File_decryption_object(stream, 0, Convert.ToInt32(stream.Length));
            stream.Close();
            int longg = 0;
            for (int i = 0; i < mass.Count; i = i + 2)
            {
                longg += mass[i + 1];
            }
            byte[] data = new byte[dataB.Length - longg];
            int longg1 = 0;
            int longg2 = 0;
            int endd = 0;
            int Begin = 0;
            for (int i = 0; i <= mass.Count; i = i + 2)
            {
                if (i < mass.Count)
                {
                    endd = mass[i] - endd;
                    longg2 = endd;
                    Array.Copy(dataB, Begin, data, longg1, longg2);
                    Begin = mass[i] + mass[i + 1];
                    longg1 += longg2;
                    endd = mass[i] + mass[i + 1];
                }
                else
                {
                    endd = dataB.Length - endd;
                    longg2 = endd;
                    Array.Copy(dataB, Begin, data, longg1, longg2);
                }
            }
            stream = new FileStream(Pathnow, FileMode.Create, FileAccess.Write);
            Encryption.File_encryption_object(stream, data);
            stream.Close();
            return true;
        }
    }
}
