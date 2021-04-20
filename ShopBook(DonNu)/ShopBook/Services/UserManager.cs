using ShopBook.Data.FileLogGrup;
using ShopBook.Data.FilePeopleGrup;
using ShopBook.Data.Interface;
using ShopBook.Entities;
using ShopBook.Entities.Users;
using ShopBook.Services.Interface;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ShopBook.Services
{
    class UserManager : IUserManager
    {
        internal event Action<IEnumerable<People>> LoadAllItem;
        FileLog log = new FileLog();
        public void UserAction(string Action, string[] mass)
        {
            if (Action == "Created")
            {
                UserСreation(mass);
            }
            if (Action == "Entrance")
            {
                UserEditing(mass);
            }
            if (Action == "Delete")
            {
                UserDelete(mass);
            }
            if (Action == "Load_all")
            {
                Load_all();
            }
            log.Action("Recording", Action, mass);
        }
        public void UserAction(string Action, string[] mass, double selser, People people)
        {
            if (Action == "Change")
            {
                UserСreation(mass, selser, people);
            }
        }
        public void UserAction(string Action, string text)
        {
            if (Action == "Change")
            {
                UserСreation(text);
            }
        }
        void UserDelete(string[] mass)
        {
            IFile<People> file = new FilePeople("Сheck");
            file.Deleting_Object(mass);
            MessageBox.Show("Пользователь удален");
        }
        void UserСreation(string[] mass)
        {
            People objectt;
            switch (mass[7])
            {
                case "Клиент": objectt = new Client(mass); break;
                case "Продавец": objectt = new Seller(mass); break;
                case "Модератор": objectt = new Moderator(mass); break;
                case "Администратор": objectt = new Administrator(mass); break;
                default: throw new ArgumentException("Недопустимый код операции");
            }
            IFile<People> file = new FilePeople("Сheck");
            if (file.Duplicate_search(objectt.get_Login_Password()) == true)
            {
                MessageBox.Show("Пользователь с такими данными уже зарегестрирован");
                return;
            }
            file.NewObject(objectt);
            MessageBox.Show("Пользователь зарегистрирован");
        }
        void UserEditing(string[] mass)
        {
            IFile<People> file = new FilePeople("Сheck");
            People[] people = file.Search(mass);
            if (people == null)
            {
                User.Peoples = null;
                MessageBox.Show("Неверные данные");
            }
            else
            {
                User.Authorization = true;
                User.Peoples = people[0];
            }
        }
        void Load_all()
        {
            FilePeople file = new FilePeople();
            if (file.Total_number_records() != 0)
            {
                IEnumerable<People> rez = file.Load_all();
                LoadAllItem?.Invoke(rez);
            }
        }
        void UserСreation(string[] mass, double selser, People peopleT)
        {
            IFile<People> file = new FilePeople("Сheck");
            People[] people = null;
            if (peopleT is Seller) { people = file.Search(peopleT.get_Login_Password()); }
            else { MessageBox.Show("Ошибка"); }
            file.Deleting_Object(people[0].get_Login_Password());
            Seller seller = (Seller)people[0];
            seller.set_info_sell(selser, mass);
            file.NewObject(people[0]);
        }
        void UserСreation(string text)
        {
            if (User.Peoples != null)
            {
                Client client = (Client)User.Peoples;
                client.set_ShoppingList(text);
            }
            UserDelete(User.Peoples.get_Login_Password());
            IFile<People> file = new FilePeople("Сheck");
            file.NewObject(User.Peoples);
        }

    }
}
