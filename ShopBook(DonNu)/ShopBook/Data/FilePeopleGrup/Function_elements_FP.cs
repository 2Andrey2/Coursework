using ShopBook.Data.FileOperation;
using ShopBook.Entities.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ShopBook.Data.FilePeopleGrup
{
    class Function_elements_FP: Common_functions
    {
        protected List<int> Map_analysis(string[] mass)
        {
            string data = openMap();
            if (data == "")
            {
                return null;
            }
            string temp = "";
            int poz = 0;
            bool[] coincidences;
            if (mass.Length == 2) { coincidences = new bool[2]; } else { coincidences = new bool[1]; }
            int lon = 0;
            List<int> massrez = new List<int>();
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == Convert.ToChar("$"))
                {
                    if (temp == mass[0] && poz == 0) { coincidences[0] = true; }
                    if (coincidences.Length == 2) { if (temp == mass[1] && poz == 1) { coincidences[1] = true; } }
                    temp = "";
                    poz++;
                    i++;
                }
                if (data[i] == Convert.ToChar("~"))
                {
                    if (coincidences.All( x => x == true ))
                    {
                        massrez.Add(lon);
                        massrez.Add(Convert.ToInt32(temp));
                        break;
                    }
                    else
                    {
                        for (int j = 0; j < coincidences.Length; j++)
                        {
                            coincidences[j] = false;
                        }
                    }
                    lon += Convert.ToInt32(temp);
                    poz = 0;
                    temp = "";
                    i++;
                    if (i == data.Length)
                    {
                        return null;
                    }
                }
                temp = temp + data[i];
            }
            return massrez;
        }

        protected void Adding_database_people (People objectt)
        {
            string[] massLP = objectt.get_Login_Password();
            Adding_database(objectt, string.Join("$", massLP[0] + "$" + massLP[1]));
        }
        protected void Delete_Map (string[] mass)
        {
            FileStream streamAmounts = new FileStream(PathAmounts, FileMode.OpenOrCreate, FileAccess.Read);
            string AmountsS = Encryption.File_decryption_string(streamAmounts);
            streamAmounts.Close();
            bool[] flag = new bool[2];
            string temp1 = "";
            string temp = "";
            int poz = 0;
            for (int i = 0; i < AmountsS.Length; i++)
            {
                if (AmountsS[i] == Convert.ToChar("$"))
                {
                    if (temp == mass[0] && poz == 0) { flag[0] = true; }
                    if (temp == mass[1] && poz == 1) { flag[1] = true; }
                    temp = "";
                    poz++;
                    i++;
                }
                if (AmountsS[i] == Convert.ToChar("~"))
                {
                    if (flag[0] == true && flag[1] == true)
                    {
                        int rez1 = i - (temp1.Length + 3);
                        if (rez1 <= 0) { rez1 = 0; }
                        int rez2 = temp1.Length + 3;
                        AmountsS = AmountsS.Remove(rez1, rez2);
                        break;
                    }
                    else
                    {
                        flag[0] = false; flag[1] = false; temp1 = "";
                    }
                    poz = 0;
                    temp = "";
                    i++;
                }
                temp += AmountsS[i];
                temp1 += AmountsS[i];
            }
            streamAmounts = new FileStream(PathAmounts, FileMode.Create, FileAccess.Write);
            var dstEncoding = Encoding.UTF8;
            byte[] Amounts = dstEncoding.GetBytes(AmountsS);
            Encryption.File_encryption_string(streamAmounts, Amounts);
            streamAmounts.Close();
        }
    }
}
