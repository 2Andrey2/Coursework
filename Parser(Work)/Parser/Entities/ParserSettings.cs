using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class ParserSettings
    {
        public string Path;
        public int CountLine;
        public int CountColumns;
        public string PathRez;
        public string Title;
        public string Formatting;
        public bool TitleBlok;
        public ParserSetup Parser;
        public int Pack;
        public ParserSettings(string path, int NumberLines, int MainColumns, string pathRez, string title, string formatting, ParserSetup parser, bool titleblok, int pack)
        {
            Path = path;
            CountLine = NumberLines;
            CountColumns = MainColumns;
            PathRez = pathRez;
            Title = title;
            Formatting = formatting;
            Parser = parser;
            TitleBlok = titleblok;
            Pack = pack;
        }
    }
}
