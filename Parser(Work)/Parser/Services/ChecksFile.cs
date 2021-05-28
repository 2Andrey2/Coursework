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
        string Path;
        Regex regex;
        int NumberLength;
        public ChecksFile(string path, int numberLength)
        {
            Path = path;
            NumberLength = numberLength;
            regex = new Regex(@"^\w{4,}(\s+)\d{" + NumberLength + @"}(\b+)");
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
                if (temp != "")
                {
                    if (!set.Add(temp))
                    {
                        MessageBox.Show("Найдены повторяющиесы строки! " + temp);
                        return true;
                    }
                }
                flag++;
            }
            return false;
        }
        public bool SerialNumberCheck(string line)
        {
            if (line != "")
            {
                MatchCollection matches = regex.Matches(line);
                if (matches.Count > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
