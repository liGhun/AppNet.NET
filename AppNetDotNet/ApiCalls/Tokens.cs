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
        public static Tuple<Token, ApiCallResponse> get(string access_token)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            Token token = new Token();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<Token, ApiCallResponse>(token, apiCallResponse);
                }
                string requestUrl = Common.baseUrl + "/stream/0/token";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);

                Tuple<Token, ApiCallResponse> returnValue = Helper.getData<Token>(response);

                return returnValue;
                
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<Token, ApiCallResponse>(token, apiCallResponse);
        }

    }
}
