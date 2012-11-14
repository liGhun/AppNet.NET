using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppNetDotNet.Model;
using Newtonsoft.Json;
using AppNetDotNet.ApiCalls;

namespace AppNetDotNet.ApiCalls
{
    
        #region Streams

        public static class SimpleStreams
        {
            
            public static Tuple <List<Post>,ApiCallResponse> getUserStream(string access_token, ParametersMyStream parameter = null)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                StreamResponse streamResponse = new StreamResponse();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<List<Post>, ApiCallResponse>(new List<Post>(), apiCallResponse);
                    }
                    string requestUrl = Common.baseUrl + "/stream/0/posts/stream";
                    if(parameter != null) {
                        requestUrl = requestUrl + "?" + parameter.getQueryString();
                    }
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                    Helper.Response response = Helper.SendGetRequest(requestUrl, headers);
                    apiCallResponse = new ApiCallResponse(response);
                    if (response.Success)
                    {
                        streamResponse = JsonConvert.DeserializeObject<StreamResponse>(response.Content);
                        apiCallResponse.meta = streamResponse.meta;
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse = new ApiCallResponse();
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                } 
                return new Tuple<List<Post>, ApiCallResponse>(streamResponse.data, apiCallResponse);
            }

            public static Tuple <List<Post>,ApiCallResponse> getGlobalStream(string access_token, Parameters parameter = null)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                List<Post> posts = new List<Post>();
                StreamResponse streamResponse = new StreamResponse();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<List<Post>, ApiCallResponse>(posts, apiCallResponse);
                    }
                    string requestUrl = Common.baseUrl + "/stream/0/posts/stream/global";
                     if(parameter != null) {
                        requestUrl = requestUrl + "?" + parameter.getQueryString();
                    }
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                    Helper.Response response = Helper.SendGetRequest(requestUrl, headers);
                    apiCallResponse = new ApiCallResponse(response);
                    if (apiCallResponse.success)
                    {
                        streamResponse = JsonConvert.DeserializeObject<StreamResponse>(response.Content);
                        apiCallResponse.meta = streamResponse.meta;
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<Post>, ApiCallResponse>(streamResponse.data, apiCallResponse);
            }
        }

        #endregion

        #region Posts

        public static class Posts
        {
            
            public static Tuple<Post,ApiCallResponse> create(string access_token, string text, string reply_to = null)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Post post = new Post();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(text))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter text";
                        return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
                    }
                    string requestUrl = Common.baseUrl + "/stream/0/posts";
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    Helper.Response response;
                    if (string.IsNullOrEmpty(reply_to))
                    {
                        response = Helper.SendPostRequest(
                            requestUrl,
                            new
                            {
                                text = text,
                            },
                            additionalHeaders: headers);
                    }
                    else
                    {
                        response = Helper.SendPostRequest(
                            requestUrl,
                            new
                            {
                                text = text,
                                reply_to = reply_to
                            },
                            additionalHeaders: headers);
                    }
                    apiCallResponse = new ApiCallResponse(response);
                    if (apiCallResponse.success)
                    {
                        post = JsonConvert.DeserializeObject<Post>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
            }

            public static Tuple<Post, ApiCallResponse> getById(string access_token, string id)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Post post = new Post();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(id))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter id";
                        return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
                    }
                    string requestUrl = Common.baseUrl + "/stream/0/posts/" + id;
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    Helper.Response response = Helper.SendGetRequest(requestUrl, headers);
                    apiCallResponse = new ApiCallResponse(response);
                    if (apiCallResponse.success)
                    {
                        post = JsonConvert.DeserializeObject<Post>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
            }

            public static Tuple<List<Post>, ApiCallResponse> getByUsernameOrId(string access_token, string usernameOrId, Parameters parameter = null)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                List <Post> posts = new List<Post>();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<List<Post>, ApiCallResponse>(posts, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(usernameOrId))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter usernameOrId";
                        return new Tuple<List<Post>, ApiCallResponse>(posts, apiCallResponse);
                    }
                    string requestUrl = Common.baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(usernameOrId) + "/posts";
                     if(parameter != null) {
                        requestUrl = requestUrl + "?" + parameter.getQueryString();
                    }
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    Helper.Response response = Helper.SendGetRequest(requestUrl, headers);
                    apiCallResponse = new ApiCallResponse(response);
                    if (apiCallResponse.success)
                    {
                        posts = JsonConvert.DeserializeObject<List<Post>>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<Post>, ApiCallResponse>(posts, apiCallResponse); ;
            }

            public static Tuple<List<Post>, ApiCallResponse> getRepliesById(string access_token, string id, Parameters parameter = null)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                List <Post> posts = new List<Post>();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<List<Post>, ApiCallResponse>(posts, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(id))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter id";
                        return new Tuple<List<Post>, ApiCallResponse>(posts, apiCallResponse);
                    }
                    string requestUrl = Common.baseUrl + "/stream/0/posts/" + id + "/replies";
                     if(parameter != null) {
                        requestUrl = requestUrl + "?" + parameter.getQueryString();
                    }
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    Helper.Response response = Helper.SendGetRequest(requestUrl, headers);
                    apiCallResponse = new ApiCallResponse(response);
                    if (apiCallResponse.success)
                    {
                        posts = JsonConvert.DeserializeObject<List<Post>>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<Post>, ApiCallResponse>(posts, apiCallResponse);
            }

            #region Reposts

            public static Tuple<Post, ApiCallResponse> repost(string access_token, string id)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Post post = new Post();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(id))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter id";
                        return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
                    }
                    string requestUrl = Common.baseUrl + "/stream/0/posts/" + id + "/repost";
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    Helper.Response response = Helper.SendPostRequest(
                            requestUrl,
                            new
                            {
                                post_id = id,
                            },
                            additionalHeaders: headers);


                    apiCallResponse = new ApiCallResponse(response);
                    if (apiCallResponse.success)
                    {
                        post = JsonConvert.DeserializeObject<Post>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
            }

            public static Tuple<Post, ApiCallResponse> unrepost(string access_token, string id)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Post post = new Post();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(id))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter id";
                        return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
                    }
                    string requestUrl = Common.baseUrl + "/stream/0/posts/" + id + "/repost";
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    Helper.Response response = Helper.SendDeleteRequest(
                            requestUrl,
                            additionalHeaders: headers);
                    apiCallResponse = new ApiCallResponse(response);
                    if (apiCallResponse.success)
                    {
                        post = JsonConvert.DeserializeObject<Post>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
            }

            #endregion

            #region Stars

            public static Tuple<Post, ApiCallResponse> star(string access_token, string id)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Post post = new Post();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(id))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter id";
                        return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
                    }
                    string requestUrl = Common.baseUrl + "/stream/0/posts/" + id + "/star";
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    Helper.Response response = Helper.SendPostRequest(
                            requestUrl,
                            new
                            {
                                post_id = id,
                            },
                            additionalHeaders: headers);
                    apiCallResponse = new ApiCallResponse(response);
                    if (apiCallResponse.success)
                    {
                        post = JsonConvert.DeserializeObject<Post>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
            }

            public static Tuple<Post, ApiCallResponse> unstar(string access_token, string id)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Post post = new Post();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(id))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter id";
                        return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
                    }
                    string requestUrl = Common.baseUrl + "/stream/0/posts/" + id + "/star";
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    Helper.Response response = Helper.SendDeleteRequest(
                            requestUrl,
                            additionalHeaders: headers);

                    apiCallResponse = new ApiCallResponse(response);
                    if (apiCallResponse.success)
                    {
                        post = JsonConvert.DeserializeObject<Post>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
            }

            #endregion

            public static Tuple<Post, ApiCallResponse> delete(string access_token, string id)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Post post = new Post();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(id))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter id";
                        return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
                    }
                    string requestUrl = Common.baseUrl + "/stream/0/posts/" + id;
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    Helper.Response response = Helper.SendDeleteRequest(requestUrl, headers);
                    if (apiCallResponse.success)
                    {
                        post = JsonConvert.DeserializeObject<Post>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
            }

            public static Tuple<List<Post>,ApiCallResponse> getStaredByUserId(string access_token, string usernameOrId, Parameters parameter = null)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                List<Post> posts = new List<Post>();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<List<Post>, ApiCallResponse>(posts, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(usernameOrId))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter username or id";
                        return new Tuple<List<Post>, ApiCallResponse>(posts, apiCallResponse);
                    }
                    string requestUrl = Common.baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(usernameOrId) + "/stars";
                     if(parameter != null) {
                        requestUrl = requestUrl + "?" + parameter.getQueryString();
                    }
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    Helper.Response response = Helper.SendGetRequest(requestUrl, headers);
                    apiCallResponse = new ApiCallResponse(response);
                    if (apiCallResponse.success)
                    {
                        posts = JsonConvert.DeserializeObject<List<Post>>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<Post>, ApiCallResponse>(posts, apiCallResponse);
            }


            public static Tuple<List<Post>, ApiCallResponse> getMentionsOfUsernameOrId(string access_token, string usernameOrId, Parameters parameter = null)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                List<Post> emptyList = new List<Post>();
                StreamResponse streamResponse = new StreamResponse();
                streamResponse.data = new List<Post>();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<List<Post>, ApiCallResponse>(emptyList, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(usernameOrId))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter username or id";
                        return new Tuple<List<Post>, ApiCallResponse>(emptyList, apiCallResponse);
                    }
                    string requestUrl = Common.baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(usernameOrId) + "/mentions";
                     if(parameter != null) {
                        requestUrl = requestUrl + "?" + parameter.getQueryString();
                    }
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                    Helper.Response response = Helper.SendGetRequest(requestUrl, headers);
                    apiCallResponse = new ApiCallResponse(response);
                    if (apiCallResponse.success)
                    {
                        streamResponse = JsonConvert.DeserializeObject<StreamResponse>(response.Content);
                        apiCallResponse.meta = streamResponse.meta;
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<Post>, ApiCallResponse>(streamResponse.data, apiCallResponse);
            }


        }
        #endregion

        #region Stream Markers

        public static class StreamMarkers
        {

            public static Tuple<StreamMarker, ApiCallResponse> set(string access_token, string streamName, string id, int percentage)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                StreamMarker receivedStreamMarker = new StreamMarker();
                StreamMarker toBeStoredStreamMarker = new StreamMarker();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<StreamMarker, ApiCallResponse>(receivedStreamMarker, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(id))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter id";
                        return new Tuple<StreamMarker, ApiCallResponse>(receivedStreamMarker, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(streamName))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter streamName";
                        return new Tuple<StreamMarker, ApiCallResponse>(receivedStreamMarker, apiCallResponse);
                    }

                    toBeStoredStreamMarker.name = streamName;
                    toBeStoredStreamMarker.id = id;
                    toBeStoredStreamMarker.percentage = percentage;

                    string jsonString = JsonConvert.SerializeObject(toBeStoredStreamMarker);

                    string requestUrl = Common.baseUrl + "/stream/0/posts/marker";
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    Helper.Response response =  Helper.SendPostRequestStringDataOnly(
                            requestUrl,
                            jsonString,
                            headers,
                            true,
                            contentType:"application/json");
                    apiCallResponse = new ApiCallResponse(response);
                    if (apiCallResponse.success)
                    {
                        receivedStreamMarker = JsonConvert.DeserializeObject<StreamMarker>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<StreamMarker, ApiCallResponse>(receivedStreamMarker, apiCallResponse);
            }
        }
        #endregion


        public class StreamResponse
        {
            public List<Post> data { get; set; }
            public Meta meta { get; set; }
        }
    }

