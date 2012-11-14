using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model
{
    public class Meta
    {
        public int code { get; set; }
        public StreamMarker marker { get; set; }
        public string max_id { get; set; }
        public string min_id { get; set; }
        public bool more { get; set; }
    }
}
