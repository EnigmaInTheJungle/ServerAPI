using System;
using System.Collections.Generic;
using System.Threading;
using GamesAPI.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;
using static GamesAPI.Actions.GameActions;

namespace GamesApiTests
{
    [TestClass]
    public class Tests
    {
        WebSocketServer server;
        GamesAPI.GamesAPI gamesAPI = new GamesAPI.GamesAPI();

        [TestInitialize]
        public void StartServer()
        {
            server = new WebSocketServer("ws://localhost:8888/");
            gamesAPI.AttachAPI(server, new GamesAPI.DAO.GameDAO(), "/Games");
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
        public void TestGamesUpdate()
        {
            var ws = new WebSocket("ws://localhost:8888/Games");

            string getMsg = "";
            ws.OnMessage += (sender, e) => { getMsg = e.Data; };

            ws.Connect();

            ws.Send(JsonConvert.SerializeObject(new { type = GET_GAMES }));

            Thread.Sleep(1000);

            JToken token = JObject.Parse(getMsg);
            Assert.AreEqual((string)token.SelectToken("type"), UPDATE_GAMES);
        }

        [TestMethod]
        public void TestGameCreate()
        {
            var ws = new WebSocket("ws://localhost:8888/Games");

            string getMsg = "";
            ws.OnMessage += (sender, e) => getMsg = e.Data;

            ws.Connect();

            ws.Send(JsonConvert.SerializeObject(new { type = CREATE_GAME, data = new { Title = "GameTitle"} }));

            Thread.Sleep(1000);

            JToken token = JObject.Parse(getMsg);
            Assert.AreEqual((string)token.SelectToken("type"), GAME_CREATED);
        }
    }
}
