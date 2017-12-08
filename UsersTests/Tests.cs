using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;
using static UsersAPI.Actions.UserActions;

namespace UsersTests
{
    [TestClass]
    public class Tests
    {
        WebSocketServer server;
        UsersAPI.UsersAPI usersAPI = new UsersAPI.UsersAPI();

        [TestInitialize]
        public void StartServer()
        {
            server = new WebSocketServer("ws://localhost:8888/");
            usersAPI.AttachAPI(server, new UsersAPI.DAO.UserDAO(), "/Users");
            server.Start();
        }

        [TestCleanup]
        public void ServerStop()
        {
            server.Stop();
        }

        [TestMethod]
        public void TestWebSocketWithServiceIsListening()
        {
            Assert.AreEqual(true, server.IsListening);
        }

        [TestMethod]
        public void TestUsersUpdate()
        {
            var ws = new WebSocket("ws://localhost:8888/Users");

            string getMsg = "";
            ws.OnMessage += (sender, e) => { getMsg = e.Data; };

            ws.Connect();

            Thread.Sleep(1000);

            JToken token = JObject.Parse(getMsg);
            Assert.AreEqual((string)token.SelectToken("type"), UPDATE_USERS);
        }
    }
}
