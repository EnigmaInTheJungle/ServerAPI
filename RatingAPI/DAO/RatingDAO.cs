using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatingAPI.Model;

namespace RatingAPI.DAO
{
    class RatingDAO : IRatingDAO
    {
        List<Rate> rating = new List<Rate>();

        public List<Rate> All => rating;

        public void AddRateToUser(int userId, int rate)
        {
            rating.Find(x => x.UserId == userId).Rating += rate;
        }

        public void ChangeUserRate(int userId, int rate)
        {
            rating.Find(x => x.UserId == userId).Rating = rate;
        }

        public void CreateUserRate(Rate userRating)
        {
            rating.Add(userRating);
        }

        public void DeleteUserRate(int userId)
        {
            rating.RemoveAll(g => g.UserId == userId);
        }
    }
}
