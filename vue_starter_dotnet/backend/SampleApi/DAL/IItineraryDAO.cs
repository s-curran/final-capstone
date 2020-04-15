using SampleApi.Models;
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
        IList<Itinerary> getUserItineraries(int id);
        Itinerary getItineraryById(int id);
        bool deleteItinerary(int itineraryId);
        bool removeLandmark(int itineraryId, string landmarkId);
        Landmark getLandmarkById(string landmarkId);
        bool addRating(string landmarkId, double averageRating, int numberOfRatings);
        Rating getRating(string landmarkId);
        bool updateStartingPoint(int itineraryId, string newStartingPoint);

    }
}
