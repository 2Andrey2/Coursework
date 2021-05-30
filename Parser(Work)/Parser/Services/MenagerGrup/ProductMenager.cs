using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace Parser
{
    class ProductMenager
    {
        int NamberPage;
        string Title;
        int Pack;
        int NumberPack;
        int AllBlok;
        int AllPage;
        string PathTypeProduct = "PruductType.info";
        WorkFile work;
        public ProductMenager(string title, int pack, int allblok)
        {
            work = new WorkFile();
            NamberPage = 1;
            Title = title;
            Pack = pack;
            NumberPack = 1;
            AllBlok = allblok;
        }
        public ProductMenager()
        {
            work = new WorkFile();
        }
        public void ProductRecord(ProductType product)
        {
            string json = JsonConvert.SerializeObject(product);
            using (StreamWriter fileW = new StreamWriter(PathTypeProduct, true, System.Text.Encoding.Default))
            {
                fileW.WriteLine(json);
            }
            MessageBox.Show("Запись выполнена");
        }
        public void rewriting(ProductType[] product)
        {
            using (StreamWriter fileW = new StreamWriter(PathTypeProduct, false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < product.Length; i++)
                {
                    fileW.WriteLine(JsonConvert.SerializeObject(product[i]));
                }
            }
            MessageBox.Show("Операция выполнена");
        }
        public ProductType[] ReadingProduct()
        {
            try
            {
                ProductType[] mass = new ProductType[System.IO.File.ReadAllLines(PathTypeProduct).Length];
                using (StreamReader fileW = new StreamReader(PathTypeProduct))
                {
                    for (int i = 0; i < mass.Length; i++)
                    {
                        mass[i] = JsonConvert.DeserializeObject<ProductType>(fileW.ReadLine());
                        mass[i].NameProduct = System.Text.RegularExpressions.Regex.Unescape(mass[i].NameProduct);
                        mass[i].Title = System.Text.RegularExpressions.Regex.Unescape(mass[i].Title);
                    }
                }
                return mass;
            }
            catch
            {
                MessageBox.Show("Файл с типами продуктов не найден");
            }
            return null;
        }
        public void OpenFilePathR(string Path)
        {
            work.OpenFilePathReader(Path);
        }
        public void OpenFilePathW(string Path)
        {
            work.OpenFilePathWriter(Path);
        }
        public void CloseFilePathR()
        {
            work.CloseFileR();
        }
        public void CloseFilePathW()
        {
            work.CloseFileW();
        }
        public string[] ReadBlok (int CountLine, bool Title)
        {
            if (Title == true)
            {
                work.ReadLine();
            }
            string[] massline = new string[CountLine];
            for (int i = 0; i < massline.Length; i++)
            {
                massline[i] = work.ReadLine();
            }
            return massline;
        }
        public string ReadLine()
        {
            return work.ReadLine(); ;
        }
        public void WriteTitle()
        {
            if (AllBlok / Pack < NumberPack)
            {
                Pack = AllBlok - AllPage;
            }
            AllPage++;
            work.WriteLineMass(new string[] { Title + "   [" + NamberPage + "/" + Pack + "]" + " " + NumberPack + " All:" + AllPage });
            NamberPage++;
            if (NamberPage > Pack)
            {
                NamberPage = 1;
                NumberPack++;
            }
        }
        public void WriteLineMass(string[] massline)
        {
            work.WriteLineMass(massline);
        }
    }
}
