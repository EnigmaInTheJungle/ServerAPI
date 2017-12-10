using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAPI.DAO;
using UsersAPI.Model;
using UsersAPI.Sockets;
using WebSocketSharp.Server;

namespace UsersAPI
{
    public class UsersAPI
    {
        Dictionary<string, User> ActiveUsers = new Dictionary<string, User>();
        public void AttachAPI(WebSocketServer server, IUserDAO userDAO, string routePath)
        {
            server.AddWebSocketService(routePath, () => new UsersSocket(userDAO, ActiveUsers));
        }
    }    
}
