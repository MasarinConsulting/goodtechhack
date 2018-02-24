using DataImporters;
using DataImporters.Luftkvalitet;
using Masarin.GTH.Search;
using Microsoft.Azure.Search;
using System;
using System.Configuration;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                // Sökklient
                
                var searchServiceName = ConfigurationManager.AppSettings.Get("SearchServiceName");
                var apiKey = ConfigurationManager.AppSettings.Get("SearchServiceApiKey");
                
                var searchClient = new SearchServiceClient(searchServiceName, new SearchCredentials(apiKey));
                
                
                Console.WriteLine("Sök. Välj vad du vill göra:");
                Console.WriteLine("");
                
                Console.WriteLine("");
                Console.WriteLine("--- Söka --- ");

                Console.WriteLine("S = Sök");

                Console.WriteLine("H = Hämta ett event");
                
                Console.WriteLine("");
                Console.WriteLine("--- Ladda återskapa ---");
                Console.WriteLine("F = Fyll index");
                Console.WriteLine("I = Skapa index, ingen laddning");
                Console.WriteLine("");
                Console.WriteLine("Q = Avsluta");

                var key = Console.ReadKey();
                Console.WriteLine("");


                // Logger

                // Index handler
                var indexHandler = new EventSearchIndex(searchClient);
                

               

                // Sökningar
                if (key.Key == ConsoleKey.S)
                {
                    var result = indexHandler.SearchEvents("", "", "asc", 1000, 0, "");
                    result.Wait();

                    foreach(var ev in result.Result.EventSearchDocuments)
                    {
                        Console.WriteLine("" + ev.Title);
                    }
                }



                else if (key.Key == ConsoleKey.H)
                {
                    
                }


                //Indexeringar
                else if (key.Key == ConsoleKey.I)
                {
                    var task = indexHandler.RecreateEventIndex();
                    task.Wait();
                }
                else if (key.Key == ConsoleKey.F)
                {
                    var taskRecreate = indexHandler.RecreateEventIndex();
                    taskRecreate.Wait();

                    //var eventDocuments = new List<EventSearchDocument>() {
                    //    //new EventSearchDocument()
                    //    //{
                    //    //    EventId = "1",
                    //    //     EventDate = DateTime.Now.ToString(),
                    //    //     Description = "Lampan är förstörd",
                    //    //     Title = "Trasig lampa",
                    //    //     Location = GeographyPoint.Create(2, 3),
                    //    //     Category = "safety", // crime, traffic, air
                    //    //     Level = "0"
                    //    //}
                    //};

                    // Brottsplats
                    var task = BrottsplatsImporter.GetData();
                    task.Wait();
                    var eventDocuments = task.Result;
                    var indexTask = indexHandler.IndexDocuments(eventDocuments);
                    indexTask.Wait();

                    // Luftkvalitet
                    var taskLuft = LuftkvalitetImporter.GetData();
                    taskLuft.Wait();
                    var luftEventDocuments = taskLuft.Result;
                    var indexTaskLuft = indexHandler.IndexDocuments(luftEventDocuments);
                    indexTaskLuft.Wait();

                    // Mockade rapporter
                    var taskReport = MockedReportsImporter.GetData();
                    taskReport.Wait();
                    var reportEventDocuments = taskReport.Result;
                    var indexTaskreport = indexHandler.IndexDocuments(reportEventDocuments);
                    indexTaskreport.Wait();

                }


                //Avslut
                else if (key.Key == ConsoleKey.Q)
                {
                    break;
                }
                
            }

            Console.WriteLine("Klar.");
            Console.ReadLine();

        }


    }
}
