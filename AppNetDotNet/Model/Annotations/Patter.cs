using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model.Annotations
{
    public class Patter
    {
        /// <summary>
        /// User-facing name for the room. Need not be unique.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// User description of the room.
        /// </summary>
        public string blurb { get; set; }
        /// <summary>
        /// Zero or more identifiers categorizing the room
        /// categories is a list of identifiers. The following identifiers are recognized by patter-app.net: 'fun', 'lifestyle', 'profession', 'language', 'community', 'tech', 'event'. If a channel does not have a recognized category, it should be listed as a 'general' room.
        /// </summary>
        public List<string> categories { get; set; }
        /// <summary>
        /// Message id of channel invite to the room.
        /// The blurb_id indicates a net.app.core.channel.invite message posted to channel 1614. When a user wishes to remove their channel from promotion, that message should be removed and the blurb, categories, and blurb_id fields should be removed from the channel annotation.
        /// </summary>
        public string blurb_id { get; set; }
    }
}
