using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model
{
    public class Message
    {
        public string id { get; set; }
        public string channel_id { get; set; }
        public User user { get; set; }
        public DateTime created_at { get; set; }
        public string text { get; set; }
        public string html { get; set; }
        public Application source { get; set; }
        public string reply_to { get; set; }
        public string thread_id { get; set; }
        public int num_replies { get; set; }
        public List<Annotation> annotations { get; set; }
        public Entities entities { get; set; }
        public bool is_deleted { get; set; }
        public bool machine_only { get; set; }
        public List<string> destinations { get; set; }

        public override string ToString()
        {
            string returnString = "";
            if (!string.IsNullOrEmpty(id))
            {
                returnString += id + ": ";
            }
            if (!string.IsNullOrEmpty(text))
            {
                returnString += text;
            }
            if (user != null)
            {
                returnString += " by " + user;
            }
            return returnString;
        }
    }
}
