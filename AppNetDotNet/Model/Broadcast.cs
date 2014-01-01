using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model
{
    public class Broadcast
    {
        public class Broadcast_Channel
        {
            public string title { get; set; }
            public string description { get; set; }
            public List<string> tags { get; set; }

            public Channel raw_base_channel { get; set; }

            public Broadcast_Channel(Channel channel)
            {
                raw_base_channel = channel;

                if (channel != null)
                {
                    if (channel.annotations != null)
                    {
                        foreach (Annotation annotation in channel.annotations)
                        {
                            switch (annotation.type)
                            {
                                case "net.app.core.broadcast.metadata":
                                    AppNetDotNet.Model.Annotations.Broadcast_Channel_Metadata channel_meta = annotation.parsedObject as AppNetDotNet.Model.Annotations.Broadcast_Channel_Metadata;
                                    if (channel_meta != null)
                                    {
                                        this.title = channel_meta.title;
                                        this.description = channel_meta.description;
                                        this.tags = channel_meta.tags;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            public override string ToString()
            {
                return title;
            }
        }

        public class Broadcast_Message
        {
            public string headline { get; set; }
            public string read_more_url { get; set; }
            public string text { get; set; }
            public string image_url { get; set; }

            public Message raw_base_message { get; set; }

            public Broadcast_Message(Message message)
            {
                raw_base_message = message;
                if (message != null)
                {
                    this.text = message.text;
                    if (message.annotations != null)
                    {
                        foreach (Annotation annotation in message.annotations)
                        {
                            switch (annotation.type)
                            {
                                case "net.app.core.oembed":
                                    AppNetDotNet.Model.Annotations.EmbeddedMedia media = annotation.parsedObject as AppNetDotNet.Model.Annotations.EmbeddedMedia;
                                    if (media != null)
                                    {
                                        if (!string.IsNullOrEmpty(media.thumbnail_url) || !string.IsNullOrEmpty(media.url))
                                        {
                                            if (string.IsNullOrEmpty(media.thumbnail_url))
                                            {
                                                media.thumbnail_url = media.url;
                                            }
                                            image_url = media.thumbnail_url;
                                        }
                                    }
                                    break;

                                case "net.app.core.broadcast.message.metadata":
                                    AppNetDotNet.Model.Annotations.Broadcast_Message_Metadata broadcast = annotation.parsedObject as AppNetDotNet.Model.Annotations.Broadcast_Message_Metadata;
                                    if (broadcast != null)
                                    {
                                        this.headline = broadcast.subject;
                                    }
                                    break;

                                case "net.app.core.crosspost":
                                    AppNetDotNet.Model.Annotations.Crosspost crosspost = annotation.parsedObject as AppNetDotNet.Model.Annotations.Crosspost;
                                    if (crosspost != null)
                                    {
                                        this.read_more_url = crosspost.canonical_url;
                                    }
                                    break;
                            }
                        }
                    }
                }
                if(string.IsNullOrEmpty(headline)) {
                    // no valid broadcast message!
                }
            }

            public override string ToString()
            {
                if (!string.IsNullOrEmpty(headline))
                {
                    return headline;
                }
                else
                {
                    return "Missing headline - no broadcast entry";
                }
            }
        }
    }
}
