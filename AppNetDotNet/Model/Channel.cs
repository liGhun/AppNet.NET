using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model
{
    public class Channel
    {
        public string id { get; set; }
        public string type { get; set; }
        public User owner { get; set; }
        public List<Annotation> annotations { get; set; }
        public ACL readers { get; set; }
        public ACL writers { get; set; }
        public bool you_subscribed { get; set; }
        public bool you_can_edit { get; set; }
        public bool has_unread { get; set; }

        public override string ToString()
        {
            string returnCode = "";

            if(!string.IsNullOrEmpty(id)) {
                returnCode += id + ": ";
            }
            if (!string.IsNullOrEmpty(type))
            {
                returnCode += "\"" + type + "\"";
            }
            if (owner != null)
            {
                returnCode += " of user " + owner;
            }

            return returnCode;
        }
    }
}
