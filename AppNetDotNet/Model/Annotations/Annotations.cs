using System;
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
        public object parsedObject { get; set; }

        public string type { 
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                if(_type != null && this.value != null) {
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
                if (_value != null && type != null)
                {
                    parsedObject = getParsedObject();
                }
            }
        }
        private object _value { get; set; }

        private object getParsedObject()
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
                        return JsonConvert.DeserializeObject<Annotations.BlogUrl>(value.ToString());
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
            }
            return null;
        }
    }
}
