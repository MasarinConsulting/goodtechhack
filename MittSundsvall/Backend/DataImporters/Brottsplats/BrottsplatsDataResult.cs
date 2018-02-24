using System;
using System.Collections.Generic;

public class Meta
{
    public int nearbyInKm { get; set; }
    public int nearbyCount { get; set; }
    public int numTries { get; set; }
}

public class Location
{
    public string name { get; set; }
    public int prio { get; set; }
}

public class Datum
{
    public int id { get; set; }
    public DateTime pubdate_iso8601 { get; set; }
    public string pubdate_unix { get; set; }
    public string title_type { get; set; }
    public string title_location { get; set; }
    public string description { get; set; }
    public string content { get; set; }
    public List<Location> locations { get; set; }
    public double lat { get; set; }
    public double lng { get; set; }
    public string viewport_northeast_lat { get; set; }
    public string viewport_northeast_lng { get; set; }
    public string viewport_southwest_lat { get; set; }
    public string viewport_southwest_lng { get; set; }
    public string image { get; set; }
}

public class BrottsplatsDataResult
{
    public List<object> links { get; set; }
    public Meta meta { get; set; }
    public List<Datum> data { get; set; }
}