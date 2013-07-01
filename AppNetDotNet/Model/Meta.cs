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

        // Streaming parameters
        public List<string> subscription_ids { get; set; }
        public string subscription_id { get; set; }
        public string connection_id { get; set; }
        public bool is_deleted { get; set; }
        public string deleted_id { get; set; }
    }
}
