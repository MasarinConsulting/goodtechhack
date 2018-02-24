using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporters.Luftkvalitet
{

    public class LuftkvalitetDataResult
    {
        public string status { get; set; }
        public Data data { get; set; }
    }
    
    public class Data
    {
        public Pm10[] pm10 { get; set; }
        public No2[] no2 { get; set; }
    }

    public class Pm10
    {
        public DateTime date { get; set; }
        public float value { get; set; }
        public string station { get; set; }
    }

    public class No2
    {
        public DateTime date { get; set; }
        public float value { get; set; }
        public string station { get; set; }
    }


}
