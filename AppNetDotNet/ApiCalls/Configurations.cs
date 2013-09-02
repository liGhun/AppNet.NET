using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Model;

namespace AppNetDotNet.ApiCalls
{
    public class Configurations
    {
        public static Tuple<Configuration, ApiCallResponse> get()
        {
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Configuration configuration = new Configuration();
                try
                {
                    string requestUrl = Common.baseUrl + "/stream/0/config";

                    Dictionary<string, string> headers = new Dictionary<string, string>();

                    Helper.Response response = Helper.SendGetRequest(
                            requestUrl,
                            headers);

                    return Helper.getData<Configuration>(response);
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Configuration, ApiCallResponse>(configuration, apiCallResponse);
            }
        }
    }
}
