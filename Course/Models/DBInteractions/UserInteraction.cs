using Course.Models.DBInteractions;
using CourseWork.Models.DataBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course.Models.Interactions
{
    public class UserInteraction: Interaction
    {
        public UserInteraction() : base() { }
        public List<User> GetUserList()
        {
            return dBContext.Users.ToList();
        }
        public void AddNewUser(string login, string password)
        {
            dBContext.Users.Add(new User() { Login = login, Password = password });
            SaveChanges();
        }
        public void DeleteUser(string login)
        {
            dBContext.Users.Remove(FindUserByLogin(login));
            SaveChanges();
        }
        public void EditUserPassord(string login, string password)
        {
            FindUserByLogin(login).Password = password;
            SaveChanges();
        }
        public User FindUserByLogin(string login)
        {
            return dBContext.Users.Find(login);
        }
    }
}