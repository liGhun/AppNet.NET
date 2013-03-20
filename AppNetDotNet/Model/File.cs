using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model
{
    public class File
    {
        public List<Annotation> annotations { get; set; }
        public Boolean complete { get; set; }
        public DerivedFile derived_files { get; set; }
        public string file_token { get; set; }
        public string id { get; set; }
        public string kind { get; set; }
        public string mime_type { get; set; }
        public string name { get; set; }
        public string sha1 { get; set; }
        public int size { get; set; }
        public Source source { get; set; }
        public int total_size { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public DateTime url_expires
        {
            get
            {
                return _url_expires;
            }
            set
            {
                _url_expires = value.ToLocalTime();
            }
        }
        /// <summary>
        /// User supplied text of the post.
        /// </summary>
        private DateTime _url_expires { get; set; }
        public User user { get; set; }

        public override string ToString()
        {
            return name;
        }

        public class Source
        {
            public string name { get; set; }
            public string link { get; set; }
            public string client_id { get; set; }
        }
        
        public class DerivedFile
        {
            //public Tuple<string,DerivedFileKeys> value { get; set; }
            public DerivedFileKeys image_thumb_200s { get; set; }
            public DerivedFileKeys image_thumb_960r { get; set; }
        }
     

        public class DerivedFileKeys
        {
            public string mime_type { get; set; }
            public string sha1 { get; set; }
            public int size { get; set; }
            public string url { get; set; }
            public DateTime url_expires
            {
                get
                {
                    return _url_expires;
                }
                set
                {
                    _url_expires = value.ToLocalTime();
                }
            }
            /// <summary>
            /// User supplied text of the post.
            /// </summary>
            private DateTime _url_expires { get; set; }
        }
    }
}
