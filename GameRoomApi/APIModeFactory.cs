using GamesAPI.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAPI.DAO;

namespace GameRoomApi
{
    public class APIModeFactory
    {
        public enum APIMode { Mock, RealJob }

        APIMode _mode;

        public APIModeFactory(APIMode mode)
        {
            _mode = mode;
        }

        public IGameDAO GetGameDAO()
        {
            switch(_mode)
            {
                case APIMode.Mock: return new GameDAO_MOCK();
                case APIMode.RealJob: return new GameDAO();
                default: return new GameDAO();
            }
        }

        public IUserDAO GetUserDAO()
        {
            switch (_mode)
            {
                case APIMode.Mock: return new UserDAO_MOCK();
                case APIMode.RealJob: return new UserDAO();
                default: return new UserDAO();
            }
        }
    }
}
