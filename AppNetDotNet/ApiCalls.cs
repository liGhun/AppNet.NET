using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppNetDotNet.Model;
using Newtonsoft.Json;

namespace AppNetDotNet
{
    public class ApiCalls
    {
        private static string baseUrl = "https://alpha-api.app.net";

        #region Streams

        public static class Streams
        {

            public static List<Post> getUserStream(string access_token, Parameters parameter = null)
            {
                string requestUrl = baseUrl + "/stream/0/posts/stream";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

                List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(response.Content);

                return posts;
            }

            public static List<Post> getGlobalStream(string access_token, Parameters parameter = null)
            {
                string requestUrl = baseUrl + "/stream/0/posts/stream/global";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

                List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(response.Content);

                return posts;
            }
        }

        #endregion

        #region Posts

        public static class Posts
        {

            public static Post write(string access_token, string text, string reply_to = null, Parameters parameter = null)
            {
                string requestUrl = baseUrl + "/stream/0/posts";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response;
                if (reply_to == null)
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

                Post post = JsonConvert.DeserializeObject<Post>(response.Content);

                return post;
            }

            public static Post getById(string access_token, string id, Parameters parameter = null)
            {
                string requestUrl = baseUrl + "/stream/0/posts/" + id;
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

                Post post = JsonConvert.DeserializeObject<Post>(response.Content);

                return post;
            }

            public static List<Post> getByUsername(string access_token, string username, Parameters parameter = null)
            {
                if (string.IsNullOrEmpty(username))
                {
                    return null;
                }
                if (!username.StartsWith("@"))
                {
                    username = "@" + username;
                }
                return getByUserId(access_token, username, parameter);
            }
            public static List<Post> getByUserId(string access_token, string userId, Parameters parameter = null)
            {
                string requestUrl = baseUrl + "/stream/0/users/" + userId + "/posts";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

                List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(response.Content);

                return posts;
            }

            public static List<Post> getRepliesById(string access_token, string id, Parameters parameter = null)
            {
                string requestUrl = baseUrl + "/stream/0/posts/" + id + "/replies";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

                List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(response.Content);

                return posts;
            }

            #region Reposts

            public static Post repost(string access_token, string id, Parameters parameter = null)
            {
                string requestUrl = baseUrl + "/stream/0/posts/" + id + "/repost";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendPostRequest(
                        requestUrl,
                        new
                        {
                            post_id = id,
                        },
                        additionalHeaders: headers);


                Post post = JsonConvert.DeserializeObject<Post>(response.Content);

                return post;
            }

            public static Post unrepost(string access_token, string id, Parameters parameter = null)
            {
                string requestUrl = baseUrl + "/stream/0/posts/" + id + "/repost";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendDeleteRequest(
                        requestUrl,
                        additionalHeaders: headers);


                Post post = JsonConvert.DeserializeObject<Post>(response.Content);

                return post;
            }

            #endregion

            #region Stars

            public static Post star(string access_token, string id, Parameters parameter = null)
            {
                string requestUrl = baseUrl + "/stream/0/posts/" + id + "/star";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendPostRequest(
                        requestUrl,
                        new
                        {
                            post_id = id,
                        },
                        additionalHeaders: headers);


                Post post = JsonConvert.DeserializeObject<Post>(response.Content);

                return post;
            }

            public static Post unstar(string access_token, string id, Parameters parameter = null)
            {
                string requestUrl = baseUrl + "/stream/0/posts/" + id + "/star";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendDeleteRequest(
                        requestUrl,
                        additionalHeaders: headers);


                Post post = JsonConvert.DeserializeObject<Post>(response.Content);

                return post;
            }

            #endregion

            public static Post delete(string access_token, string id, Parameters parameter = null)
            {
                string requestUrl = baseUrl + "/stream/0/posts/" + id;
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendDeleteRequest(requestUrl, headers);

                Post post = JsonConvert.DeserializeObject<Post>(response.Content);

                return post;
            }

            public static List<Post> getStaredByUsername(string access_token, string username, Parameters parameter = null)
            {
                if (string.IsNullOrEmpty(username))
                {
                    return null;
                }
                if (!username.StartsWith("@"))
                {
                    username = "@" + username;
                }
                return getStaredByUserId(access_token, username, parameter);
            }

            public static List<Post> getStaredByUserId(string access_token, string userId, Parameters parameter = null)
            {
                string requestUrl = baseUrl + "/stream/0/users/" + userId + "/stars";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

                List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(response.Content);

                return posts;
            }

            public static List<Post> getMentionsOfUsername(string access_token, string username, Parameters parameter = null)
            {
                if (string.IsNullOrEmpty(username))
                {
                    return null;
                }
                if (!username.StartsWith("@"))
                {
                    username = "@" + username;
                }
                return getMentionsOfUserId(access_token, username, parameter);
            }
            public static List<Post> getMentionsOfUserId(string access_token, string userId, Parameters parameter = null)
            {
                string requestUrl = baseUrl + "/stream/0/users/" + userId + "/mentions";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

                List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(response.Content);

                return posts;
            }

        }
        #endregion

        public class Parameters
        {
            public string since_id { get; set; }
            public string before_id { get; set; }
            public int count { get; set; }
            public int include_muted { get; set; }
            public int include_deleted { get; set; }
            public int include_directed_posts { get; set; }
            public int include_machine { get; set; }
            public int include_annotations { get; set; }
            public int include_starred_by { get; set; }
            public int include_reposters { get; set; }
            public int include_user { get; set; }
        }
    }
}
