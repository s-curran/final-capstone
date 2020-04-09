﻿using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public interface IItineraryDAO
    {
        int createItinerary(Itinerary itinerary);
        bool addLandmark(Landmark landmark);
        bool addToItinerary(int itineraryId, int orderNum, Landmark landmark);
        IList<Landmark> getItinerary(int itinderaryId);
        IList<Landmark> getLandmarks();
        IList<Itinerary> getUserItinerary(int id);
    }
}
