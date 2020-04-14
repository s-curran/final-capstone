using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    public class NewRatingVM
    {
        public Landmark landmarkVM { get; set; }
        public int newNumberOfRatings { get; set; }

        public double newAverageRating { get; set; }

    }
}
