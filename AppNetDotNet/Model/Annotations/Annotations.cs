﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Model;
using Newtonsoft.Json;

namespace AppNetDotNet.Model
{
    public interface IAnnotation
    {
        string type { get; set; }
    }

    public class Annotation : IAnnotation
    {
        public Annotation()
        {
            parse_content = true;
        }

        public Annotation(bool auto_parse_content = true)
        {
            parse_content = auto_parse_content;
        }

        public Annotation(string annotation_type, object annotation_value_object, bool auto_parse_content = false)
        {
            parse_content = auto_parse_content;
            type = annotation_type;
            value = annotation_value_object;
        }

        public string json_body_string
        {
            get
            {         
                return JsonConvert.SerializeObject(json_body);
            }
        }

        public JSON_body json_body
        {
            get
            {
                JSON_body body = new JSON_body();
                body.type = type;
                body.value = value;
                return body;
            }
        }

        public class JSON_body
        {
            public string type { get; set; }
            public object value { get; set; }
        }

        private bool parse_content { get; set; }

        public object parsedObject { get; set; }

        public string type { 
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                if(_type != null && this.value != null && parse_content) {
                    parsedObject = getParsedObject();
                }
            }
        }
        private string _type { get;set; }

        public object value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                if (_value != null && type != null && parse_content)
                {
                    parsedObject = getParsedObject();
                }
            }
        }
        private object _value { get; set; }

        private object getParsedObject()
        {
            try
            {
                switch (this.type)
                {
                    case "net.app.core.crosspost":
                        {
                            return JsonConvert.DeserializeObject<Annotations.Crosspost>(value.ToString());
                        }
                    case "net.app.core.oembed":
                        {
                            return JsonConvert.DeserializeObject<Annotations.EmbeddedMedia>(value.ToString());
                        }
                    case "net.app.core.geolocation":
                        {
                            return JsonConvert.DeserializeObject<Annotations.GeoLocation>(value.ToString());
                        }
                    case "net.app.core.language":
                        {
                            return JsonConvert.DeserializeObject<Annotations.Language>(value.ToString());
                        }
                    case "net.app.core.directory.blog":
                        {
                            return JsonConvert.DeserializeObject<Annotations.Blog>(value.ToString());
                        }
                    case "net.app.core.directory.facebook":
                        {
                            return JsonConvert.DeserializeObject<Annotations.FacebookId>(value.ToString());
                        }
                    case "net.app.core.directory.homepage":
                        {
                            return JsonConvert.DeserializeObject<Annotations.Homepage>(value.ToString());
                        }
                    case "net.app.core.directory.twitter":
                        {
                            return JsonConvert.DeserializeObject<Annotations.TwitterUsername>(value.ToString());
                        }
                    case "net.patter-app.settings":
                        {
                            return JsonConvert.DeserializeObject<Annotations.Patter>(value.ToString());
                        }
                    case "net.questionapp.poll":
                        {
                            return JsonConvert.DeserializeObject<Annotations.Poll>(value.ToString());
                        }
                    case "net.app.core.fallback_url":
                        {
                            return JsonConvert.DeserializeObject<Annotations.FallbackURL>(value.ToString());
                        }
                    case "com.pilgrimagesoftware.yawp.client":
                        {
                            return JsonConvert.DeserializeObject<Annotations.YawpClientInfo>(value.ToString());
                        }
                    case "com.pilgrimagesoftware.yawp.topic":
                        {
                            return JsonConvert.DeserializeObject<Annotations.YawpTopic>(value.ToString());
                        }
                    case "net.app.core.channel.invite":
                        {
                            return JsonConvert.DeserializeObject<Annotations.ChannelInvite>(value.ToString());
                        }
                    case "org.xmpp.presence":
                        {
                            return JsonConvert.DeserializeObject<Annotations.Presence>(value.ToString());
                        }
                    case "de.li-ghun.issue":
                        {
                            return JsonConvert.DeserializeObject<Annotations.Issue>(value.ToString());
                        }
                    case "net.app.core.broadcast.message.metadata":
                        {
                            return JsonConvert.DeserializeObject<Annotations.Broadcast_Message_Metadata>(value.ToString());
                        }
                    case "net.app.core.broadcast.metadata":
                        {
                            return JsonConvert.DeserializeObject<Annotations.Broadcast_Channel_Metadata>(value.ToString());
                        }
                }
            }
            catch { }
            return null;
        }
    }
}
