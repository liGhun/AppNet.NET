﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Streams.Model;
using Newtonsoft.Json;

namespace AppNetDotNet.Streams.ApiCalls
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
                apiCallResponse = new ApiCallResponse(response);
                if (apiCallResponse.success)
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.Error += delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                    {
                        throw args.ErrorContext.Error;
                    };
                    token = JsonConvert.DeserializeObject<Token>(response.Content,settings);
                    if (token == null)
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "No user / token available";
                    }
                }
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