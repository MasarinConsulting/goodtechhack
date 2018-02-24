using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masarin.GTH.Search
{
    public class IndexHelper
    {
        public static async Task<bool> IndexDocuments<T>(ISearchServiceClient searchServiceClient, List<T> documents, string indexName) where T : class
        {

            var i = 0;
            while (1 == 1)
            {
                i++;

                try
                {
                    await TryIndexDocuments<T>(searchServiceClient, documents, indexName);
                    break;
                }
                catch (IndexBatchException ex)
                {
                    if (i < 3)
                    {
                        Console.WriteLine("{0} indexering misslyckas, omförsök {1}", indexName, i);
                    }
                    else
                    {
                        Console.WriteLine("{0} indexering misslyckas, avbryter {1} {2}", indexName, i, ex);
                        throw ex;
                    }

                }
            }
            return true;
        }


        private static async Task TryIndexDocuments<T>(ISearchServiceClient searchServiceClient, List<T> documents, string indexName) where T : class
        {

            var indexClient = searchServiceClient.Indexes.GetClient(indexName);

            if (documents != null && documents.Count > 0)
            {
                Console.WriteLine("Pushing " + indexName + " to search " + documents.Count());
                var page = 1000;
                if (documents.Count() > page)
                {
                    var count = (documents.Count() / page) + 1;
                    Parallel.For(0, count, new ParallelOptions { MaxDegreeOfParallelism = 4 }, async x =>
                    {
                        var records = documents.Skip(x * page).Take(page).ToList();

                        var batch = IndexBatch.MergeOrUpload(records);
                        await indexClient.Documents.IndexAsync(batch);
                    });
                }
                else
                {
                    var batch = IndexBatch.MergeOrUpload(documents);
                    await indexClient.Documents.IndexAsync(batch);
                }
            }



        }
    }
}
