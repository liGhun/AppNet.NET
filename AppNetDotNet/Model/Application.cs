using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model
{
    public class Application
    {
        // not called "App" like in the documentation due to reserved wording in .NET
        public string client_id { get; set; }
        public string link { get; set; }
        public string name { get; set; }
    }
}
