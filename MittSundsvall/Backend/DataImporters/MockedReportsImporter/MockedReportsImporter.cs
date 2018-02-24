using Masarin.GTH.Search;
using Microsoft.Spatial;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataImporters
{
    public class MockedReportsImporter
    {

        static public async Task<List<EventSearchDocument>> GetData()
        {
            Random r = new Random();
            double factor = 0.03;
            double lat = 62.3900953;
            double lng = 17.3104547;
            string datatype = "personal";

            List<EventSearchDocument> documentList = new List<EventSearchDocument>()
            {
                new EventSearchDocument(){
                    Category = "crime",
                    DataType = datatype,
                    Description = "Inbrott i villa",
                    Title = "Inbrott",
                    EventDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventDateOffset = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                    EventId = Guid.NewGuid().ToString(),
                    Level = "0",
                    Location = GeographyPoint.Create(lat + (r.NextDouble()-0.5)*factor ,lng + (r.NextDouble()-0.5)*factor)
                },
                new EventSearchDocument(){
                    Category = "crime",
                    DataType = datatype,
                    Description = "Främmande man beter sig hotfullt",
                    Title = "Hot",
                    EventDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventDateOffset = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                    EventId = Guid.NewGuid().ToString(),
                    Level = "0",
                    Location = GeographyPoint.Create(lat + (r.NextDouble()-0.5)*factor ,lng + (r.NextDouble()-0.5)*factor)
                },
                new EventSearchDocument(){
                    Category = "safety",
                    DataType = datatype,
                    Description = "Gatlykta sönderslagen",
                    Title = "Skadegörelse",
                    EventDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventDateOffset = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                    EventId = Guid.NewGuid().ToString(),
                    Level = "0",
                    Location = GeographyPoint.Create(lat + (r.NextDouble()-0.5)*factor ,lng + (r.NextDouble()-0.5)*factor)
                },
                new EventSearchDocument(){
                    Category = "air",
                    DataType = datatype,
                    Description = "Dålig luft p.g.a. avgaser",
                    Title = "Dålig luft",
                    EventDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventDateOffset = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                    EventId = Guid.NewGuid().ToString(),
                    Level = "0",
                    Location = GeographyPoint.Create(lat + (r.NextDouble()-0.5)*factor ,lng + (r.NextDouble()-0.5)*factor)
                },
                new EventSearchDocument(){
                    Category = "air",
                    DataType = datatype,
                    Description = "Illaluktande doft i området",
                    Title = "Illaluktande doft",
                    EventDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventDateOffset = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                    EventId = Guid.NewGuid().ToString(),
                    Level = "0",
                    Location = GeographyPoint.Create(lat + (r.NextDouble()-0.5)*factor ,lng + (r.NextDouble()-0.5)*factor)
                },
                new EventSearchDocument(){
                    Category = "traffic",
                    DataType = datatype,
                    Description = "Fortkörning i bostadsområde",
                    Title = "Fortkörning",
                    EventDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventId = Guid.NewGuid().ToString(),
                    Level = "0",
                    Location = GeographyPoint.Create(lat + (r.NextDouble()-0.5)*factor ,lng + (r.NextDouble()-0.5)*factor)
                },
                new EventSearchDocument(){
                    Category = "safety",
                    DataType = datatype,
                    Description = "Gatlykta fungerar inte",
                    Title = "Gatlykta",
                    EventDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventDateOffset = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                    EventId = Guid.NewGuid().ToString(),
                    Level = "0",
                    Location = GeographyPoint.Create(lat + (r.NextDouble()-0.5)*factor ,lng + (r.NextDouble()-0.5)*factor)
                },
                new EventSearchDocument(){
                    Category = "traffic",
                    DataType = datatype,
                    Description = "Omkörning på fel sida",
                    Title = "Omkörning på fel sida",
                    EventDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventId = Guid.NewGuid().ToString(),
                    Level = "0",
                    Location = GeographyPoint.Create(lat + (r.NextDouble()-0.5)*factor ,lng + (r.NextDouble()-0.5)*factor)
                },
                new EventSearchDocument(){
                    Category = "traffic",
                    DataType = datatype,
                    Description = "Viltolycka, ena vägfilen avstängd",
                    Title = "Viltolycka",
                    EventDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventDateOffset = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                    EventId = Guid.NewGuid().ToString(),
                    Level = "0",
                    Location = GeographyPoint.Create(lat + (r.NextDouble()-0.5)*factor ,lng + (r.NextDouble()-0.5)*factor)
                },
                new EventSearchDocument(){
                    Category = "safety",
                    DataType = datatype,
                    Description = "Trasig lampa på gångväg",
                    Title = "Trasig lampa",
                    EventDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventDateOffset = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                    EventId = Guid.NewGuid().ToString(),
                    Level = "0",
                    Location = GeographyPoint.Create(lat + (r.NextDouble()-0.5)*factor ,lng + (r.NextDouble()-0.5)*factor)
                },
                new EventSearchDocument(){
                    Category = "safety",
                    DataType = datatype,
                    Description = "Gatlyktan i passagen trasig",
                    Title = "Trasig gatlykta", 
                    EventDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventDateOffset = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                    EventId = Guid.NewGuid().ToString(),
                    Level = "0",
                    Location = GeographyPoint.Create(lat + (r.NextDouble()-0.5)*factor ,lng + (r.NextDouble()-0.5)*factor)
                },
                new EventSearchDocument(){
                    Category = "safety",
                    DataType = datatype,
                    Description = "Gatlyktan i passagen trasig",
                    Title = "Trasig gatlykta",
                    EventDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventDateOffset = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                    EventId = Guid.NewGuid().ToString(),
                    Level = "0",
                    Location = GeographyPoint.Create(lat + (r.NextDouble()-0.5)*factor ,lng + (r.NextDouble()-0.5)*factor)
                },
                new EventSearchDocument(){
                    Category = "safety",
                    DataType = datatype,
                    Description = "Gatlyktan i passagen trasig",
                    Title = "Trasig gatlykta",
                    EventDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventDateOffset = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                    EventId = Guid.NewGuid().ToString(),
                    Level = "0",
                    Location = GeographyPoint.Create(lat + (r.NextDouble()-0.5)*factor ,lng + (r.NextDouble()-0.5)*factor)
                }
            };
            return documentList;
        }
    }
}
