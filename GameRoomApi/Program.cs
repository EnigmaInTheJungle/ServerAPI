using System;
using System.Collections.Generic;
using System.Linq;
using GamesAPI;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;
namespace GameRoomApi
{
    class Program
    {
        static void Main(string[] args)
        {
            APIModeFactory apiFactory = new APIModeFactory(APIModeFactory.APIMode.RealJob);

            var socket = new WebSocketServer("ws://localhost:8888/");
            GamesAPI.GamesAPI gamesAPI = new GamesAPI.GamesAPI();
            UsersAPI.UsersAPI usersAPI = new UsersAPI.UsersAPI();

            gamesAPI.AttachAPI(socket, apiFactory.GetGameDAO(), "/Games");
            usersAPI.AttachAPI(socket, apiFactory.GetUserDAO(), "/Users");

            socket.Start();

            Console.ReadKey();
        }
    }
}
