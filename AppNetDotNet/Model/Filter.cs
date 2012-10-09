using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model
{
    public class Filter
    {
        public string id { get; set; }
        public string type { get; set; }
        public List<string> user_ids { get; set; }
        public List<string> hashtags { get; set; }
        public List<string> link_domains { get; set; }
        public List<string> mention_user_ids { get; set; }
    }
}
