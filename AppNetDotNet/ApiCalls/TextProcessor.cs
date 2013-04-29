using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Model;

namespace AppNetDotNet.ApiCalls
{
    public class TextProcessor
    {
        /// <summary>
        /// When a request is made to create a Post the provided body text is processed for entities. You can use this endpoint to test how App.net will parse text for entities as well as render text as html. 
        /// Calls to this endpoint will not create or update any objects in App.net
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Tuple<Post, ApiCallResponse> process(string access_token, string text)
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
                string requestUrl = Common.baseUrl + "/stream/0/text/process";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendPostRequestStringDataOnly(
                    requestUrl, 
                    "text=" + System.Web.HttpUtility.UrlEncode(text),
                    headers, 
                    true);

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
    }
}
