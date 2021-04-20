using ShopBook.Data.Interface;
using ShopBook.Entities.Products;
using System.Collections.Generic;
using ShopBook.Data.FileProductGrup;
using System.IO;

namespace ShopBook.Data.FileOperation.FileProductGrup
{
    class FileProduct: Function_elements_FProduct, IFile<Product>
    {
        public FileProduct()
        {

        }
        public FileProduct(string type)
        {
            if (type == "Book") { Pathnow = PathBook; PathMapnow = MapBook; }
            if (type == "AddProducts") { Pathnow = PathAddProducts; PathMapnow = MapAddProducts; }
            if (File.Exists(Pathnow) == false || File.Exists(PathMapnow) == false)
            {
                FileStream file = File.Create(Pathnow);
                file.Close();
                file = File.Create(PathMapnow);
                file.Close();
                firstcreation = true;
            }
            else
            {
                firstcreation = false;
            }
        }
        public Product[] Search(string[] mass)
        {
            List<int> massrez = Map_analysis(mass);
            return Loading(massrez);
        }
        public void NewObject(Product objectt)
        {
            Adding_database_product(objectt);
            Changing_total_number_records("Number of records", "+1");
        }
        public void Deleting_Object(Product objectt)
        {
            if (Delete_database(Map_analysis(objectt.Maptemp)))
            {
                Delete_Map(objectt.Maptemp);
                Changing_total_number_records("Number of users", "-1");
            }
        }
        public void Deleting_Object(string[] mass)
        {
            if (Delete_database(Map_analysis(mass)))
            {
                Delete_Map(mass);
                Changing_total_number_records("Number of users", "-1");
            }
        }
        public int Total_number_records()
        {
            return Changing_total_number_records("Number of records", "get");
        }
        public Product[] Load_all()
        {
            List<int> massrez = All_Map();
            return Loading(massrez);
        }
        private Product[] Loading (List<int> massrez)
        {
            Product[] rez = new Product[massrez.Count / 2];
            int flag = 0;
            for (int i = 0; i < massrez.Count; i = i + 2)
            {
                FileStream stream = new FileStream(Pathnow, FileMode.Open, FileAccess.Read);
                byte[] dataB = Encryption.File_decryption_object(stream, massrez[i], massrez[i + 1]);
                rez[flag] = (Product)FileOperation.Byte.ByteArrayToObject(dataB);
                flag++;
                stream.Close();
            }
            //streamMap.Close();
            //stream.Close();
            if (massrez.Count == 0)
            {
                return null;
            }
            return rez;
        }
        public bool Duplicate_search(string[] mass)
        {
            if (firstcreation == false)
            {
                if (Search(mass) != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
