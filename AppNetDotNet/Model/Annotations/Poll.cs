using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model.Annotations
{
    public class Poll
    {
        /// <summary>
        /// Placed on Channels
        /// </summary>
        public string question { get; set; }
        /// <summary>
        /// Placed on Channels
        /// </summary>
        public List<string> options { get; set; }
        /// <summary>
        /// Placed on Messages
        /// </summary>
        public string answer { get; set; }
    }
}
