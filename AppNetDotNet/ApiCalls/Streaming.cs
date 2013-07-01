using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Model;
using System.Web;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppNetDotNet.ApiCalls
{
    public class Streaming
    {
        /// <summary>
        /// The different stop reasons for stopping a stream.
        /// </summary>
        public enum StopReasons
        {
            StoppedByRequest,
            WebConnectionFailed,
            Unknown,
            Unauthorised,
            Forbidden,
            NotFound,
            NotAcceptable,
            
        }


        public delegate void FollowingCallback(List<User> users, bool is_deleted = false);
        public delegate void FollowersCallback(List<User> users, bool is_deleted = false);
        public delegate void PostsCallback(List<Post> posts, bool is_deleted = false);
        public delegate void MentionsCallback(List<Post> posts, bool is_deleted = false);
        public delegate void StreamCallback(List<Post> posts, bool is_deleted = false);
        public delegate void UnifiedCallback(List<Post> posts, bool is_deleted = false);
        public delegate void ChannelsCallback(List<Message> messages, bool is_deleted = false);
        public delegate void ChannelSubscribersCallback(List<User> users, bool is_deleted = false);
        public delegate void ChannelMessagesCallback(List<Message> messages, bool is_deleted = false);
        public delegate void FilesCallback(List<AppNetDotNet.Model.File> files, bool is_deleted = false);

        public delegate void RawJsonCallback(string json);

        public delegate void StreamStoppedCallback(StopReasons stopreason);
        

        public class UserStream
        {
            private FollowingCallback followingCallback;
            private FollowersCallback followersCallback;
            private PostsCallback postsCallback;
            private MentionsCallback mentionsCallback;
            private StreamCallback streamCallback;
            private UnifiedCallback unifiedCallback;
            private ChannelsCallback channelsCallback;
            private ChannelSubscribersCallback channelSubscribersCallback;
            private ChannelMessagesCallback channelMessagesCallback;
            private FilesCallback filesCallback;

            private RawJsonCallback rawJsonCallback;

            private StreamStoppedCallback streamStoppedCallback;

            
            public string connection_id  { get; set; }
            public bool stop_received {get;set;}

            public Dictionary<string, StreamingEndpoint> subscribed_endpoints { get; set; }
            public List<StreamingEndpoint> to_be_subscribed_endpoints { get; set; }

            StreamingOptions streaming_options{get;set;}

            private HttpWebRequest request;
            public string access_token { get; set; }

            public UserStream(string access_token, StreamingOptions options = null)
            {
                this.access_token = access_token;
                streaming_options = options;
            }

            public IAsyncResult StartUserStream(
                                 FollowingCallback followingCallback = null,
                                 FollowersCallback followersCallback = null,
                                 PostsCallback postsCallback = null,
                                 MentionsCallback mentionsCallback = null,
                                 StreamCallback streamCallback = null,
                                 UnifiedCallback unifiedCallback = null,
                                 ChannelsCallback channelsCallback = null,
                                 ChannelSubscribersCallback channelSubscribersCallback = null,
                                 ChannelMessagesCallback channelMessagesCallback = null,
                                 FilesCallback filesCallback = null,
                                 RawJsonCallback rawJsonCallback = null)
            {
                if (this.request != null)
                {
                    throw new InvalidOperationException("Stream is already open");
                }

                this.followingCallback = followingCallback;
                this.followersCallback = followersCallback;
                this.postsCallback = postsCallback;
                this.mentionsCallback = mentionsCallback;
                this.streamCallback = streamCallback;
                this.unifiedCallback = unifiedCallback;
                this.channelsCallback = channelsCallback;
                this.channelSubscribersCallback = channelSubscribersCallback;
                this.channelMessagesCallback = channelMessagesCallback;
                this.filesCallback = filesCallback;
                this.rawJsonCallback = rawJsonCallback;

                subscribed_endpoints = new Dictionary<string, StreamingEndpoint>();
                to_be_subscribed_endpoints = new List<StreamingEndpoint>();

                Dictionary<string, string> headers = new Dictionary<string,string>();
                headers.Add("Authorization", "Bearer " + access_token);


                string url = "https://stream-channel.app.net/stream/user?ct=AppNet.Net";

                if (streaming_options != null)
                {
                    if (!string.IsNullOrEmpty(streaming_options.query_string()))
                    {
                        url += "&" + streaming_options.query_string();
                    }
                }

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                request.AllowAutoRedirect = true;
                request.Accept = "*/*";
                request.Timeout = 10000;
                request.KeepAlive = true;

                    request.UserAgent = "AppNet.Net (http://www.nymphicusapp.com/windows/chapper)";

                    foreach (KeyValuePair<string, string> additonalHeader in headers)
                    {
                        request.Headers.Add(additonalHeader.Key, additonalHeader.Value);
                    }

                try
                {
                    return request.BeginGetResponse(StreamCallback, request);
                               
                }
                catch (WebException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

                
            }

            public bool is_active
            {
                get
                {
                    return request != null;
                }
            }

            public void stopUserStream()
            {
                stop_received = true;
            }

            public void subscribe_to_endpoint(StreamingEndpoint endpoint)
            {
                if (endpoint != null)
                {
                    if (endpoint.needs_channel_id && string.IsNullOrEmpty(endpoint.channel_id))
                    {
                        throw new Exception("Channel endpoint is missing in subsciption");
                    }
                    if (string.IsNullOrEmpty(connection_id))
                    {
                        to_be_subscribed_endpoints.Add(endpoint);
                        return;
                    }

                    string request_url = string.Format("https://alpha-api.app.net{0}?connection_id={1}&ct={2}", endpoint.endpoint, System.Web.HttpUtility.UrlEncode(connection_id), "AppNet.net");
                    if (endpoint.options != null)
                    {
                        if (!string.IsNullOrEmpty(endpoint.options.query_string()))
                        {
                            request_url += "&" + endpoint.options.query_string();
                        }
                    }

                    Dictionary<string, string> header = new Dictionary<string,string>();
                    header.Add("Authorization", "Bearer " + access_token);

                    Helper.Response subscription_response = Helper.SendGetRequest(request_url,header);

                    if(subscription_response.Success) {
                        Tuple<string, ApiCallResponse> apiCallResponse = Helper.getData<string>(subscription_response);
                        if(apiCallResponse != null) {
                            if (apiCallResponse.Item2 != null)
                            {
                                if(apiCallResponse.Item2.meta != null) {
                                    string subscription_id = apiCallResponse.Item2.meta.subscription_id;
                                    endpoint.subscription_id = subscription_id;
                                    if (!string.IsNullOrEmpty(subscription_id))
                                    {
                                        subscribed_endpoints.Add(subscription_id, endpoint);
                                    }
                                }
                            }

                        }
                    }

                }
            }

            private void set_streaming_endpoints() {
                Dictionary<string, StreamingEndpoint> endpoints = new Dictionary<string, StreamingEndpoint>();

                StreamingEndpoint following = new StreamingEndpoint();
                following.title = "Following";
                following.endpoint = "/stream/0/users/me/following";
                following.return_type = typeof(List<User>);
                endpoints.Add("Following", following);                

                StreamingEndpoint followers = new StreamingEndpoint();
                followers.title = "Followers";
                followers.endpoint = "/stream/0/users/me/followers";
                followers.return_type = typeof(List<User>);
                endpoints.Add("Followers", followers);

                StreamingEndpoint posts = new StreamingEndpoint();
                posts.title = "Posts";
                posts.endpoint = "/stream/0/users/me/posts";
                posts.return_type = typeof(List<Post>);
                endpoints.Add("Posts", posts);

                StreamingEndpoint mentions = new StreamingEndpoint();
                mentions.title = "Mentions";
                mentions.endpoint = "/stream/0/users/me/mentions";
                mentions.return_type = typeof(List<Post>);
                endpoints.Add("Mentions", mentions);

                StreamingEndpoint stream = new StreamingEndpoint();
                stream.title = "Stream";
                stream.endpoint = "/stream/0/posts/stream";
                stream.return_type = typeof(List<Post>);
                endpoints.Add("Stream", stream);

                StreamingEndpoint unified = new StreamingEndpoint();
                unified.title = "Unified";
                unified.endpoint = "/stream/0/posts/stream/unified";
                unified.return_type = typeof(List<Post>);
                endpoints.Add("Unified", unified);

                StreamingEndpoint channels = new StreamingEndpoint();
                channels.title = "Channels";
                channels.endpoint = "/stream/0/channels";
                channels.return_type = typeof(List<Message>);
                endpoints.Add("Channels", channels);

                StreamingEndpoint channel_subscribers = new StreamingEndpoint();
                channel_subscribers.title = "Channel subscribers";
                channel_subscribers.endpoint = "/stream/0/channels/{0}/subscribers";
                channel_subscribers.return_type = typeof(List<User>);
                channel_subscribers.needs_channel_id = true;
                endpoints.Add("Channel subscribers", channel_subscribers);

                StreamingEndpoint channel_messages = new StreamingEndpoint();
                channel_messages.title = "Channel messages";
                channel_messages.endpoint = "/stream/0/channels/{0}/messages";
                channel_messages.return_type = typeof(List<Message>);
                channel_messages.needs_channel_id = true;
                endpoints.Add("Channel messages", channel_messages);

                StreamingEndpoint files = new StreamingEndpoint();
                files.title = "Files";
                files.endpoint = "/stream/0/users/me/files";
                files.return_type = typeof(List<AppNetDotNet.Model.File>);
                endpoints.Add("Files", files);

                _available_endpoints = endpoints;
                
            }

            public Dictionary<string, StreamingEndpoint> available_endpoints
            {
                get
                {
                    if (_available_endpoints == null)
                    {
                        set_streaming_endpoints();
                    }
                    return _available_endpoints;
                }
            }
            public static Dictionary<string, StreamingEndpoint> _available_endpoints { get; set; }


            private void StreamCallback(IAsyncResult result)
            {
                HttpWebRequest req = (HttpWebRequest)result.AsyncState;
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)req.EndGetResponse(result);

                    connection_id = response.Headers["Connection-Id"];

                    if (to_be_subscribed_endpoints != null)
                    {
                        foreach (StreamingEndpoint endpoint in to_be_subscribed_endpoints)
                        {
                            subscribe_to_endpoint(endpoint);
                        }
                        to_be_subscribed_endpoints.Clear();
                    }

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            try
                            {
                                while (!stop_received && !reader.EndOfStream)
                                {
                                    string lineOfData = reader.ReadLine();
                                    if (string.IsNullOrEmpty(lineOfData))
                                    {
                                        continue;
                                    }
                                    Console.WriteLine(lineOfData);
                                    if (rawJsonCallback != null && !string.IsNullOrEmpty(lineOfData))
                                    {
                                        rawJsonCallback(lineOfData);
                                    }

                                    ThreadPool.QueueUserWorkItem(delegate { ParseMessage(lineOfData); });


                                }


                                reader.Close();
                                OnStreamStopped(stop_received ? StopReasons.StoppedByRequest : StopReasons.WebConnectionFailed);
                            }
                            catch
                            {
                                reader.Close();
                                OnStreamStopped(stop_received ? StopReasons.StoppedByRequest : StopReasons.WebConnectionFailed);
                            }
                        }
                    }
                }
                catch (WebException we)
                {
                    HttpWebResponse httpResponse = we.Response as HttpWebResponse;
                    if (httpResponse != null)
                    {
                        switch ((httpResponse).StatusCode)
                        {
                            case HttpStatusCode.Unauthorized:
                                {
                                    OnStreamStopped(StopReasons.Unauthorised);
                                    break;
                                }
                            case HttpStatusCode.Forbidden:
                                {
                                    OnStreamStopped(StopReasons.Forbidden);
                                    break;
                                }
                            case HttpStatusCode.NotFound:
                                {
                                    OnStreamStopped(StopReasons.NotFound);
                                    break;
                                }
                            case HttpStatusCode.NotAcceptable:
                                {
                                    OnStreamStopped(StopReasons.NotAcceptable);
                                    break;
                                }
             
                            default:
                                {
                                    OnStreamStopped(StopReasons.Unknown);
                                    break;
                                }
                        }
                    }
                    else
                    {
                        OnStreamStopped(StopReasons.Unknown);
                    }
                }
                catch (Exception)
                {
                    OnStreamStopped(StopReasons.WebConnectionFailed);
                }
                finally
                {
                    req.Abort();
                    if (response != null)
                        response.Close();
                    request = null;
                }
            }

            private void OnStreamStopped(StopReasons reason)
            {
                if (streamStoppedCallback != null)
                    streamStoppedCallback(reason);
            }

            private void ParseMessage(string json)
            {
                Helper.Response api_response = new Helper.Response();
                api_response.Success = true;
                api_response.Content = json;

                ApiCallResponse apiCallResponse = new ApiCallResponse();
                apiCallResponse.success = true;
                apiCallResponse.content = json;
                
                Newtonsoft.Json.Linq.JObject responseJson = Newtonsoft.Json.Linq.JObject.Parse(json);
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Error += delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                {
                    throw args.ErrorContext.Error;
                };
                if (responseJson != null)
                {
                    try
                    {
                        apiCallResponse.meta = JsonConvert.DeserializeObject<Model.Meta>(responseJson["meta"].ToString(), settings);
                    }
                    catch (Exception exp) {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = exp.Message;
                        apiCallResponse.errorDescription = exp.StackTrace;
                        return;
                    }
                }

                if (apiCallResponse.meta != null)
                {
                    if (apiCallResponse.meta.subscription_ids != null)
                    {
                        foreach (string subscription_id in apiCallResponse.meta.subscription_ids)
                        {
                            if (subscribed_endpoints.ContainsKey(subscription_id))
                            {
                                StreamingEndpoint endpoint = subscribed_endpoints[subscription_id];
                                if (endpoint != null)
                                {
                                    switch (endpoint.title)
                                    {

                                        case "Following":
                                            List<User> following = JsonConvert.DeserializeObject<List<User>>(responseJson["data"].ToString(), settings);
                                            if (following != null && followingCallback != null)
                                            {
                                                followingCallback(following, is_deleted: apiCallResponse.meta.is_deleted);
                                            }
                                            break;

                                        case "Followers":
                                            List<User> followers = JsonConvert.DeserializeObject<List<User>>(responseJson["data"].ToString(), settings);
                                            if (followers != null && followersCallback != null)
                                            {
                                                followersCallback(followers, is_deleted: apiCallResponse.meta.is_deleted);
                                            }
                                            break;

                                        case "Unified":
                                            List<Post> unified_posts = JsonConvert.DeserializeObject<List<Post>>(responseJson["data"].ToString(), settings);
                                            if (unified_posts != null && unifiedCallback != null)
                                            {
                                                unifiedCallback(unified_posts, is_deleted: apiCallResponse.meta.is_deleted);
                                            }
                                            break;

                                        case "Channels":
                                            List<Message> channels = JsonConvert.DeserializeObject<List<Message>>(responseJson["data"].ToString(), settings);
                                            if (channels != null && channelsCallback != null)
                                            {
                                                channelsCallback(channels, is_deleted: apiCallResponse.meta.is_deleted);
                                            }
                                            break;

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    public class StreamingOptions
    {
        public bool include_annotations { get; set; }
        public bool include_message_annotations { get; set; }
        public bool include_channel_annotations { get; set; }
        public bool include_user_annotations { get; set; }
        public bool include_post_annotations { get; set; }
        public bool include_file_annotations { get; set; }
        public bool include_starred_by { get; set; }
        public bool include_reposters { get; set; }
        public bool include_marker { get; set; }
        public bool include_recent_message { get; set; }
        public bool include_html { get; set; }

        public string query_string()
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            if (include_annotations)
            {
                parameter.Add("include_annotations", "1");
            }
            if (include_message_annotations)
            {
                parameter.Add("include_message_annotations", "1");
            }
            if (include_channel_annotations)
            {
                parameter.Add("include_channel_annotations", "1");
            }
            if (include_user_annotations)
            {
                parameter.Add("include_user_annotations", "1");
            }
            if (include_post_annotations)
            {
                parameter.Add("include_post_annotations", "1");
            }
            if (include_file_annotations)
            {
                parameter.Add("include_file_annotations", "1");
            }
            if (include_starred_by)
            {
                parameter.Add("include_starred_by", "1");
            }
            if (include_reposters)
            {
                parameter.Add("include_reposters", "1");
            }
            if (include_marker)
            {
                parameter.Add("include_marker", "1");
            }
            if (include_recent_message)
            {
                parameter.Add("include_recent_message", "1");
            }
            if (include_html)
            {
                parameter.Add("include_html", "1");
            }

            return HelpMethods.GetParameter.get(parameter);
        }
    }

    public class SubscriptionOptions
    {
        public bool include_incomplete { get; set; }
        public bool include_private { get; set; }
        public List<string> file_types { get; set; }
        public List<string> channel_types { get; set; }
        public bool include_read { get; set; }
        public bool include_muted { get; set; }
        public bool include_deleted { get; set; }
        public bool include_machine { get; set; }
        public bool include_directed_posts { get; set; }


        public string query_string()
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            if (include_incomplete)
            {
                parameter.Add("include_incomplete", "1");
            }
            if (include_private)
            {
                parameter.Add("include_private", "1");
            }
            if (file_types != null)
            {
                parameter.Add("file_types", string.Join(",", file_types));
            }
            if (channel_types != null)
            {
                parameter.Add("channel_types", string.Join(",", channel_types));
            }
            if (include_read)
            {
                parameter.Add("include_read", "1");
            }
            if (include_muted)
            {
                parameter.Add("include_muted", "1");
            }
            if (include_deleted)
            {
                parameter.Add("include_deleted", "1");
            }
            if (include_machine)
            {
                parameter.Add("include_machine", "1");
            }
            if (include_directed_posts)
            {
                parameter.Add("include_directed_posts", "1");
            }

            return HelpMethods.GetParameter.get(parameter);
        }
    }

    public class StreamingEndpoint
    {
        public string title { get; set; }
        /// <summary>
        /// the Url without the base url (e. g. "/stream/0/users/me/following")
        /// </summary>
        public string endpoint { get; set; }
        public SubscriptionOptions options { get; set; }
        public Type  return_type { get; set; }
        public string subscription_id { get; set; }
        public bool needs_channel_id { get; set; }
        public string channel_id { get; set; }
    }

}
