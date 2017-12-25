using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAPI.Model;

namespace UsersAPI.DAO
{
    public class UserDAO_MOCK : IUserDAO
    {
        protected string path = "";

        public List<User> All => users;

        List<User> users = new List<User>
        {
            new User("Leon", "123"), new User("Matilda", "123")
        };

        public void Create(User user)
        {
            users.Add(user);
        }

        public User Get(int id)
        { 
            return users.Find(u => u.Id == id);
        }

        public User Get(string name)
        {
            return users.Find(u => u.Name == name);
        }
    }

}
