using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Model;
using Newtonsoft.Json;

namespace AppNetDotNet.ApiCalls
{
    public class Broadcasts
    {
        // public Broadcast.Channel create_channel(string title, string description, List<string> editors, bool public = true, 

        public static Tuple<Channel, ApiCallResponse> subscribe(string access_token, string id)
        {
            return Channels.Subscriptions.subscribe(access_token, id);
        }

        public static Tuple<Channel, ApiCallResponse> unsubscribe(string access_token, string id)
        {
            return Channels.Subscriptions.unsubscribe(access_token, id);
        }

        public static Tuple<List<Broadcast.Broadcast_Channel>, ApiCallResponse> getOfCurrentUser(string access_token)
        {
            Channels.channelParameters parameters = new Channels.channelParameters();
            parameters.channel_types = "net.app.core.broadcast";
            parameters.include_annotations = true;
            Tuple<List<Channel>, ApiCallResponse> channels = Channels.Subscriptions.getOfCurrentUser(access_token, parameters);
            if (channels.Item2.success)
            {
                List<Broadcast.Broadcast_Channel> broadcast_channels = new List<Broadcast.Broadcast_Channel>();
                foreach (Channel channel in channels.Item1)
                {
                    broadcast_channels.Add(new Broadcast.Broadcast_Channel(channel));
                }
                return new Tuple<List<Broadcast.Broadcast_Channel>, ApiCallResponse>(broadcast_channels, channels.Item2);
            }
            else
            {
                return new Tuple<List<Broadcast.Broadcast_Channel>, ApiCallResponse>(null, channels.Item2);
            }

        }

        public static Tuple<Broadcast.Broadcast_Channel, ApiCallResponse> get(string access_token, string id)
        {
            Channels.channelParameters parameters = new Channels.channelParameters();
            parameters.channel_types = "net.app.core.broadcast";
            parameters.include_annotations = true;
            Tuple<Channel,ApiCallResponse> channel = Channels.get(access_token, id);
            if (channel.Item2.success)
            {
                return new Tuple<Broadcast.Broadcast_Channel, ApiCallResponse>(new Broadcast.Broadcast_Channel(channel.Item1), channel.Item2);
            }
            else
            {
                return new Tuple<Broadcast.Broadcast_Channel, ApiCallResponse>(null, channel.Item2);
            }

        }
        
        

        public static Tuple<List<Broadcast.Broadcast_Message>, ApiCallResponse> getMessagesInChannel(string accessToken, string channelId)
        {
            List<Broadcast.Broadcast_Message> broadcasts = new List<Broadcast.Broadcast_Message>();
            Messages.messageParameters parameters = new Messages.messageParameters();
            parameters.include_message_annotations = true;
            parameters.include_annotations = true;
            parameters.include_user_annotations = true;
            Tuple<List<Message>, ApiCallResponse> entries = AppNetDotNet.ApiCalls.Messages.getMessagesInChannel(accessToken, channelId, parameters);
            if (entries.Item2.success)
            {
                foreach (Message message in entries.Item1)
                {
                    broadcasts.Add(new Broadcast.Broadcast_Message(message));
                }
            }

            return new Tuple<List<Broadcast.Broadcast_Message>, ApiCallResponse>(broadcasts, entries.Item2);
        }

        public static Tuple<Message, ApiCallResponse> create_message(
            string access_token,
            string channel_id,
            string headline,
            string text = null,
            string read_more_link = null,
            List<File> toBeEmbeddedFiles = null)
        {
            Model.Annotations.Broadcast_Message_Metadata message_annotations = new Model.Annotations.Broadcast_Message_Metadata();
            message_annotations.subject = headline;
            List<Annotation> annotations = new List<Annotation>();

            Annotation annotation = new Annotation("net.app.core.broadcast.message.metadata", message_annotations);
            annotations.Add(annotation);

            if (!string.IsNullOrEmpty(read_more_link))
            {
                Model.Annotations.Crosspost crosspost_annotation = new Model.Annotations.Crosspost();
                crosspost_annotation.canonical_url = read_more_link;
                Annotation annotation_more_link = new Annotation("net.app.core.crosspost", crosspost_annotation);
                annotations.Add(annotation_more_link);
            }
            int machine_only = 1;
            if (!string.IsNullOrEmpty(text))
            {
                machine_only = 0;
            }
            return Messages.create(access_token, text, channel_id, null, annotations: annotations, machineOnly: machine_only, parse_links: true,
                parse_markdown_links: true, toBeEmbeddedFiles: toBeEmbeddedFiles);
        }
    }
}
