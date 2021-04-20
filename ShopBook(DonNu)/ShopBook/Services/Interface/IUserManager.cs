using ShopBook.Entities.Users;

namespace ShopBook.Services.Interface
{
    interface IUserManager
    {
        void UserAction(string Action, string[] mass);
        void UserAction(string Action, string[] mass, double selser, People people);
        void UserAction(string Action, string text);
    }
}
