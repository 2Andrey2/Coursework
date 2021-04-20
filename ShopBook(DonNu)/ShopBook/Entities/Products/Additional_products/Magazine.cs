using System;
using ShopBook.Data.FileOperation.FileProductGrup;
using ShopBook.Entities.Products.Abstract;

namespace ShopBook.Entities.Products.Additional_products
{
    [Serializable]
    class Magazine: Complementary_products
    {
        private string Author;
        private string Genre;
        private string Topic;
        private string Audience;

        public Magazine(string[] mass)
        {
            FileProduct product = new FileProduct();
            Namber = product.Total_number_records(); Name = mass[1]; Author = mass[2]; Topic = mass[3]; Storage = mass[4]; Genre = mass[5]; Manufacturer = mass[6]; Audience = mass[7]; Price = Convert.ToDouble(mass[8]);
            Maptemp = new string[10];
            Maptemp[0] = mass[0]; Maptemp[1] = Convert.ToString(Namber); Maptemp[2] = Name; Maptemp[3] = Author; Maptemp[4] = Topic; Maptemp[5] = Storage; Maptemp[6] = Genre; Maptemp[7] = Manufacturer; Maptemp[8] = Audience; Maptemp[9] = Price.ToString();
            Type = mass[0];
        }
    }
}
