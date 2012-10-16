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
        public static Tuple<Stream,ApiCallResponse> getStatus(string access_token, string streamId, Parameters parameter = null)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            Stream stream = new Stream();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<Stream, ApiCallResponse>(stream, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/streams/" + streamId;
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);
                apiCallResponse = new ApiCallResponse(response);
                if (apiCallResponse.success)
                {
                    stream = JsonConvert.DeserializeObject<Stream>(response.Content);
                }
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<Stream, ApiCallResponse>(stream, apiCallResponse);
        }
    }
}
