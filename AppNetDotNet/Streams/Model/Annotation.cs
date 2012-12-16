using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Streams.Model
{
    public class Annotation
    {
        public string type { get; set; }
        public Dictionary<string,string> value { get; set; }
    }
}
