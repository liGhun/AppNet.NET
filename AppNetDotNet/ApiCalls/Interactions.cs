using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Model;

namespace AppNetDotNet.ApiCalls
{
    public class Interactions
    {
        public static Tuple<List<Interaction>, ApiCallResponse> getUserInteractionsWithMe(string access_token)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            List<Interaction> interactions = new List<Interaction>();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<List<Interaction>, ApiCallResponse>(interactions, apiCallResponse);
                }

                string requestUrl = Common.baseUrl + "/stream/0/users/me/interactions";

                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(
                        requestUrl,
                        headers);

                return Helper.getData<List<Interaction>>(response);
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<List<Interaction>, ApiCallResponse>(interactions, apiCallResponse);
        }
    }
}
