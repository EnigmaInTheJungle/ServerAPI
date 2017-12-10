using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RatingAPI.Actions;
using RatingAPI.DAO;
using RatingAPI.Model;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace RatingAPI.Sockets
{
    class RatingSocket : WebSocketBehavior
    {
        IRatingDAO ratingDAO;

        public RatingSocket(IRatingDAO ratingDAO)
        {
            this.ratingDAO = ratingDAO;
        }

        protected override void OnOpen()
        {
            ratingDAO.CreateUserRate(new Rate(0, 0));
            BroadcastCurrentRating();
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            JToken token = JObject.Parse(e.Data);
            string actionType = (string)token.SelectToken("type");

            if (actionType == RatingActions.GET_CURRENT_RATING)
                SendCurrentRating();

        }
        protected override void OnClose(CloseEventArgs e)
        {
            BroadcastCurrentRating();
        }

        private void SendCurrentRating()
        {
            Send(JsonConvert.SerializeObject(new { type = RatingActions.UPDATE_RATING, data = ratingDAO.All }));
        }
        private void BroadcastCurrentRating()
        {
            Sessions.Broadcast(JsonConvert.SerializeObject(new { type = RatingActions.UPDATE_RATING, data = ratingDAO.All }));
        }
    }
}
