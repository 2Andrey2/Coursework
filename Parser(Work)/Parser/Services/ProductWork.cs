using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace Parser
{
    class ProductWork
    {
        string Path = "PruductType.info";
        public void ProductRecord(ProductType product)
        {
            string json = JsonConvert.SerializeObject(product);
            using (StreamWriter fileW = new StreamWriter(Path, true, System.Text.Encoding.Default))
            {
                fileW.WriteLine(json);
            }
            MessageBox.Show("Запись выполнена");
        }
        public void rewriting(ProductType[] product)
        {
            using (StreamWriter fileW = new StreamWriter(Path, false, System.Text.Encoding.Default))
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
                ProductType[] mass = new ProductType[System.IO.File.ReadAllLines(Path).Length];
                using (StreamReader fileW = new StreamReader(Path))
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

    }
}
