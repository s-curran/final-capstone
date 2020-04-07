using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {


        /// <summary>
        /// Gets a collection of values. The requestor must be authenticated.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var result = $"Welcome back {User.Identity.Name}";
            return Ok(result);
        }
        /// <summary>
        /// Gets a collection of values. The requestor must be authenticated.
        /// </summary>
        /// <returns></returns>
        [HttpGet("open")]
        public IActionResult GetOpen()
        {
            var result = $"Welcome, un-authenticated being!";
            return Ok(result);
        }

        /// <summary>
        /// Gets a special message. The requestor must be a user.
        /// </summary>
        /// <returns></returns>
        [HttpGet("special")]
        [Authorize(Roles = "User")]
        public IActionResult RequestToken()
        {
            var result = "If you see this then you are a user.";
            return Ok(result);
        }

        [HttpGet("places")]
        public IActionResult Places(string location, string radius, string type, bool opennow)
        {
            //location=${this.lat},${this.long}&radius=${this.radius}&type=${this.type}&key=${this.key}
            WebClient client = new WebClient();
            string url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={location}&radius={radius}&type={type}&key=AIzaSyANWIg-qW05HeNmXG2Yh1Fd7w8I9w4WXto&opennow={opennow}";
            string s = client.DownloadString(url);
            return new ContentResult() { Content = s };
        }
    }
}