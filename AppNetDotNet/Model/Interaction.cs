using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace AppNetDotNet.Model
{
    public class Interaction
    {
        /// <summary>
        /// What users did. Currently one of follow, reply, repost, or star
        /// </summary>
        public string action
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;
                if (objects != null)
                {
                    parseObjects();
                }
            }
        }
        private string _action { get; set; }

        /// <summary>
        /// The time of the most recent interaction occurred at in ISO 8601 format.
        /// </summary>
        public DateTime event_date { get; set; }

        /// <summary>
        /// users took action on. These objects will be Users if action=follow
        /// </summary>
        public List<Newtonsoft.Json.Linq.JObject> objects
        {
            get
            {
                return _objects;
            }
            set
            {
                _objects = value;
                if (!string.IsNullOrEmpty(action))
                {
                    parseObjects();
                }
            }
        }
        private List<Newtonsoft.Json.Linq.JObject> _objects { get; set; }

        /// <summary>
        /// A list of User objects that took action on objects.
        /// </summary>
        public List<User> users { get; set; }

        // ----------------------------------------------------

        /// <summary>
        /// This list will be filled automatically if action=follow and will contain the objects value parsed as a list of users
        /// This is not a standard field of the API but for your convenience in this library
        /// </summary>
        public List<User> follower { get; set; }

        /// <summary>
        /// This list will be filled automatically if action!=follow and will contain the objects value parsed as a list of posts
        /// This is not a standard field of the API but for your convenience in this library
        /// </summary>
        public List<Post> posts { get; set; }

        private void parseObjects()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Error += delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
            {
                Console.WriteLine("Missed");
                return;
            };
            string jsonString = objects.ToString();
            switch (action)
            {
                case "follow":
                    follower = new List<User>();
                    foreach(Newtonsoft.Json.Linq.JObject jobject in objects) {
                        User user = JsonConvert.DeserializeObject<User>(jobject.ToString(), settings);
                        follower.Add(user);
                    }
                    break;
                case "reply":
                case "repost":
                case "star":
                    posts = new List<Post>();
                    foreach(Newtonsoft.Json.Linq.JObject jobject in objects) {
                        Post post = JsonConvert.DeserializeObject<Post>(jobject.ToString(), settings);
                        posts.Add(post);
                    }
                    break;
                default:
                    //unknown action - have a look at the API docs what the objects field could be filled with
                    break;
            }
            

        }

        public override string ToString()
        {
            string returnValue = "Interaction ";
            if (!string.IsNullOrEmpty(action))
            {
                returnValue += action;
            }
            if (users != null)
            {
                if (users.Count > 0)
                {
                    returnValue += " by @" + users[0].username + " (maybe more users)";
                }
            }
            return returnValue;
        }
    }
}
