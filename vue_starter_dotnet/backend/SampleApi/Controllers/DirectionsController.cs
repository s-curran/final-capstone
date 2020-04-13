using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApi.DAL;
using SampleApi.Models;

namespace SampleApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DirectionsController : ControllerBase
    {
        private IItineraryDAO itineraryDAO;

        public DirectionsController(IItineraryDAO itineraryDAO)
        {
            this.itineraryDAO = itineraryDAO;
        }

        [HttpGet]
        public IActionResult Directions(int itineraryId)
        {
            Itinerary itinerary = itineraryDAO.getItineraryById(itineraryId);
            itinerary.Landmarks = itineraryDAO.getItinerary(itineraryId);

            string origin = itinerary.StartPoint.Replace(" ", "+");
            int lastIndex = itinerary.Landmarks.Count;
            string destination = itinerary.Landmarks[lastIndex - 1].LandmarkId;

            WebClient client = new WebClient();
            string url = $"https://maps.googleapis.com/maps/api/directions/json?key=AIzaSyANWIg-qW05HeNmXG2Yh1Fd7w8I9w4WXto&origin={origin}&destination=place_id:{destination}";

            if (lastIndex > 1)
            {
                string waypoints = "";
                for (int i = 0; i <= lastIndex -3; i++)
                {
                    waypoints += $"place_id:{itinerary.Landmarks[i].LandmarkId}|";
                }

                waypoints += $"place_id:{itinerary.Landmarks[lastIndex - 2].LandmarkId}";

                url += $"&waypoints={waypoints}";
            }

            string s = client.DownloadString(url);
            return new ContentResult() { Content = s };
        }
    }
}