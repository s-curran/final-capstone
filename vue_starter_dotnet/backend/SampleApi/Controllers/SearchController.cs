using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpGet("places")]
        public IActionResult Places(string location, string radius, string type, bool opennow)
        {
            //location=${this.lat},${this.long}&radius=${this.radius}&type=${this.type}&key=${this.key}
            WebClient client = new WebClient();
            string url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={location}&radius={radius}&type={type}&key=AIzaSyANWIg-qW05HeNmXG2Yh1Fd7w8I9w4WXto";
            if (opennow == true)
            {
                url += $"&opennow=true";
            }
            string s = client.DownloadString(url);
            return new ContentResult() { Content = s };
        }

        [HttpGet("place")]
        public IActionResult Places(string placeId)
        {
            WebClient client = new WebClient();
            string url = $"https://maps.googleapis.com/maps/api/place/details/json?placeid={placeId}&key=AIzaSyANWIg-qW05HeNmXG2Yh1Fd7w8I9w4WXto";

            string s = client.DownloadString(url);
            return new ContentResult() { Content = s };
        }
    }
}