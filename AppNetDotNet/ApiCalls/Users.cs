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
        public static Tuple<User, ApiCallResponse> getUserByUsernameOrId(string access_token, string usernameOrId, Parameters parameter = null)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            User user = new User();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
                }
                if (string.IsNullOrEmpty(usernameOrId))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter usernameOrId";
                    return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(usernameOrId);
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

                return Helper.getData<User>(response);
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
        }

        #region Following

        public static Tuple<User,ApiCallResponse> followByUsernameOrId(string access_token, string usernameOrId, Parameters parameter = null)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            User user = new User();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
                }
                if (string.IsNullOrEmpty(usernameOrId))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter usernameOrId";
                    return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(usernameOrId) + "/follow";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendPostRequest(requestUrl, new object(), headers);

                return Helper.getData<User>(response);
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
        }

        public static Tuple<User,ApiCallResponse> unfollowByUsernameOrId(string access_token, string usernameOrId, Parameters parameter = null)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            User user = new User();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
                }
                if (string.IsNullOrEmpty(usernameOrId))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter usernameOrId";
                    return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(usernameOrId) + "/follow";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendDeleteRequest(requestUrl, headers);
                return Helper.getData<User>(response);
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
        }

        public static Tuple<List<User>,ApiCallResponse> getFollowingsOfUser(string access_token, string usernameOrId, Parameters parameter = null)
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
                if (string.IsNullOrEmpty(usernameOrId))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter usernameOrId";
                    return new Tuple<List<User>, ApiCallResponse>(users, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(usernameOrId) + "/following";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);
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

        public static Tuple<List<User>, ApiCallResponse> getFollowersOfUser(string access_token, string usernameOrId, Parameters parameter = null)
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
                if (string.IsNullOrEmpty(usernameOrId))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter usernameOrId";
                    return new Tuple<List<User>, ApiCallResponse>(users, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(usernameOrId) + "/followers";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

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

        #endregion

        #region Muting

        public static Tuple<User, ApiCallResponse> mute(string access_token, string usernameOrId)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            User user = new User();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
                }
                if (string.IsNullOrEmpty(usernameOrId))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter usernameOrId";
                    return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(usernameOrId) + "/mute";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendPostRequest(requestUrl, new object(), headers);

                return Helper.getData<User>(response);
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
        }

        public static Tuple<User, ApiCallResponse> unmute(string access_token, string usernameOrId)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            User user = new User();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
                }
                if (string.IsNullOrEmpty(usernameOrId))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter usernameOrId";
                    return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(usernameOrId) + "/mute";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendDeleteRequest(requestUrl, headers);

                return Helper.getData<User>(response);
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
        }

        public static Tuple<List<User>,ApiCallResponse> getMutedUsers(string access_token, string userNameOrId = "me")
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
                string requestUrl = Common.baseUrl + "/stream/0/users/" + userNameOrId + "/muted";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

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

        #endregion

        #region Blocking

        public static Tuple<User, ApiCallResponse> block(string access_token, string usernameOrId)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            User user = new User();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
                }
                if (string.IsNullOrEmpty(usernameOrId))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter usernameOrId";
                    return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(usernameOrId) + "/block";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendPostRequest(requestUrl, new object(), headers);

                return Helper.getData<User>(response);
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
        }

        public static Tuple<User, ApiCallResponse> unblock(string access_token, string usernameOrId)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            User user = new User();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
                }
                if (string.IsNullOrEmpty(usernameOrId))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter usernameOrId";
                    return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/users/" + Common.formatUserIdOrUsername(usernameOrId) + "/block";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendDeleteRequest(requestUrl, headers);

                return Helper.getData<User>(response);
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<User, ApiCallResponse>(user, apiCallResponse);
        }

        public static Tuple<List<User>, ApiCallResponse> getBlockedUsers(string access_token, string userIdOrName = "me")
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
                string requestUrl = Common.baseUrl + "/stream/0/users/" + userIdOrName + "/blocked";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

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

        public static Tuple<List<User>, ApiCallResponse> getBlockedUserIdsForMultipleUser(string access_token, List<string> userIds)
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
                if (userIds == null)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter userIds";
                    return new Tuple<List<User>, ApiCallResponse>(users, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/users/blocked/ids?=" + string.Join(",",userIds);
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

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

        #endregion

        #region Searches

        public static Tuple<List<User>,ApiCallResponse> searchUsers(string access_token, string searchString)
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
                if (string.IsNullOrEmpty(searchString))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter searchString";
                    return new Tuple<List<User>, ApiCallResponse>(users, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/users/search?q=" + System.Web.HttpUtility.HtmlEncode(searchString);
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

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

        #endregion

        #region Reposters

        public static Tuple<List<User>, ApiCallResponse> getRepostersOfPost(string access_token, string postId)
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
                if (string.IsNullOrEmpty(postId))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter postId";
                    return new Tuple<List<User>, ApiCallResponse>(users, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/posts/" + postId + "/reposters";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

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

        #endregion

        #region Stars

        public static Tuple<List<User>,ApiCallResponse> getUserWhoStarredAPost(string access_token, string postId)
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
                if (string.IsNullOrEmpty(postId))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter postId";
                    return new Tuple<List<User>, ApiCallResponse>(users, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/posts/" + postId + "/stars";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

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

        #endregion

    }
}

