using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using AppNetDotNet.Model;

namespace AppNetDotNet.ApiCalls
{
    public static class Users
    {
        private static string baseUrl = "https://alpha-api.app.net";


        public static User getUserByUsername(string access_token, string username, Parameters parameter = null)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            if (!username.StartsWith("@"))
            {
                username = "@" + username;
            }
            return getUserById(access_token, username, parameter);
        }
        public static User getUserById(string access_token, string userId, Parameters parameter = null)
        {
            string requestUrl = baseUrl + "/stream/0/users/" + userId;
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

            User user = JsonConvert.DeserializeObject<User>(response.Content);

            return user;
        }

        #region Following

        public static User followByUsername(string access_token, string username, Parameters parameter = null)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            if (!username.StartsWith("@"))
            {
                username = "@" + username;
            }
            return followById(access_token, username, parameter);
        }
        public static User followById(string access_token, string userId, Parameters parameter = null)
        {
            string requestUrl = baseUrl + "/stream/0/users/" + userId + "/follow";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendPostRequest(requestUrl, new object(), headers);

            User user = JsonConvert.DeserializeObject<User>(response.Content);

            return user;
        }

          public static User unfollowByUsername(string access_token, string username, Parameters parameter = null)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            if (!username.StartsWith("@"))
            {
                username = "@" + username;
            }
            return unfollowById(access_token, username, parameter);
        }
        public static User unfollowById(string access_token, string userId, Parameters parameter = null)
        {
            string requestUrl = baseUrl + "/stream/0/users/" + userId + "/follow";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendDeleteRequest(requestUrl, headers);

            User user = JsonConvert.DeserializeObject<User>(response.Content);

            return user;
        }

        public static List<User> getFollowingsOfUser(string access_token, string userIdOrUsername, Parameters parameter = null)
        {
            string requestUrl = baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(userIdOrUsername) + "/following";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

            List<User> users = JsonConvert.DeserializeObject<List<User>>(response.Content);

            return users;
        }

        public static List<User> getFollowersOfUser(string access_token, string userIdOrUsername, Parameters parameter = null)
        {
            string requestUrl = baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(userIdOrUsername) + "/followers";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

            List<User> users = JsonConvert.DeserializeObject<List<User>>(response.Content);

            return users;
        }

        #endregion

        #region Muting

        public static User mute(string access_token, string userIdOrUsername, Parameters parameter = null)
        {
            string requestUrl = baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(userIdOrUsername) + "/mute";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendPostRequest(requestUrl, new object(), headers);

            User user = JsonConvert.DeserializeObject<User>(response.Content);

            return user;
        }

        public static User unmute(string access_token, string userIdOrUsername, Parameters parameter = null)
        {
            string requestUrl = baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(userIdOrUsername) + "/mute";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendDeleteRequest(requestUrl, headers);

            User user = JsonConvert.DeserializeObject<User>(response.Content);

            return user;
        }

        public static List<User> getMutedUsers(string access_token, Parameters parameter = null)
        {
            string requestUrl = baseUrl + "/stream/0/users/me/muted";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

            List<User> users = JsonConvert.DeserializeObject<List<User>>(response.Content);

            return users;
        }

        #endregion

        #region Searches

        public static List<User> searchUsers(string access_token, string searchString, Parameters parameter = null)
        {
            string requestUrl = baseUrl + "/stream/0/users/search?q=" + System.Web.HttpUtility.HtmlEncode(searchString);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

            List<User> users = JsonConvert.DeserializeObject<List<User>>(response.Content);

            return users;
        }

        #endregion

        #region Reposters

        public static List<User> getRepostersOfPost(string access_token, string postId, Parameters parameter = null)
        {
            string requestUrl = baseUrl + "/stream/0/posts/" + postId + "/reposters";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

            List<User> users = JsonConvert.DeserializeObject<List<User>>(response.Content);

            return users;
        }

        #endregion

        #region Reposters

        public static List<User> getUserWhoStarredAPost(string access_token, string postId, Parameters parameter = null)
        {
            string requestUrl = baseUrl + "/stream/0/posts/" + postId + "/stars";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

            List<User> users = JsonConvert.DeserializeObject<List<User>>(response.Content);

            return users;
        }

        #endregion

    }
}

