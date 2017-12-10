using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersAPI.Model
{
    public class User
    {
        static int count = 0;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public User()
        {

        }

        public User(User user)
        {
            Name = user.Name;
            Password = user.Password;
            Id = ++count;
        }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
            Id = ++count;
        }
    }
}
