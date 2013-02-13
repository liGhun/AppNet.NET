using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model.Annotations
{
    public class Presence
    {
        /// <summary>
        /// Presence codes
        /// available	The entity or resource is open to receiving messages and is likely to respond.
        /// busy	The entity or resource is open to receiving messages, however, a response should not be expected.
        /// away	The entity or resource is temporarily away.
        /// dnd	The entity or resource is busy (dnd = "Do Not Disturb"). This is similar to busy, however, it is more emphatic. The entity or resource is not open to receiving messages.
        /// xa	The entity or resource is away for an extended period (xa = "eXtended Away").
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// User supplied text.
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// User supplied URL.
        /// </summary>
        public string link { get; set; }
        /// <summary>
        /// The time at which the status was last changed in ISO 8601 format.
        /// </summary>
        public string updated_at { get; set; }
    }
}
