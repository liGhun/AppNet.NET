using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace AppNetDotNet.Model
{
    public class Configuration
    {
        /// <summary>
        /// Configuration that is common to all text fields (post.text, message.text, user.description.text).
        /// Object is not parsed as documentation is unclear on structure
        /// </summary>
        public Newtonsoft.Json.Linq.JObject text { get; set; }
        /// <summary>
        /// The configuration related to User objects.
        /// </summary>
        public ResourceConfiguration user { get; set; }
        /// <summary>
        /// he configuration related to File objects.
        /// </summary>
        public ResourceConfiguration file { get; set; }
        /// <summary>
        /// The configuration related to Post objects.
        /// </summary>
        public ResourceConfiguration post { get; set; }
        /// <summary>
        /// The configuration related to Message objects.
        /// </summary>
        public ResourceConfiguration message { get; set; }
        /// <summary>
        /// The configuration related to Channel objects.
        /// annotation_max_bytes does not apply here
        /// </summary>
        public ResourceConfiguration channel { get; set; }
    }

    public class ResourceConfiguration
    {
        /// <summary>
        /// The maximum number of bytes that can be attached to this type of object as an Annotation
        /// </summary>
        public int annotation_max_bytes { get; set; }
        /// <summary>
        /// The maximum number of characters that an instance of this object can be. For User objects, this applies to the User description. This field does not apply to Channel objects.
        /// </summary>
        public int text_max_length { get; set; }
    }
}
