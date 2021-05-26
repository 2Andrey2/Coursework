using Parser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Data;
using System.IO;
using System.Windows;

namespace Parser.Services
{
    class UserManager
    {
        WorkUser workuser;
        public UserManager(string Path)
        {
            workuser = new WorkUser(Path);
        }
        public bool Authorization(int[] infouser)
        {
            User[] alluser = SetUserAll();
            if (alluser == null) { return false; }
            for (int i = 0; i < alluser.Length; i++)
            {
                if (alluser[i].AuthorizationCheck(infouser) == true)
                {
                    ActiveUser.user = alluser[i];
                    return true;
                }
            }
            return false;
        }
        public User[] SetUserAll()
        {
            try
            {
                return workuser.ReadFile();
            }
            catch (Exception ex)
            {
                if (ex.Message != "not") { MessageBox.Show(ex.Message, ex.StackTrace); };
                return null;
            }
        }
        public void CreatedUser(User user)
        {
            User[] alluser = SetUserAll();
            for (int i = 0; i < alluser.Length; i++)
            {
                if (alluser[i].Login == user.Login)
                {
                    MessageBox.Show("Потльзатель с таким логином уже зарегистрирован");
                    return;
                }
            }
            User[] newalluser;
            if (alluser != null)
            {
                newalluser = new User[alluser.Length + 1];
                for (int i = 0; i < alluser.Length; i++)
                {
                    newalluser[i] = alluser[i];
                }
            }
            else
            {
                newalluser = new User[1];
            }
            newalluser[newalluser.Length-1] = user;
            workuser.WriteFile(newalluser);
        }
        public void DeleteUser(User datauser)
        {
            User[] alluser = SetUserAll();
            for (int i = 0; i < alluser.Length; i++)
            {
                if (GetMassUserInfo(alluser[i]).SequenceEqual(GetMassUserInfo(datauser)) == true)
                {
                    alluser = alluser.Where((val, idx) => idx != i).ToArray();
                }
            }
            workuser.WriteFile(alluser);
        }
        private void StreamClose (FileStream stream)
        {
            stream.Close();
        }
        private string[] GetMassUserInfo (User user)
        {
            return new string[] { user.Name, user.Surname, user.MiddleName, user.Telephone, user.Login, user.Password, user.Position};
        }

    }
}
