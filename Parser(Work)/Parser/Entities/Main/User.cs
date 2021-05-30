using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Entities
{
    [Serializable]
    class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Telephone { get; set; }
        public string Login { get; set; }
        int LoginHeh;
        public string Password { get; set; }
        int PasswordHeh;
        public string Position { get; set; }
        public User(string[] massdata)
        {
            Name = massdata[0];
            Surname = massdata[1];
            MiddleName = massdata[2];
            Telephone = massdata[3];
            Login = massdata[4];
            Password = massdata[5];
            Position = massdata[6];
            LoginHeh = Convert.ToInt32(massdata[7]);
            PasswordHeh = Convert.ToInt32(massdata[8]);
        }
        public bool AuthorizationCheck(int[] info)
        {
            if(LoginHeh == info[0] && PasswordHeh == info[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
