using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAPI.Actions;
using UsersAPI.DAO;
using UsersAPI.Model;
using WebSocketSharp;
using WebSocketSharp.Net.WebSockets;
using WebSocketSharp.Server;

namespace UsersAPI.Sockets
{
    class UsersSocket : WebSocketBehavior
    {
        IUserDAO userDAO;
        Dictionary<string, User> ActiveUsers;

        public UsersSocket(IUserDAO userDAO, Dictionary<string, User> users)
        {
            this.userDAO = userDAO;
            this.ActiveUsers = users;
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            JToken token = JObject.Parse(e.Data);
            string actionType = (string)token.SelectToken("type");

            if (actionType == UserActions.GET_ONLINE_USERS)
                SendOnlineUsers();
            else if (actionType == UserActions.REGISTER_USER)
                RegisterNewUser(token);
            else if (actionType == UserActions.LOGIN_USER)
                LoginUser(token);

        }

        private void LoginUser(JToken token)
        {
            User receivedUserInfo = token.SelectToken("data").ToObject<User>();

            User loggedUser = userDAO.Get(receivedUserInfo.Name);
            if (loggedUser != null)
            {
                if (receivedUserInfo.Password == loggedUser.Password)
                {
                    if(ActiveUsers.Values.Select(z => z.Name).ToList().Contains(receivedUserInfo.Name))
                    {
                        Send(JsonConvert.SerializeObject(new { type = UserActions.USER_ALREADY_LOGIN }));
                    }
                    else
                    {
                        AddActiveUser(loggedUser);
                        Send(JsonConvert.SerializeObject(new { type = UserActions.LOGIN_SUCCESSFULL, data = loggedUser }));
                        BroadcastOnlineUsers();
                    }
                }
                else
                {
                    Send(JsonConvert.SerializeObject(new { type = UserActions.INCORRECT_PASSWORD }));
                }
            }
            else
            {
                Send(JsonConvert.SerializeObject(new { type = UserActions.USER_NOT_FOUND }));
            }
        }

        private void RegisterNewUser(JToken token)
        {
            User receivedUserInfo = token.SelectToken("data").ToObject<User>();

            User reggisteredUser = userDAO.Get(receivedUserInfo.Name);
            if (reggisteredUser != null)
            {
                Send(JsonConvert.SerializeObject(new { type = UserActions.USER_ALREADY_EXISTS }));
            }
            else
            {
                User newUser = new User(receivedUserInfo.Name, receivedUserInfo.Password);
                userDAO.Create(newUser);
                Send(JsonConvert.SerializeObject(new { type = UserActions.REGISTRATION_SUCCESSFULL }));
            }
        }

        protected override void OnClose(CloseEventArgs e)
        {
            ActiveUsers.Remove(this.ID);
            BroadcastOnlineUsers();
        }


        void AddActiveUser(User user)
        {
            User cryptedUser = new User();
            cryptedUser.Id = user.Id;
            cryptedUser.Name = user.Name;
            cryptedUser.Password = "crypted";
            ActiveUsers.Add(this.ID, new User(cryptedUser));
        }
        private void SendOnlineUsers()
        {
            Send(JsonConvert.SerializeObject(new { type = UserActions.UPDATE_USERS, data = ActiveUsers.Values }));
        }
        private void BroadcastOnlineUsers()
        {
            Sessions.Broadcast(JsonConvert.SerializeObject(new { type = UserActions.UPDATE_USERS, data = ActiveUsers.Values }));
        }
    }
}
