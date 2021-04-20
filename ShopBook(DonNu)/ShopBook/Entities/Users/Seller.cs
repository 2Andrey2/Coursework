using System;
using ShopBook.Entities.Users.Abstract;

namespace ShopBook.Entities.Users
{
    [Serializable]
    class Seller : Workers
    {
        double Sales;
        string[] selesmass;
        public Seller(string[] mass) : base(mass)
        {
            Salary = 10000;
            selesmass = new string[0];
        }
        public string[] get_info_sell()
        {
            string[] rez = new string[selesmass.Length + 1];
            rez[0] = Convert.ToString(Sales);
            for (int i = 1; i < rez.Length; i++)
            {
                rez[i] = selesmass[i - 1];
            }
            return rez;
        }
        public void set_info_sell(double seles, string mass)
        {
            Sales += seles;
            Array.Resize(ref selesmass, selesmass.Length + 1);
            selesmass[selesmass.Length-1] = mass;
        }
        public void set_info_sell(double seles, string[] mass)
        {
            Sales += seles;
            selesmass = mass;
        }
        public string[] For_table()
        {
            string[] mass = new string[] { Name, Surname, MiddleName, Address, Convert.ToString(PhoneNumber), Login, Password, Position, Convert.ToString(Sales) };
            return mass;
        }
    }
}
