using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Masarin.GTH.Search
{
    public class EventSearchResultDocument
    {
        public long TotalCount { get; set; }

        public List<EventSearchDocument> EventSearchDocuments { get; set; }
    }
}