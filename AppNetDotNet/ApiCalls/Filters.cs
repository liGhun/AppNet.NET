using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Model;
using Newtonsoft.Json;

namespace AppNetDotNet.ApiCalls
{
    public static class Filters
    {
        public static List<Filter> getForCurrentUser(string access_token, Parameters parameter = null)
        {
            string requestUrl = Common.baseUrl + "/stream/0/filters";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

            List<Filter> filters = JsonConvert.DeserializeObject<List<Filter>>(response.Content);

            return filters;
        }

        public static Filter getbyId(string access_token, string filterId, Parameters parameter = null)
        {
            string requestUrl = Common.baseUrl + "/stream/0/filters/" + filterId;
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

            Filter filter = JsonConvert.DeserializeObject<Filter>(response.Content);

            return filter;
        }

        public static List<Filter> delete(string access_token, string filterId, Parameters parameter = null)
        {
            string requestUrl = Common.baseUrl + "/stream/0/filters/" + filterId;
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendDeleteRequest(requestUrl, headers);

            List<Filter> filter = JsonConvert.DeserializeObject<List<Filter>>(response.Content);

            return filter;
        }

        public static Filter create(string access_token, Filter filter, Parameters parameter = null)
        {
            string requestUrl = Common.baseUrl + "/stream/0/filters";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendPostRequest(requestUrl, new {
                filter = JsonConvert.SerializeObject(filter)
            },
                headers);

            Filter createdFilter = JsonConvert.DeserializeObject<Filter>(response.Content);

            return createdFilter;
        }
    }
}
