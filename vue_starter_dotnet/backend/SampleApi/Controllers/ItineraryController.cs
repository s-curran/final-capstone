using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApi.DAL;
using SampleApi.Models;

namespace SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItineraryController : ControllerBase
    {
        private IItineraryDAO itineraryDAO;
        private IUserDAO userDAO;

        /// <summary>
        /// Create a new Itinerary controllwer
        /// </summary>
        /// <param name="itineraryDAO">Data access object to store itineraries</param>
        /// <param name="userDAO">Object to store user data</param>
        public ItineraryController(IItineraryDAO itineraryDAO, IUserDAO userDAO)
        {
            this.itineraryDAO = itineraryDAO;
            this.userDAO = userDAO;
        }

        /// <summary>
        /// Creates an itinerary for the user and provides an itinerary id
        /// </summary>
        /// <param name="date"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [Authorize]
        public IActionResult Create(Itinerary itinerary)
        {
            // Get current user
            string username = User.Identity.Name;

            // Get user from name 
            User user = userDAO.GetUser(username);


            // Create new itinerary from params
            itinerary.UserId = user.Id;

            // Call dao to add itinerary
            int itineraryId = itineraryDAO.createItinerary(itinerary);

            return Ok(itineraryId);
        }

        /// <summary>
        /// Retrieves an itinerary for the user
        /// </summary>
        /// <param name="itineraryId"></param>
        /// <returns></returns>
        [HttpGet("display")]
        [Authorize]
        public IActionResult Display(int itineraryId)
        {
            Itinerary itinerary = itineraryDAO.getItineraryById(itineraryId);
            itinerary.Landmarks = itineraryDAO.getItinerary(itineraryId);

            return Ok(itinerary);
        }

        /// <summary>
        /// Add's a landmark to the users itinerary
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderNum"></param>
        /// <param name="landmark"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [Authorize]
        public IActionResult Add(LandmarkItinVM vm)
        {
            Landmark landmark = itineraryDAO.getLandmarkById(vm.LandmarkId);
            IList<Landmark> landmarks = itineraryDAO.getLandmarks();
            int orderNum = 1;

            if(!landmarks.Contains(landmark))
            {
                itineraryDAO.addLandmark(landmark);
            }

            bool added = itineraryDAO.addToItinerary(vm.ItineraryId, orderNum, landmark);

            if (added)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Retrieves all of the users itineraries
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        [Authorize]
        public IActionResult List()
        {
            string username = User.Identity.Name;

            User user = userDAO.GetUser(username);

            IList<Itinerary> userItinerary = itineraryDAO.getUserItineraries(user.Id);

            if (userItinerary.Count > 0)
            {
                return Ok(userItinerary);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete's a users itinerary
        /// </summary>
        /// <param name="itineraryId"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [Authorize]
        public IActionResult Delete(int itineraryId)
        {
            bool deleted = itineraryDAO.deleteItinerary(itineraryId);

            if (deleted)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Removes a landmark from the itinerary
        /// </summary>
        /// <param name="itineraryId"></param>
        /// <param name="landmarkId"></param>
        /// <returns></returns>
        [HttpDelete("remove")]
        [Authorize]
        public IActionResult Remove(int itineraryId, string landmarkId)
        {
            bool deleted = itineraryDAO.removeLandmark(itineraryId, landmarkId);

            if (deleted)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}