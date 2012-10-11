using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Model;
using Newtonsoft.Json;

namespace AppNetDotNet.ApiCalls
{
    public class Tokens
    {
        public static Token get(string access_token, Parameters parameter = null)
        {
            string requestUrl = Common.baseUrl + "/stream/0/token";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

            Token token = JsonConvert.DeserializeObject<Token>(response.Content);

            return token;
        }
    }
}
