using System;
using ShopBook.Data.FilePeopleGrup;

namespace ShopBook.Entities.Users
{
    [Serializable]
    class Client : People
    {
        int Number;
        string[] ShoppingList;

        public Client(string[] mass) : base(mass)
        {
            FilePeople people = new FilePeople("not");
            Number = people.Total_number_records();
        }
        public string[] For_table()
        {
            string[] mass = new string[] { Name, Surname, MiddleName, Address, Convert.ToString(PhoneNumber), Login, Password, Position};
            //nameT.Text, SurnameT.Text, middleNameT.Text, addressT.Text, telephoneT.Text, LoginT.Text, PasswordT.Text, position.Text
            return mass;
        }
        public void set_ShoppingList(string text)
        {
            if (ShoppingList == null)
            {
                ShoppingList = new string[1];
                ShoppingList[0] = text;
            }
            else
            {
                Array.Resize(ref ShoppingList, ShoppingList.Length + 1);
                ShoppingList[ShoppingList.Length - 1] = text;
            }
        }
        public string[] get_ShoppingList()
        {
            return ShoppingList;
        }
    }
}
