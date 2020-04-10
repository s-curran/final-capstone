using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    public class Itinerary
    {
        public int ItineraryId { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfTour { get; set; }
        public string TourName { get; set; }
        public string StartPoint { get; set; }
        //public IDictionary<int, Landmark> ItineraryOrder { get; set; }
    }
}
