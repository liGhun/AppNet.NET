using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Model;
using Newtonsoft.Json;

namespace AppNetDotNet.ApiCalls
{
    public class Streams
    {
        public static Stream getStatus(string access_token, string streamId, Parameters parameter = null)
        {
            string requestUrl = Common.baseUrl + "/stream/0/streams/" + streamId;
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + access_token);
            Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

            Stream stream = JsonConvert.DeserializeObject<Stream>(response.Content);

            return stream;
        }
    }
}
