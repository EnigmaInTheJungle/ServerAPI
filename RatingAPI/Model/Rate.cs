using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingAPI.Model
{
    public class Rate
    {
        static int count = 0;
        
        public int UserId { get; set; }
        public int Rating { get; set; }

        public Rate()
        {

        }

        public Rate(Rate user)
        {
            Rating = user.Rating;
            UserId = user.UserId;
        }

        public Rate(int userId, int rating)
        {
            UserId = userId;
            Rating = rating;
        }
    }
}
