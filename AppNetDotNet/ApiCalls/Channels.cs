using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Model;
using Newtonsoft.Json;

namespace AppNetDotNet.ApiCalls
{
    public class Channels
    {
        public static Tuple<Channel, ApiCallResponse> createPM(string access_token, string type, List<Annotation> annotations, ACL readers = null, ACL writers = null)
        {
            return create(access_token, "net.app.core.pm", annotations, readers, writers);
        }

        public static Tuple<Channel, ApiCallResponse> create(string access_token, string type, List<Annotation> annotations, ACL readers = null, ACL writers = null)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            Channel channel = new Channel();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<Channel, ApiCallResponse>(channel, apiCallResponse);
                }

                string requestUrl = Common.baseUrl + "/stream/0/channels";

                channel.annotations = annotations;
                channel.readers = readers;
                channel.writers = writers;
                channel.type = type;

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;

                string jsonString = JsonConvert.SerializeObject(channel, Formatting.None, settings);

                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                Helper.Response response = Helper.SendPostRequestStringDataOnly(
                        requestUrl,
                        jsonString,
                        headers,
                        true,
                        contentType: "application/json");

                return Helper.getData<Channel>(response);

            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<Channel, ApiCallResponse>(channel, apiCallResponse);
        }

        public static Tuple<Channel, ApiCallResponse> get(string access_token, string id, channelParameters parameters = null)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            Channel channel = new Channel();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<Channel, ApiCallResponse>(channel, apiCallResponse);
                }
                if (string.IsNullOrEmpty(id))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter id";
                    return new Tuple<Channel, ApiCallResponse>(channel, apiCallResponse);
                }

                string requestUrl = Common.baseUrl + "/stream/0/channels/" + id;
                if (parameters != null)
                {
                    requestUrl += "?" + parameters.getQueryString();
                }

                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                Helper.Response response = Helper.SendGetRequest(
                        requestUrl,
                        headers);

                return Helper.getData<Channel>(response);
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<Channel, ApiCallResponse>(channel, apiCallResponse);
        }

        public static Tuple<Channel, ApiCallResponse> update(string access_token, string id, List<Annotation> annotations, ACL readers = null, ACL writers = null)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            Channel channel = new Channel();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<Channel, ApiCallResponse>(channel, apiCallResponse);
                }
                if (string.IsNullOrEmpty(id))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter id";
                    return new Tuple<Channel, ApiCallResponse>(channel, apiCallResponse);
                }

                string requestUrl = Common.baseUrl + "/stream/0/channels/" + id;

                channel.annotations = annotations;
                channel.readers = readers;
                channel.writers = writers;

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;

                string jsonString = JsonConvert.SerializeObject(channel, Formatting.None, settings);

                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                Helper.Response response = Helper.SendPutRequestStringDataOnly(
                        requestUrl,
                        jsonString,
                        headers,
                        true,
                        contentType: "application/json");

                return Helper.getData<Channel>(response);

            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<Channel, ApiCallResponse>(channel, apiCallResponse);
        }



        public class Subscriptions
        {
            public static Tuple<List<Channel>, ApiCallResponse> getOfCurrentUser(string access_token, channelParameters parameters = null)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                List<Channel> channels = new List<Channel>();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<List<Channel>, ApiCallResponse>(channels, apiCallResponse);
                    }

                    string requestUrl = Common.baseUrl + "/stream/0/channels";
                    if (parameters != null)
                    {
                        requestUrl += "?" + parameters.getQueryString();
                    }

                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                    Helper.Response response = Helper.SendGetRequest(
                            requestUrl,
                            headers);

                    return Helper.getData<List<Channel>>(response);
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<Channel>, ApiCallResponse>(channels, apiCallResponse);
            }

            public static Tuple<Channel, ApiCallResponse> subscribe(string access_token, string id)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Channel channel = new Channel();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<Channel, ApiCallResponse>(channel, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(id))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter id";
                        return new Tuple<Channel, ApiCallResponse>(channel, apiCallResponse);
                    }

                    string requestUrl = Common.baseUrl + "/stream/0/channels/" + id + "/subscribe";

                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                    Helper.Response response = Helper.SendPostRequest(
                            requestUrl,
                            new
                            {
                                channel_id = id
                            },
                            additionalHeaders: headers);

                    return Helper.getData<Channel>(response);
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Channel, ApiCallResponse>(channel, apiCallResponse);
            }

            public static Tuple<Channel, ApiCallResponse> unsubscribe(string access_token, string id)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Channel channel = new Channel();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<Channel, ApiCallResponse>(channel, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(id))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter id";
                        return new Tuple<Channel, ApiCallResponse>(channel, apiCallResponse);
                    }

                    string requestUrl = Common.baseUrl + "/stream/0/channels/" + id + "/subscribe";

                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                    Helper.Response response = Helper.SendDeleteRequest(
                            requestUrl,
                            headers,
                            data: new
                            {
                                channel_id = id
                            });

                    return Helper.getData<Channel>(response);
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Channel, ApiCallResponse>(channel, apiCallResponse);
            }

            public static Tuple<List<User>, ApiCallResponse> getSubscribers(string access_token, string id)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                List<User> users = new List<User>();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<List<User>, ApiCallResponse>(users, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter id";
                        return new Tuple<List<User>, ApiCallResponse>(users, apiCallResponse);
                    }

                    string requestUrl = Common.baseUrl + "/stream/0/channels/" + id + "/subscribers";

                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                    Helper.Response response = Helper.SendGetRequest(
                            requestUrl,
                            headers);

                    return Helper.getData<List<User>>(response);

                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<User>, ApiCallResponse>(users, apiCallResponse);
            }

            public static Tuple<List<string>, ApiCallResponse> getSubscribersIds(string access_token, string id)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                List<string> users = new List<string>();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<List<string>, ApiCallResponse>(users, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter id";
                        return new Tuple<List<string>, ApiCallResponse>(users, apiCallResponse);
                    }

                    string requestUrl = Common.baseUrl + "/stream/0/channels/" + id + "/subscribers/ids";

                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                    Helper.Response response = Helper.SendGetRequest(
                            requestUrl,
                            headers);

                    return Helper.getData<List<string>>(response);

                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<string>, ApiCallResponse>(users, apiCallResponse);
            }
        }

        
        public class channelParameters
        {
            /// <summary>
            /// A comma separated list of the Channel types to include. For instance channel_types=net.app.core.pm,net.myapp.room will only return channels with a type of net.app.core.pm or net.myapp.room.
            /// </summary>
            public string channel_types { get; set; }
            /// <summary>
            /// Should the Stream Marker be included with each Channel? Only available when requested with a user access token. Defaults to false.
            /// </summary>
            public bool include_marker { get; set; }
            /// <summary>
            /// Should the most recent Message be included with each Channel? Defaults to false.
            /// </summary>
            public bool include_recent_message { get; set; }
            /// <summary>
            /// Should annotations be included in the response objects? Defaults to false.
            /// </summary>
            public bool include_annotations { get; set; }
            /// <summary>
            /// Should User annotations be included in the response objects? Defaults to false.
            /// </summary>
            public bool include_user_annotations { get; set; }
            /// <summary>
            /// Should Message annotations be included in the response objects? Defaults to false.
            /// </summary>
            public bool include_message_annotations { get; set; }

            public string getQueryString()
            {
                string queryString = "";
                if (!string.IsNullOrEmpty(channel_types))
                {
                    queryString += "channel_types=" + channel_types + "&";
                }
                if (include_marker)
                {
                    queryString += "include_marker=1&";
                }
                if (include_recent_message)
                {
                    queryString += "include_recent_message=1&";
                }
                if (include_annotations)
                {
                    queryString += "include_annotations=1&";
                }
                if (include_user_annotations)
                {
                    queryString += "include_user_annotations=1&";
                }
                if (include_message_annotations)
                {
                    queryString += "include_message_annotations=1&";
                }
                queryString = queryString.TrimEnd('&');
                return queryString;
            }
        }
    }
}
