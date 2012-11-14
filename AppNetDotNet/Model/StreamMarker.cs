using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model
{
    public class StreamMarker
    {
        public string id { get; set; }
        public string name { get; set; }
        public int percentage { get; set; }
        public DateTime updated_at { get; set; }
        public string version { get; set; }
    }
}
