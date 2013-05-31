﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model
{
    public class User
    {
        public override string ToString()
        {
            if (username != null && name != null)
            {
                return "@" + username + " (" + name + ")";
            }
            return "Incomplete user";
        }

        public string id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public Description description { get; set; }
        public List<Annotation> annotations { get; set; }

        public string timezone { get; set; }
        public string locale { get; set; }
        public Image avatar_image { get; set; }
        public string type { get; set; }
        public Image cover_image { get; set; }
        public DateTime created_at
        {
            get
            {
                return _created_at;
            }
            set
            {
                _created_at = value.ToLocalTime();
            }
        }
        /// <summary>
        /// User supplied text of the post.
        /// </summary>
        private DateTime _created_at { get; set; }
        public Counts counts { get; set; }

        public bool follows_you { get; set; }
        public bool you_follow { get; set; }
        public bool you_muted { get; set; }
        public bool you_blocked { get; set; }
        public bool you_can_subscribe { get; set; }
        public string verified_domain { get; set; }

        public class Counts
        {
            public int following { get; set; }
            public int followers { get; set; }
            public int posts { get; set; }
            public int stars { get; set; }
        }

        public class Description
        {
            public string text { get; set; }
            public string html { get; set; }
            public Entities entities { get; set; }
        }
    }
}
