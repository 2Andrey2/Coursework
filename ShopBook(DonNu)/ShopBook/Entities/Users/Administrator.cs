using System;
using ShopBook.Entities.Users.Abstract;

namespace ShopBook.Entities.Users
{
    [Serializable]
    class Administrator: Workers
    {
        public Administrator(string[] mass) : base(mass)
        {
            Salary = 30000;
        }
        public string[] For_table()
        {
            string[] mass = new string[] { Name, Surname, MiddleName, Login, Password, Position, Address, Convert.ToString(PhoneNumber) };
            return mass;
        }
    }
}
