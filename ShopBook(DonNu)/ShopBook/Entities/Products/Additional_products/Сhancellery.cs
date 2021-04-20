using System;
using ShopBook.Data.FileOperation.FileProductGrup;
using ShopBook.Entities.Products.Abstract;

namespace ShopBook.Entities.Products.Additional_products
{
    [Serializable]
    class Сhancellery: Complementary_products
    {
        string Appointment;
        public Сhancellery(string[] mass)
        {
            FileProduct product = new FileProduct();
            Namber = product.Total_number_records(); Name = mass[1]; Storage = mass[2]; Manufacturer = mass[3]; Appointment = mass[4]; Price = Convert.ToDouble(mass[5]);
            Maptemp = new string[7];
            Maptemp[0] = mass[0]; Maptemp[1] = Convert.ToString(Namber); Maptemp[2] = Name; Maptemp[3] = Storage; Maptemp[4] = Manufacturer; Maptemp[5] = Appointment; Maptemp[6] = Price.ToString();
            Type = mass[0];
        }
    }
}
