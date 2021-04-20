using System;
using ShopBook.Entities.Users.Abstract;

namespace ShopBook.Entities.Users
{
    [Serializable]
    class Moderator: Workers
    {
        public Moderator(string[] mass) : base(mass)
        {
            Salary = 20000;
        }
        public string[] For_table()
        {
            string[] mass = new string[] { Name, Surname, MiddleName, Address, Convert.ToString(PhoneNumber), Login, Password, Position };
            return mass;
        }
    }
}
