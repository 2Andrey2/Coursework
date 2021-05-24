using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Services
{
    class MainStream_dop
    {
        protected ParserSettings settings;
        public MainStream_dop(ParserSettings parserSettings)
        {
            settings = parserSettings;
        }
        protected int GetNumberBlocks()
        {
            int allblok;
            if (settings.TitleBlok == true)
            {
                allblok = System.IO.File.ReadAllLines(settings.Path).Length / (settings.CountLine * settings.CountColumns + 1);
            }
            else
            {
                allblok = System.IO.File.ReadAllLines(settings.Path).Length / (settings.CountLine * settings.CountColumns);
            }
            return allblok;
        }
        protected bool RunDataChecks()
        {
            ChecksFile checksFile = new ChecksFile();
            if (checksFile.CheckingDuplicates(settings.Path) == true)
            {
                return true;
            }
            return false;
        }
    }
}
