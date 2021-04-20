using ShopBook.Data.FilePeopleGrup;
using System;


namespace ShopBook.Entities.Users.Abstract
{
    [Serializable]
    abstract class Workers : People
    {
        int Code;
        protected double Salary;
        protected Workers(string[] mass) : base(mass)
        {
            FilePeople people = new FilePeople("not");
            Code = people.Total_number_records();
        }
    }
}
