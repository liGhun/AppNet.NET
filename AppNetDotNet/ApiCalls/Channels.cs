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
            singleChannelResponse responseObject = new singleChannelResponse();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<Channel, ApiCallResponse>(channel, apiCallResponse);
                }

                string requestUrl = Common.baseUrl += "/stream/0/channels";

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

                apiCallResponse = new ApiCallResponse(response);

                if (apiCallResponse.success)
                {
                    responseObject = JsonConvert.DeserializeObject<singleChannelResponse>(response.Content);
                }
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<Channel, ApiCallResponse>(responseObject.data, apiCallResponse);
        }

        public static Tuple<Channel, ApiCallResponse> get(string access_token, string id)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            Channel channel = new Channel();
            singleChannelResponse responseObject = new singleChannelResponse();
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

                string requestUrl = Common.baseUrl += "/stream/0/channels/" + id;

                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                Helper.Response response = Helper.SendGetRequest(
                        requestUrl,
                        headers);

                apiCallResponse = new ApiCallResponse(response);

                if (apiCallResponse.success)
                {
                    responseObject = JsonConvert.DeserializeObject<singleChannelResponse>(response.Content);
                }
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<Channel, ApiCallResponse>(responseObject.data, apiCallResponse);
        }

        public static Tuple<Channel, ApiCallResponse> update(string access_token, string id, List<Annotation> annotations, ACL readers = null, ACL writers = null)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            Channel channel = new Channel();
            singleChannelResponse responseObject = new singleChannelResponse();
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

                string requestUrl = Common.baseUrl += "/stream/0/channels/" + id;

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

                apiCallResponse = new ApiCallResponse(response);

                if (apiCallResponse.success)
                {
                    responseObject = JsonConvert.DeserializeObject<singleChannelResponse>(response.Content);
                }
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<Channel, ApiCallResponse>(responseObject.data, apiCallResponse);
        }



        public class Subscriptions
        {
            public static Tuple<List<Channel>, ApiCallResponse> getOfCurrentUser(string access_token)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                List<Channel> channels = new List<Channel>();
                multipleChannelsResponse responseObject = new multipleChannelsResponse();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<List<Channel>, ApiCallResponse>(channels, apiCallResponse);
                    }

                    string requestUrl = Common.baseUrl + "/stream/0/channels";

                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                    Helper.Response response = Helper.SendGetRequest(
                            requestUrl,
                            headers);

                    apiCallResponse = new ApiCallResponse(response);

                    if (apiCallResponse.success)
                    {
                        responseObject = JsonConvert.DeserializeObject<multipleChannelsResponse>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<Channel>, ApiCallResponse>(responseObject.data, apiCallResponse);
            }

            public static Tuple<Channel, ApiCallResponse> subscribe(string access_token, string id)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Channel channel = new Channel();
                singleChannelResponse responseObject = new singleChannelResponse();
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

                    string requestUrl = Common.baseUrl += "/stream/0/channels/" + id + "/subscribe";

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

                    apiCallResponse = new ApiCallResponse(response);

                    if (apiCallResponse.success)
                    {
                        responseObject = JsonConvert.DeserializeObject<singleChannelResponse>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Channel, ApiCallResponse>(responseObject.data, apiCallResponse);
            }

            public static Tuple<Channel, ApiCallResponse> unsubscribe(string access_token, string id)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Channel channel = new Channel();
                singleChannelResponse responseObject = new singleChannelResponse();
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

                    string requestUrl = Common.baseUrl += "/stream/0/channels/" + id + "/subscribe";

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

                    apiCallResponse = new ApiCallResponse(response);

                    if (apiCallResponse.success)
                    {
                        responseObject = JsonConvert.DeserializeObject<singleChannelResponse>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Channel, ApiCallResponse>(responseObject.data, apiCallResponse);
            }

            public static Tuple<List<User>, ApiCallResponse> getSubscribers(string access_token, string id)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                List<User> users = new List<User>();
                AppNetDotNet.ApiCalls.Users.multipleUsersResponse responseObject = new Users.multipleUsersResponse();
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

                    string requestUrl = Common.baseUrl += "/stream/0/channels/" + id + "/subscribers";

                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                    Helper.Response response = Helper.SendGetRequest(
                            requestUrl,
                            headers);

                    apiCallResponse = new ApiCallResponse(response);

                    if (apiCallResponse.success)
                    {
                        responseObject = JsonConvert.DeserializeObject<AppNetDotNet.ApiCalls.Users.multipleUsersResponse>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<User>, ApiCallResponse>(responseObject.data, apiCallResponse);
            }

            public static Tuple<List<string>, ApiCallResponse> getSubscribersIds(string access_token, string id)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                List<string> users = new List<string>();
                multipleIdsResponse responseObject = new multipleIdsResponse();
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

                    string requestUrl = Common.baseUrl += "/stream/0/channels/" + id + "/subscribers/ids";

                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                    Helper.Response response = Helper.SendGetRequest(
                            requestUrl,
                            headers);

                    apiCallResponse = new ApiCallResponse(response);

                    if (apiCallResponse.success)
                    {
                        responseObject = JsonConvert.DeserializeObject<multipleIdsResponse>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<string>, ApiCallResponse>(responseObject.data, apiCallResponse);
            }
        }

        public class singleChannelResponse
        {
            public Channel data { get; set; }
            public Meta meta { get; set; }
        }

        public class multipleChannelsResponse
        {
            public List<Channel> data { get; set; }
            public Meta meta { get; set; }
        }

        public class multipleIdsResponse
        {
            public List<string> data { get; set; }
            public Meta meta { get; set; }
        }

    }
}
