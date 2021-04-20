using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ShopBook.Data.FileOperation;
using ShopBook.Entities.Products;

namespace ShopBook.Data.FileProductGrup
{
    class Function_elements_FProduct: Common_functions
    {
        protected List<int> Map_analysis(string[] mass)
        {
            string temp = "";
            bool Typematch = true;
            int poz = 0;
            int pozCoincidences = 1;
            int pozMassrez = 0;
            int lon = 0;
            bool[] Coincidences = new bool[mass.Where(x => x != "").ToArray().Length];
            List<int> massrez = new List<int>();
            string data = openMap();
            // для тестов
            FileStream stream = new FileStream(Pathnow, FileMode.Open, FileAccess.Read);
            byte[] dataB = Encryption.File_decryption_object(stream, 0, Convert.ToInt32(stream.Length));
            stream.Close();

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == Convert.ToChar("$"))
                {
                    if (poz == 0) // Первый элемент всегда тип товара если не подходит дальше не смотрим
                    {
                        if (temp != mass[0]) { Typematch = false; Coincidences[0] = false; } else { Typematch = true; Coincidences[0] = true; }
                    }
                    if (Typematch == true && poz != 0)
                    {
                        for (int j = 1; j < mass.Length; j++)
                        {
                            if (temp == mass[j] && poz == j+1)
                            {
                                Coincidences[pozCoincidences] = true;
                                pozCoincidences++;
                                break;
                            }
                        }
                    }
                    temp = "";
                    poz++;
                    i++;
                }
                if (data[i] == Convert.ToChar("~"))
                {
                    if (Coincidences.All(s => s == true))// Неправильно работает
                    {
                        massrez.Add(lon);
                        massrez.Add(Convert.ToInt32(temp));
                        pozMassrez++;
                    }
                    for (int j = 0; j < Coincidences.Length; j++)
                    {
                        Coincidences[j] = false;
                    }
                    lon += Convert.ToInt32(temp);
                    poz = 0;
                    temp = "";
                    pozCoincidences = 1;
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
        protected void Adding_database_product(Product objectt)
        {
            Adding_database(objectt, string.Join("$", objectt.Maptemp));
        }
        protected void Delete_Map(string[] mass)
        {
            FileStream streamMap = new FileStream(PathMapnow, FileMode.Open, FileAccess.Read);
            string data = Encryption.File_decryption_string(streamMap);
            string datatemp = "";
            streamMap.Close();
            string temp = "";
            string temp1 = "";
            bool Typematch = true;
            int poz = 0;
            int pozCoincidences = 1;
            int pozMassrez = 0;
            int lon = 0;
            bool[] Coincidences = new bool[mass.Where(x => x != "").ToArray().Length];
            List<int> massrez = new List<int>();
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == Convert.ToChar("$"))
                {
                    if (poz == 0) // Первый элемент всегда тип товара если не подходит дальше не смотрим
                    {
                        if (temp != mass[0]) { Typematch = false; Coincidences[0] = false; } else { Typematch = true; Coincidences[0] = true; }
                    }
                    if (Typematch == true && poz != 0)
                    {
                        for (int j = 1; j < mass.Length; j++)
                        {
                            if (temp == mass[j])
                            {
                                Coincidences[pozCoincidences] = true;
                                pozCoincidences++;
                                break;
                            }
                        }
                    }
                    temp = "";
                    poz++;
                    temp1 += data[i];
                    datatemp += data[i];
                    i++;
                }
                if (data[i] == Convert.ToChar("~"))
                {
                    if (Coincidences.All(s => s == true))// Неправильно работает
                    {
                        massrez.Add(lon);
                        massrez.Add(Convert.ToInt32(temp));
                        pozMassrez++;
                        datatemp = datatemp.Remove(datatemp.Length - temp1.Length);
                    }
                    else { datatemp += "~"; }
                    for (int j = 0; j < Coincidences.Length; j++)
                    {
                        Coincidences[j] = false;
                    }
                    lon += Convert.ToInt32(temp);
                    poz = 0;
                    temp = "";
                    temp1 = "";
                    pozCoincidences = 1;
                    i++;
                    if (i >= data.Length)
                    {
                        break;
                    }
                }
                temp += data[i];
                temp1 += data[i];
                datatemp += data[i];
            }
            streamMap = new FileStream(PathMapnow, FileMode.Create, FileAccess.Write);
            var dstEncoding = Encoding.UTF8;
            byte[] Amounts = dstEncoding.GetBytes(datatemp);
            Encryption.File_encryption_string(streamMap, Amounts);
            streamMap.Close();
        }
    }
}
