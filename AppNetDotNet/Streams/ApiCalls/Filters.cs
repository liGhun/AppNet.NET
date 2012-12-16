using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Streams.Model;
using Newtonsoft.Json;

namespace AppNetDotNet.Streams.ApiCalls
{
    public static class Filters
    {
        public static Tuple<List<Filter>, ApiCallResponse> getForCurrentUser(string access_token)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            List<Filter> filters = new List<Filter>();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<List<Filter>, ApiCallResponse>(filters, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/filters";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);
                apiCallResponse = new ApiCallResponse(response);
                if (apiCallResponse.success)
                {
                    filters = JsonConvert.DeserializeObject<List<Filter>>(response.Content);
                }
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<List<Filter>, ApiCallResponse>(filters, apiCallResponse);
        }

        public static Tuple<Filter, ApiCallResponse> getbyId(string access_token, string filterId)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            Filter filter = new Filter();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<Filter, ApiCallResponse>(filter, apiCallResponse);
                }
                if (string.IsNullOrEmpty(filterId))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter filterId";
                    return new Tuple<Filter, ApiCallResponse>(filter, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/filters/" + filterId;
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);
                apiCallResponse = new ApiCallResponse(response);
                if (apiCallResponse.success)
                {
                    filter = JsonConvert.DeserializeObject<Filter>(response.Content);
                }
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<Filter, ApiCallResponse>(filter, apiCallResponse);
        }

        public static Tuple<Filter, ApiCallResponse> delete(string access_token, string filterId)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            Filter filter = new Filter();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<Filter, ApiCallResponse>(filter, apiCallResponse);
                }
                if (string.IsNullOrEmpty(filterId))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter filterId";
                    return new Tuple<Filter, ApiCallResponse>(filter, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/filters/" + filterId;
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendDeleteRequest(requestUrl, headers);
                apiCallResponse = new ApiCallResponse(response);
                if (apiCallResponse.success)
                {
                    filter = JsonConvert.DeserializeObject<Filter>(response.Content);
                }
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<Filter, ApiCallResponse>(filter, apiCallResponse);
        }

        public static Tuple<Filter,ApiCallResponse> create(string access_token, Filter filter)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            Filter createdFilter = new Filter();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<Filter, ApiCallResponse>(createdFilter, apiCallResponse);
                }
                if (filter == null)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter Filter";
                    return new Tuple<Filter, ApiCallResponse>(createdFilter, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/filters";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendPostRequest(requestUrl, new
                {
                    createdFilter = JsonConvert.SerializeObject(filter)
                },
                    headers);
                apiCallResponse = new ApiCallResponse(response);
                if (apiCallResponse.success)
                {
                    createdFilter = JsonConvert.DeserializeObject<Filter>(response.Content);
                }
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<Filter, ApiCallResponse>(filter, apiCallResponse);
        }
    }
}
