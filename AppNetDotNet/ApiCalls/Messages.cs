using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Model;
using Newtonsoft.Json;

namespace AppNetDotNet.ApiCalls
{
    public class Messages
    {
        public static Tuple<Message, ApiCallResponse> createPrivateMessage(string access_token, string text, List<string> receipientUsersnameOrIds, string reply_to = null, List<Annotation> annotations = null, Entities entities = null, bool machineOnly = false)
        {
            return create(access_token, text, "pm", receipientUsersnameOrIds,  reply_to, annotations, entities, machineOnly);
        }

        public static Tuple<Message, ApiCallResponse> create(string access_token, string text, string channelId, List<string> receipientUsersnameOrIds, string reply_to = null, List<Annotation> annotations = null, Entities entities = null, bool machineOnly = false)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            Message message = new Message();
            singleMessageResponse responseObject = new singleMessageResponse();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<Message, ApiCallResponse>(message, apiCallResponse);
                }
                if (string.IsNullOrEmpty(text))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter text";
                    return new Tuple<Message, ApiCallResponse>(message, apiCallResponse);
                }
                if (channelId.ToLower() == "pm" && receipientUsersnameOrIds == null)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter destinations for PM";
                    return new Tuple<Message, ApiCallResponse>(message, apiCallResponse);
                }
                string requestUrl = Common.baseUrl;
                if (channelId.ToLower() == "pm")
                {
                    requestUrl += "/stream/0/channels/pm/messages";
                }
                else
                {
                    requestUrl += "/stream/0/channels/" + channelId + "/messages";
                }

                message.text = text;
                message.reply_to = reply_to;
                message.annotations = annotations;
                message.entities = entities;
                message.machine_only = machineOnly;
                message.destinations = receipientUsersnameOrIds;

               /* pmSendParameters sendParams = new pmSendParameters();
                sendParams.text = text;
                sendParams.destinations = receipientUsersnameOrIds; */

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;

                string jsonString = JsonConvert.SerializeObject(message,Formatting.None, settings);

                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                Helper.Response response = Helper.SendPostRequestStringDataOnly(
                        requestUrl,
                        jsonString,
                        headers,
                        true,
                        contentType:"application/json");
                
                apiCallResponse = new ApiCallResponse(response);
                
                if (apiCallResponse.success)
                {
                     responseObject = JsonConvert.DeserializeObject<singleMessageResponse>(response.Content);
                }
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<Message, ApiCallResponse>(responseObject.data, apiCallResponse);
        }

        public static Tuple<Message, ApiCallResponse> get(string access_token, string channelId, string messageId)
        {
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Message message = new Message();
                singleMessageResponse responseObject = new singleMessageResponse();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<Message, ApiCallResponse>(message, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(channelId))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing channelId";
                        return new Tuple<Message, ApiCallResponse>(message, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(messageId))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing messageId";
                        return new Tuple<Message, ApiCallResponse>(message, apiCallResponse);
                    }

                    string requestUrl = Common.baseUrl + "/stream/0/channels/" + channelId + "/messages/" + messageId;

                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                    Helper.Response response = Helper.SendGetRequest(
                            requestUrl,
                            headers);

                    apiCallResponse = new ApiCallResponse(response);

                    if (apiCallResponse.success)
                    {
                        responseObject = JsonConvert.DeserializeObject<singleMessageResponse>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Message, ApiCallResponse>(responseObject.data, apiCallResponse);
            }
        }

        public static Tuple<Message, ApiCallResponse> delete(string access_token, string channelId, string messageId)
        {
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Message message = new Message();
                singleMessageResponse responseObject = new singleMessageResponse();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<Message, ApiCallResponse>(message, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(channelId))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing channelId";
                        return new Tuple<Message, ApiCallResponse>(message, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(messageId))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing messageId";
                        return new Tuple<Message, ApiCallResponse>(message, apiCallResponse);
                    }

                    string requestUrl = Common.baseUrl + "/stream/0/channels/" + channelId + "/messages/" + messageId;

                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                    Helper.Response response = Helper.SendDeleteRequest(
                            requestUrl,
                            headers);

                    apiCallResponse = new ApiCallResponse(response);

                    if (apiCallResponse.success)
                    {
                        responseObject = JsonConvert.DeserializeObject<singleMessageResponse>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Message, ApiCallResponse>(responseObject.data, apiCallResponse);
            }
        }

        public static Tuple<List<Message>, ApiCallResponse> getMessagesInChannel(string access_token, string channelId)
        {
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                List<Message> messages = new List<Message>();
                multipleMessagesResponse responseObject = new multipleMessagesResponse();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<List<Message>, ApiCallResponse>(messages, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(channelId))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing channelId";
                        return new Tuple<List<Message>, ApiCallResponse>(messages, apiCallResponse);
                    }

                    string requestUrl = Common.baseUrl + "/stream/0/channels/" + channelId + "/messages";

                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    headers.Add("X-ADN-Migration-Overrides", "response_envelope=1");
                    Helper.Response response = Helper.SendGetRequest(
                            requestUrl,
                            headers);

                    apiCallResponse = new ApiCallResponse(response);

                    if (apiCallResponse.success)
                    {
                        responseObject = JsonConvert.DeserializeObject<multipleMessagesResponse>(response.Content);
                    }
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<Message>, ApiCallResponse>(responseObject.data, apiCallResponse);
            }
        }

        public class singleMessageResponse
        {
            public Message data { get; set; }
            public Meta meta { get; set; }
        }

        public class multipleMessagesResponse
        {
            public List<Message> data { get; set; }
            public Meta meta { get; set; }
        }
    }
}
