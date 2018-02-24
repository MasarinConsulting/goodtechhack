using Masarin.GTH.Search;
using Masarin.GTH.WebApi.Models;
using Microsoft.Azure.Search;
using Microsoft.Spatial;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Http;

namespace Masarin.GTH.WebApi.Controllers
{
    [RoutePrefix("api/events")]
    public class EventsController : ApiController
    {
        
        EventSearchIndex _eventsIndexHandler;

        public EventsController()
        {
            var searchServiceName = ConfigurationManager.AppSettings.Get("SearchServiceName");
            var apiKey = ConfigurationManager.AppSettings.Get("SearchServiceApiKey");
            var searchClient = new SearchServiceClient(searchServiceName, new SearchCredentials(apiKey));
            _eventsIndexHandler = new EventSearchIndex(searchClient);
        }

        

        // GET: api/Events
        public async Task<EventSearchResultDocument> Get(string longitude = null, string latitude = null, string distance = "", string category = "", string now = "")
        {
            EventSearchResultDocument events = null;

            string filter = "";

            if (string.IsNullOrEmpty(distance))
            {
                distance = "2"; // default 2 kilometer
            }

            if (string.IsNullOrEmpty(category))
            {
                category = ""; // default alla
            }
            else
            {
                filter = $"Category eq '{category}'";



                if (string.IsNullOrEmpty(now))
                {
                    now = DateTime.UtcNow.AddHours(1).ToString();
                }

                if (category == "air" && now != "")
                {

                    var start = DateTime.Parse(now).ToUniversalTime().ToString("o");
                    start = start.Replace("02-23", "02-22"); // TODO: fuling, vi har vara air-data den 22:a
                    start = start.Replace("02-24", "02-22"); // TODO: fuling, vi har vara air-data den 22:a

                    var slut = DateTime.Parse(now).ToUniversalTime().AddMinutes(15).ToString("o");
                    slut = slut.Replace("02-23", "02-22"); // TODO: fuling, vi har vara air-data den 22:a
                    slut = slut.Replace("02-24", "02-22"); // TODO: fuling, vi har vara air-data den 22:a
                    

                    //var nowDate = DateTimeOffset.Parse(now);
                    //var hour = nowDate.AddHours(1).Hour;
                    //var minute = nowDate.Minute;
                    //var minuteNextKvart = nowDate.AddMinutes(15);
                    //var datePlus = $"2018-02-22T{hour}:{minuteNextKvart}:00Z-00:00";
                    //var dateMinus = $"2018-02-22T{hour}:{minute}:00Z-00:00";
                    //filter += $" and EventDateOffset ge {dateMinus} and EventDateOffset le {datePlus} ";
                    filter += $" and (EventDateOffset ge {start} and EventDateOffset lt {slut}) ";
                }
            }

            if (string.IsNullOrEmpty(longitude) || string.IsNullOrEmpty(latitude))
            {
                events = await _eventsIndexHandler.SearchEvents("", "EventDate", "asc", 1000, 0, filter);
            }
            else
            {
                if(!string.IsNullOrEmpty(filter))
                {
                    filter += " and ";
                }

                filter += $"geo.distance(Location, geography'POINT({longitude} {latitude})') le {distance}";
                events = await _eventsIndexHandler.SearchEvents("", "EventDate", "asc", 1000, 0, filter);
            }
            

            return events;
        }

        // GET: api/Events/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Events
        public async Task Post([FromBody]ReportEventVM value)
        {
            if(value != null && value.Category != null)
            {

                //NumberFormatInfo format = new NumberFormatInfo();

                // Set the decimal seperator
                //format.NumberDecimalSeparator = ".";

                //var lat = double.Parse(value.Location.Latitude, format);
                //var longitude = double.Parse(value.Location.Longitude, format);

                GeographyPoint location = null;
                if (value.Location != null)
                {
                    location = GeographyPoint.Create(value.Location.Latitude, value.Location.Longitude);
                }

                //GeographyPoint.Create(double.Parse(value.Location.Latitude), double.Parse(value.Location.Longitude));

                var searchDoc = new EventSearchDocument()
                {
                    Category = value.Category,
                    DataSource = value.UserId,
                    DataType = "personal",
                    Description = value.Description,
                    EventDate = DateTime.UtcNow.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss"),
                    EventDateOffset = DateTime.UtcNow.AddHours(1),
                    EventId = Guid.NewGuid().ToString(),
                    Level = value.Level,
                    Location = location,
                    Title = value.Title
                };

                await _eventsIndexHandler.IndexDocuments(new List<EventSearchDocument>() { searchDoc });

            }

        }

        // PUT: api/Events/5
        public void Put(string id, [FromBody]ReportEventVM value)
        {
        }

        // DELETE: api/Events/5
        public void Delete(string id)
        {
        }
    }
}
