using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model
{
    public class Stream
    {
        public Int64 sent { get; set; }
        public Double buffer { get; set; }
        public List<Filter> filters { get; set; }
        public Dictionary<string,string> links { get; set; }
        
    }
}
