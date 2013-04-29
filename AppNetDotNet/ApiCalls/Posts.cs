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
                    Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

                    return Helper.getData<List<Post>>(response);
                }
                catch (Exception exp)
                {
                    apiCallResponse = new ApiCallResponse();
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                } 
                return new Tuple<List<Post>, ApiCallResponse>(new List<Post>(), apiCallResponse);
            }

            public static Tuple <List<Post>,ApiCallResponse> getUnifiedStream(string access_token, ParametersMyStream parameter = null)
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<List<Post>, ApiCallResponse>(new List<Post>(), apiCallResponse);
                    }
                    string requestUrl = Common.baseUrl + "/stream/0/posts/stream/unified";
                    if(parameter != null) {
                        requestUrl = requestUrl + "?" + parameter.getQueryString();
                    }
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

                    return Helper.getData<List<Post>>(response);
                }
                catch (Exception exp)
                {
                    apiCallResponse = new ApiCallResponse();
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                } 
                return new Tuple<List<Post>, ApiCallResponse>(new List<Post>(), apiCallResponse);
            }

            public static Tuple <List<Post>,ApiCallResponse> getGlobalStream(string access_token, Parameters parameter = null)
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
                    string requestUrl = Common.baseUrl + "/stream/0/posts/stream/global";
                     if(parameter != null) {
                        requestUrl = requestUrl + "?" + parameter.getQueryString();
                    }
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    
                    Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

                    return Helper.getData<List<Post>>(response);
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<Post>, ApiCallResponse>(posts, apiCallResponse);
            }
        }

        #endregion

        #region Posts

        public static class Posts
        {
            
            public static Tuple<Post,ApiCallResponse> create(string access_token, string text, string reply_to = null, List<File> toBeEmbeddedFiles = null, List<Annotation> annotations = null, Entities entities = null, int machine_only = 0)
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

                    postCreateParameters postCreateContent = new postCreateParameters();
                    postCreateContent.text = text;
                    postCreateContent.reply_to = reply_to;
                    postCreateContent.machine_only = machine_only;
                    postCreateContent.entities = new EntitiesWithoutAllProperty(entities);
                    //postCreateContent.annotations = annotations;
                    if (toBeEmbeddedFiles != null)
                    {
                        if (postCreateContent.annotations == null)
                        {
                            postCreateContent.annotations = new List<AppNetDotNet.Model.Annotations.AnnotationReplacement_File>();
                        }
                        foreach (File file in toBeEmbeddedFiles)
                        {
                            AppNetDotNet.Model.Annotations.AnnotationReplacement_File fileReplacementAnnotation = new AppNetDotNet.Model.Annotations.AnnotationReplacement_File(file);
                            postCreateContent.annotations.Add(fileReplacementAnnotation);
                        }
                    }

                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;

                    string jsonString = JsonConvert.SerializeObject(postCreateContent, Formatting.None, settings);

                    jsonString = jsonString.Replace("netAppCoreFile_dummy_for_replacement", "+net.app.core.file");

                    Helper.Response response;
                        response = Helper.SendPostRequestStringDataOnly(
                            requestUrl,
                            jsonString,
                            headers,
                            true,
                            contentType: "application/json");

                        return Helper.getData<Post>(response);
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

                    return Helper.getData<Post>(response);
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

                    return Helper.getData<List<Post>>(response);
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<Post>, ApiCallResponse>(posts, apiCallResponse);
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

                    return Helper.getData<List<Post>>(response);
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

                    return Helper.getData<Post>(response);
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

                    return Helper.getData<Post>(response);
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

                    return Helper.getData<Post>(response);
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

                    return Helper.getData<Post>(response);
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

                    return Helper.getData<Post>(response);
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Post, ApiCallResponse>(post, apiCallResponse);
            }

            /// <summary>
            /// Report a post as spam. This will mute the author of the post and send a report to App.net.
            /// </summary>
            /// <param name="access_token">the user access token</param>
            /// <param name="id">the id of the post to be reported</param>
            /// <returns></returns>
            public static Tuple<Post, ApiCallResponse> report(string access_token, string id)
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
                    string requestUrl = Common.baseUrl + string.Format("/stream/0/posts/{0}/report",id);
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    Helper.Response response = Helper.SendPostRequest(
                        requestUrl, 
                        new Dictionary<string,string>(),
                        headers);

                    return Helper.getData<Post>(response);
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

                    return Helper.getData<List<Post>>(response);
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
                    Helper.Response response = Helper.SendGetRequest(requestUrl, headers);
                    return Helper.getData<List<Post>>(response);
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<Post>, ApiCallResponse>(emptyList, apiCallResponse);
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

                    return Helper.getData<StreamMarker>(response);
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

        public class postCreateParameters
        {
            public string text { get; set; }
            public string reply_to { get; set; }
            public int machine_only { get; set; }
            public EntitiesWithoutAllProperty entities { get; set; }
            public List<AppNetDotNet.Model.Annotations.AnnotationReplacement_File> annotations { get; set; }
        }

        public class postCreateEntity
        {

        }
    }

