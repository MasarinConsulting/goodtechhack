using Microsoft.Azure.Search;
using Microsoft.Spatial;
using System;

namespace Masarin.GTH.Search
{
    public class EventSearchDocument
    {

        [System.ComponentModel.DataAnnotations.Key]
        [IsFilterable]
        public string EventId { get; set; }

        [IsSearchable, IsFilterable, IsSortable,]
        public string Title { get; set; }

        [IsSearchable, IsFilterable, IsSortable,]
        public string Description { get; set; }

        [IsSearchable, IsFilterable, IsSortable,]
        public string EventDate { get; set; }

        [IsFilterable, IsSortable,]
        public DateTimeOffset EventDateOffset { get; set; }

        [IsFilterable, IsSortable]
        public GeographyPoint Location { get; set; }

        [IsSearchable, IsFilterable, IsSortable, IsFacetable]
        public string Category { get; set; }

        [IsSearchable, IsFilterable, IsSortable]
        public string Level { get; set; }

        [IsFilterable, IsSortable]
        public string DataType { get; set; } // api, personal

        [IsSearchable, IsFilterable, IsSortable]
        public string DataSource { get; set; }
    }
}