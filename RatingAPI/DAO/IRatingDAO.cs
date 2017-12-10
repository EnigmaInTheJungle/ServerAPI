using RatingAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingAPI.DAO
{
    interface IRatingDAO
    {
        List<Rate> All { get; }

        void CreateUserRate(Rate userRating);
        void AddRateToUser(int userId, int rate);
        void ChangeUserRate(int userId, int rating);
        void DeleteUserRate(int userId);
    }
}
