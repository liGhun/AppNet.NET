using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model
{
    public class ACL
    {
        public bool any_user { get; set; }
        public bool immutable { get; set; }
        public bool Public { get; set; }
        public List<string> user_ids { get; set; }
        public bool you { get; set; }
    }
}
