using Masarin.GTH.Search;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Spatial;

namespace DataImporters.Luftkvalitet
{
    public class LuftkvalitetImporter
    {

        static public async Task<List<EventSearchDocument>> GetData()
        {

            var result = await GetPage();

            var eventSearchDocuments = new List<EventSearchDocument>();

            var no2events = result.data?.no2?.Select(s => 
                new EventSearchDocument()
                {
                    Category = "air",
                    Description = "Att minska utsläppen av kväveoxider är viktigt ur flera perspektiv, både för hälsa och för miljön. Kväveoxider är giftiga och irriterar luftvägarna och slemhinnor.",
                    Title = "Kvävedioxid " + s.value,
                    EventDate = s.date.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventDateOffset = s.date,
                    EventId = Guid.NewGuid().ToString(),
                    Level = GetLevel(s.value, "no2"),
                    Location = GetLocation(s.station),
                    DataType = "api",
                    DataSource = "https://mitt.sundsvall.se"
                }
                ).ToList();

            eventSearchDocuments.AddRange(no2events);

            var pm10events = result.data?.pm10?.Select(s =>
                new EventSearchDocument()
                {
                    Category = "air",
                    Description = "Partiklar som är mindre än tio mikrometer i diameter (PM10) kan när de andas in nå ner i lungorna och orsaka lungsjukdomar.",
                    Title = "Partiklar " + s.value,
                    EventDate = s.date.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventDateOffset = s.date,
                    EventId = Guid.NewGuid().ToString(),
                    Level = GetLevel(s.value, "pm10"),
                    Location = GetLocation(s.station),
                    DataType = "api",
                    DataSource = "https://mitt.sundsvall.se"
                }
                ).ToList();

            eventSearchDocuments.AddRange(pm10events);

            return eventSearchDocuments.Where(e=>e.Location != null).ToList();
        }

        private static string GetLevel(float value, string typ)
        {
            var level = "3";

            if(typ == "no2")
            {
                if(value < 10)
                {
                    level = "1";
                }
                else if (value < 30)
                {
                    level = "2";
                }
                else if (value > 50)
                {
                    level = "3";
                }
                else if (value < 70)
                {
                    level = "4";
                }
                else
                {
                    level = "5";
                }


            }
            else if(typ == "pm10")
            {
                if (value < 4)
                {
                    level = "1";
                }
                else if (value < 6)
                {
                    level = "2";
                }
                else if (value > 8)
                {
                    level = "3";
                }
                else if (value < 10)
                {
                    level = "4";
                }
                else
                {
                    level = "5";
                }
            }


            return level;
        }

        private static GeographyPoint GetLocation(string station)
        {
            GeographyPoint geoPoint = null;
            if(station.ToLower() == "oleico4") // Bergsgatan 24
            {
                geoPoint = GeographyPoint.Create(62.3864804, 17.3012454);
            }
            else if (station.ToLower() == "sundsvall_gen2") // Köpmangatan 9
            {
                geoPoint = GeographyPoint.Create(62.388685, 17.308687);
            }
            else if (station.ToLower() == "sundsvall ") // Strandvägen 8, gamla e4 landsvägsallén
            {
                geoPoint = GeographyPoint.Create(62.390212, 17.314956);
            }

            return geoPoint;
        }

        public static async Task<LuftkvalitetDataResult> GetPage()
        {
            LuftkvalitetDataResult root = null;
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://mitt.sundsvall.se");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/air/v1/air.json?dateFrom=2018-02-22T00:00:00&dateTo=2018-02-23T00:00:00&order=DESC");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    root = JsonConvert.DeserializeObject<LuftkvalitetDataResult>(data);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return root;
        }

        
    }
}
