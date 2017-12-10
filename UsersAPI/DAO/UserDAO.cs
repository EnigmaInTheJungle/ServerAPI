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
    public class UserDAO : IUserDAO
    {
        protected string path = "";

        public List<User> All => Load();

        public UserDAO(string path = "Users.json")
        {
            this.path = path;
        }

        public void Create(User user)
        {
            List<User> users = Load();
            users.Add(user);
            Write(users);
        }

        List<User> Load()
        {
            if (File.Exists(path) == false)
            {
                FileStream fs = File.Create(path);
                fs.Close();
            }
            string jsonString = File.ReadAllText(path);
            List<User> users = new List<User>();
            if (jsonString.Length == 0)
                users = new List<User>();
            else
                users = JsonConvert.DeserializeObject<List<User>>(jsonString);
            return users;
        }

        void Write(List<User> users)
        {
            string jsonString = JsonConvert.SerializeObject(users);
            File.WriteAllText(path, jsonString);
        }

        public User Get(int id)
        {
            List<User> users = Load();
            return users.Find(u => u.Id == id);
        }

        public User Get(string name)
        {
            List<User> users = Load();
            return users.Find(u => u.Name == name);
        }
    }

}
