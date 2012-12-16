using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Streams.Model
{
    public class Entities
    {
        public List<Mention> mentions { get; set; }
        public List<Hashtag> hashtags { get; set; }
        public List<Link> links { get; set; }

        public class Mention
        {
            public string name { get; set; }
            public string id { get; set; }
            public int pos { get; set; }
            public int len { get; set; }
        }

        public class Hashtag
        {
            public string name { get; set; }
            public int pos { get; set; }
            public int len { get; set; }
        }

        public class Link {
            public string text { get; set; }
            public string url { get; set; }
            public int pos { get; set; }
            public int len { get; set; }
        }
    }
}
