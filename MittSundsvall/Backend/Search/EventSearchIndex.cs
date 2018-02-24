using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace Masarin.GTH.Search
{

    public class EventSearchIndex
    {

        private const string INDEX_EVENTS = "events";
        private const string SUGGESTION_EVENTS = "eventssuggestions";

        private ISearchServiceClient _searchServiceClient;
        
        

        public EventSearchIndex(ISearchServiceClient searchServiceClient)
        {
            _searchServiceClient = searchServiceClient;
        }
        

        public async Task RecreateEventIndex()
        {
            Console.WriteLine("{0}", "Creating index...\n");
            if (await _searchServiceClient.Indexes.ExistsAsync(INDEX_EVENTS))
            {
                
                await _searchServiceClient.Indexes.DeleteAsync(INDEX_EVENTS);
                
            }

            var definition = new Index()
            {
                Name = "events",
                Fields = FieldBuilder.BuildForType<EventSearchDocument>()
            };

            _searchServiceClient.Indexes.Create(definition);

            //var definition = new Index()
            //{
            //    Name = INDEX_EVENTS,
            //    Fields = new[]
            //        {
            //        new Field("EventId", DataType.String)                 { IsKey = true },
            //        new Field("Title", DataType.String)                   { IsSearchable = true, IsFilterable = true, IsSortable = true },
            //        new Field("Description", DataType.String)             { IsSearchable = true, IsFilterable = true, IsSortable = true },
            //        new Field("EventDate", DataType.String)                    { IsSearchable = true, IsFilterable = true, IsSortable = true },
            //        new Field("Location", DataType.GeographyPoint)        { IsSearchable = false, IsFilterable = true, IsSortable = true },
            //        new Field("Category", DataType.String)                    { IsSearchable = true, IsFilterable = true, IsSortable = true },


            //    },
            //    Suggesters = new[] { new Suggester {
            //        Name = SUGGESTION_EVENTS,
            //        SearchMode = SuggesterSearchMode.AnalyzingInfixMatching,
            //        SourceFields = new string[] {
            //            "Title",
            //            "Description",
            //            "Category"
            //        }
            //    } }

            //};
            //try
            //{
            //    await _searchServiceClient.Indexes.CreateAsync(definition);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Något gick fel vid uppskapandet av produktionsresultatindex " + e);
            //}

        }


        public async Task<bool> IndexDocuments(List<EventSearchDocument> documents)
        {
            return await IndexHelper.IndexDocuments<EventSearchDocument>(_searchServiceClient, documents, INDEX_EVENTS);
        }

        public async Task<List<string>> SuggestEvents(string searchText, int take, string[] searchFields)
        {
            var indexClientProduktionsunderlag = _searchServiceClient.Indexes.GetClient(INDEX_EVENTS);

            var sp = new SuggestParameters
            {
                Top = take
            };

            //Om sökfält är angivet sök i detta, annars sök i alla som igår i suggestern och är sökbara
            if (searchFields != null && searchFields.Count() != 0)
                sp.SearchFields = searchFields;

            var response = await indexClientProduktionsunderlag.Documents.SuggestAsync<EventSearchDocument>(searchText, SUGGESTION_EVENTS, suggestParameters: sp);

            List<string> suggestions = new List<string>();
            foreach (var result in response.Results)
            {
                suggestions.Add(result.Text);
            }

            List<string> uniqueSuggestions = suggestions.Distinct().ToList();

            return uniqueSuggestions;

        }
        

        public async Task<EventSearchResultDocument> SearchEvents(string searchText, string sortBy, string sortAscDesc, int take, int skip, string filterExpression)
        {
            var eventSearchResultDocument = new EventSearchResultDocument();

            var sortOrder = "asc";
            if (!string.IsNullOrEmpty(sortAscDesc))
            {
                if (sortAscDesc.ToLower() == "desc")
                {
                    sortOrder = "desc";
                }
            }

            var spProduktionsresultat = new SearchParameters
            {
                Top = take,
                Skip = skip,
                IncludeTotalResultCount = true,
                SearchMode = SearchMode.All,
                QueryType = QueryType.Full,
                OrderBy = new[] { $"EventDate {sortOrder}" }
            };

            // Orderby
            if (!string.IsNullOrEmpty(sortBy))
            {
                var propertyname = new EventSearchDocument().GetType().GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.Name;
                if (!string.IsNullOrEmpty(propertyname))
                {
                    spProduktionsresultat.OrderBy = new[] { $"{propertyname} {sortOrder}" };
                }
            }

            // Filter
            if (!string.IsNullOrEmpty(filterExpression))
            {
                //spProduktionsunderlag.Filter = string.Format("InternStatusKod eq '{0}'", internStatusFilter);
                spProduktionsresultat.Filter = filterExpression;
            }

            var indexClientEvents = _searchServiceClient.Indexes.GetClient(INDEX_EVENTS);

            try
            {
                var response = await indexClientEvents.Documents.SearchAsync<EventSearchDocument>(searchText, spProduktionsresultat);

                eventSearchResultDocument.EventSearchDocuments = response.Results.Select(s => s.Document).ToList();
                eventSearchResultDocument.TotalCount = response.Count ?? 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApplicationException(e.Message);
            }



            return eventSearchResultDocument;
        }
        
        public async Task Remove(List<string> produktionsresultatIdentitetList)
        {
            if (produktionsresultatIdentitetList != null && produktionsresultatIdentitetList.Any())
            {
                try
                {
                    var batch = IndexBatch.Delete("ProduktionsresultatIdentitet", produktionsresultatIdentitetList);

                    var indexClient = _searchServiceClient.Indexes.GetClient(INDEX_EVENTS);
                    await indexClient.Documents.IndexAsync(batch);

                }
                catch (Exception e)
                {
                    throw new ApplicationException(e.Message);
                }
            }


        }
    }
}