using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    public class LandmarkItinVM
    {
        public int ItineraryId { get; set; }
        public string LandmarkId { get; set; }
        public string LandmarkName { get; set; }
        public string LandmarkAddress { get; set; }


    }
}
