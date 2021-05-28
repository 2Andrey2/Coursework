using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class ProductType
    {
        public string NameProduct;
        public int CountLine;
        public int CountColumns;
        public string PathRez;
        public string Title;
        public string Formatting;
        public bool PresenceHeaders;
        public int Pack;
        public int NumberLength;
        public void infowrite(string[] info)
        {
            NameProduct = info[0];
            CountLine = Convert.ToInt32(info[1]);
            CountColumns = Convert.ToInt32(info[2]);
            PathRez = info[3];
            Title = info[4];
            Formatting = info[5];
            PresenceHeaders = Convert.ToBoolean(info[6]);
            Pack = Convert.ToInt32(info[7]);
            NumberLength = Convert.ToInt32(info[8]);
        }
    }
}
