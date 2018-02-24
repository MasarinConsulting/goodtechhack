using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Masarin.GTH.WebApi.Models
{
    public class ReportEventVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }
        public string Category { get; set; }
        public LocationVM Location { get; set; }
        public string UserId { get; set; }
    }
}