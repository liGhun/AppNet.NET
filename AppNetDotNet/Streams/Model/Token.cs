using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Streams.Model
{
    public class Token
    {
        public string client_id { get; set; }
        public Application app { get; set; }
        public List<string> scopes { get; set; }
        public User user { get; set; }        
    }
}
