using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Parser.Services
{
    class ChecksFile
    {
        //С хешами но считает их одинакаво(
        //public bool CheckingDuplicates (string Path)
        //{
        //    WorkFile workFile = new WorkFile(Path, "temp.txt");
        //    StreamReader reader = workFile.OpenFilePath();
        //    while (true)
        //    {
        //        string temp = reader.ReadLine();
        //        if (temp == null) { break; }
        //        workFile.WriteFile(new string[] { temp.GetHashCode().ToString() });
        //    }
        //    int qwe = "ZZВВ 000351                                               0,0#л 0#,0%".GetHashCode();
        //    int dfgh = "ZZГМ 000741                                               0,0#л 0#,0%".GetHashCode();
        //    workFile.CloseFile();
        //    workFile = new WorkFile(null, "temp.txt");
        //    reader = workFile.ReaderRezPathOpen();
        //    int[] masshesh = new int[System.IO.File.ReadAllLines("temp.txt").Length];
        //    for (int i = 0; i < masshesh.Length; i++)
        //    {
        //        masshesh[i] = Convert.ToInt32(reader.ReadLine());
        //    }
        //    reader.Close();
        //    File.Delete("temp.txt");
        //    var set = new HashSet<int>();
        //    foreach (var item in masshesh)
        //        if (!set.Add(item))
        //        {
        //            MessageBox.Show("Найдены повторяющиесы строки!" + item);
        //            return true;
        //        }
        //    return false;
        //}
        // Работает но со строками
        public bool CheckingDuplicates(string Path)
        {
            WorkFile workFile = new WorkFile(null, Path);
            StreamReader reader = workFile.ReaderRezPathOpen();
            string[] masshesh = new string[System.IO.File.ReadAllLines("temp.txt").Length];
            for (int i = 0; i < masshesh.Length; i++)
            {
                masshesh[i] = reader.ReadLine();
            }
            reader.Close();
            var set = new HashSet<string>();
            foreach (var item in masshesh)
                if (!set.Add(item))
                {
                    MessageBox.Show("Найдены повторяющиесы строки!" + item);
                    return true;
                }
            return false;
        }
    }
}
