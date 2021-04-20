using ShopBook.Entities.Users.Interface;
using System;

namespace ShopBook.Entities.Users
{
    [Serializable]
    class People: IPeople
    {
        protected string Name;
        protected string Surname;
        protected string MiddleName;
        protected string Login;
        protected string Password;
        protected string Address;
        protected int PhoneNumber;
        protected string Position;
        public People(string[] mass)
        {
            Name = mass[0];
            Surname = mass[1];
            MiddleName = mass[2];
            Address = mass[3];
            PhoneNumber = Convert.ToInt32(mass[4]);
            Login = mass[5];
            Password = mass[6];
            Position = mass[7];
        }
        public string[] get_Login_Password ()
        {
            string[] mass = new string[2]; mass[0] = Login; mass[1] = Password;
            return mass;
        }
        public string[] For_table() { return null; }
    }
}
