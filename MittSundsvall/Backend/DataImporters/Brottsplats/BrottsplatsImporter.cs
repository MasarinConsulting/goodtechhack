using Masarin.GTH.Search;
using Microsoft.Spatial;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataImporters
{
    public class BrottsplatsImporter
    {

        static public async Task<List<EventSearchDocument>> GetData()
        {
            bool finish = false;
            List<EventSearchDocument> documentList = new List<EventSearchDocument>();
            BrottsplatsDataResult root = null;
            int page = 0;
            while (!finish)
            {
                page++;
                root = await GetPage(page);
                MapPage(documentList, root);
                if (page == 1)
                {
                    finish = true;
                }
            }
            return documentList;
        }

        private static void MapPage(List<EventSearchDocument> documentList, BrottsplatsDataResult root)
        {
            foreach (var post in root.data)
            {
                documentList.Add(new EventSearchDocument()
                {
                    Category = "crime",
                    Description = post.description,
                    EventDate = post.pubdate_iso8601.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventDateOffset = post.pubdate_iso8601,
                    EventId = Guid.NewGuid().ToString(),
                    Location = GeographyPoint.Create(post.lat, post.lng),
                    Title = post.title_type,
                    Level = "0",
                    DataType = "api",
                    DataSource = "https://brottsplatskartan.se"

                });
            }
        }

        private static async Task<BrottsplatsDataResult> GetPage(int page)
        {
            BrottsplatsDataResult root = null;
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://brottsplatskartan.se");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await client.GetAsync($"/api/eventsNearby?lat=62.390811&lng=17.306927");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    root = JsonConvert.DeserializeObject<BrottsplatsDataResult>(data);
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
