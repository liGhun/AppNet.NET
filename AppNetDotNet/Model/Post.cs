using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNetDotNet.Model
{
    public class Post
    {
        public override string ToString()
        {
            if (user != null && text != null)
            {
                return user.ToString() + ": " +  text;
            }
            return "Incomplete post";
        }

        public string id { get; set; }
        public User user { get; set; }
        public DateTime created_at { get; set; }
        public string text { get; set; }
        public string html { get; set; }
        public string reply_to { get; set; }
        public string canonical_url { get; set; }
        public string thread_id { get; set; }
        public int num_replies { get; set; }
        public int num_stars { get; set; }
        public int num_reposts { get; set; }
        public List<Annotation> annotations { get; set; }
        public Entities entities { get; set; }
        public bool is_deleted { get; set; }
        public bool machine_only { get; set; }
        public bool you_starred { get; set; }
        public List<User> starred_by { get; set; }
        public bool you_reposted { get; set; }
        public List<User> reposters { get; set; }
        public Post repost_of { get; set; }

        public class Source
        {
            public string name { get; set; }
            public string link { get; set; }
        }

        
    }
}
