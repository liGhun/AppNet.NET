using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.ApiCalls
{
    public class Parameters
    {
        public string since_id { get; set; }
        public string before_id { get; set; }
        public int count { get; set; }
        public int include_muted { get; set; }
        public int include_deleted { get; set; }
        public int include_directed_posts { get; set; }
        public int include_machine { get; set; }
        public int include_annotations { get; set; }
        public int include_starred_by { get; set; }
        public int include_reposters { get; set; }
        public int include_user { get; set; }
    }
}
