using System;
using ShopBook.Data.FileOperation.FileProductGrup;

namespace ShopBook.Entities.Products
{
    [Serializable]
    class Book: Product
    {
        string Author;
        string Genre;
        string Material;

        public Book(string[] mass)
        {
            FileProduct product = new FileProduct();
            Namber = product.Total_number_records(); Name = mass[1]; Author = mass[2]; Genre = mass[3]; Manufacturer = mass[4]; Material = mass[5]; Storage = mass[6]; Price = Convert.ToDouble(mass[7]);
            Maptemp = new string[9];
            Maptemp[0] = mass[0]; Maptemp[1] = Convert.ToString(Namber); Maptemp[2] = Name; Maptemp[3] = Author; Maptemp[4] = Genre; Maptemp[5] = Manufacturer; Maptemp[6] = Material; Maptemp[7] = Storage; Maptemp[8] = Price.ToString();
            Type = mass[0];
        }
    }
}
