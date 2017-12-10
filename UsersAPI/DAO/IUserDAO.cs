
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAPI.Model;

namespace UsersAPI.DAO
{
    public interface IUserDAO
    {
        List<User> All { get; }
        void Create(User user);
        User Get(int id);
        User Get(string name);
    }
}
