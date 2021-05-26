using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        string Path;
        Regex regex;
        int NumberLength;
        public ChecksFile(string path, int numberLength)
        {
            Path = path;
            NumberLength = numberLength;
            regex = new Regex(@"^\w{4,}(\s+)\d{" + NumberLength + @"}(\s+)");
        }
        public bool RunningChecks(bool title, int blok)
        
        {
            WorkFile workFile = new WorkFile(null, Path);
            StreamReader reader = workFile.ReaderRezPathOpen();
            var set = new HashSet<string>();
            int flag = blok;
            int longfile = System.IO.File.ReadAllLines(Path).Length;
            //if (title == true) { longfile = System.IO.File.ReadAllLines(Path).Length - blok; }
            //else { longfile = System.IO.File.ReadAllLines(Path).Length; }
            for (int i = 0; i < longfile; i++)
            {
                if (title = true && flag == blok)
                {
                    reader.ReadLine();
                    flag = 0;
                }
                string temp = reader.ReadLine();
                if (temp == null)
                {
                    return false;
                }
                if (SerialNumberCheck(temp) != true)
                {
                    MessageBox.Show("Найден кривой серийник! " + temp);
                    return true;
                }
                if (!set.Add(temp))
                {
                    MessageBox.Show("Найдены повторяющиесы строки! " + temp);
                    return true;
                }
                flag++;
            }
            return false;
        }
        public bool SerialNumberCheck(string line)
        {
            MatchCollection matches = regex.Matches(line);
            if (matches.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
