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
        public static Tuple<Message, ApiCallResponse> createPrivateMessage(string access_token, string text, List<string> receipientUsersnameOrIds, string reply_to = null, List<Annotation> annotations = null, Entities entities = null, int machineOnly = 0)
        {
            return create(access_token, text, "pm", receipientUsersnameOrIds,  reply_to, annotations, entities, machineOnly);
        }

        public static Tuple<Message, ApiCallResponse> create(string access_token, string text, string channelId, List<string> receipientUsersnameOrIds, string reply_to = null, List<Annotation> annotations = null, Entities entities = null, int machineOnly = 0)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            Message message = new Message();
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

                messageCreateParameters messageContent = new messageCreateParameters();
                messageContent.text = text;
                messageContent.reply_to = reply_to;
                messageContent.annotations = annotations;
                messageContent.entities = entities;
                messageContent.machine_only = machineOnly;
                messageContent.destinations = receipientUsersnameOrIds;

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;

                string jsonString = JsonConvert.SerializeObject(messageContent, Formatting.None, settings);

                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendPostRequestStringDataOnly(
                        requestUrl,
                        jsonString,
                        headers,
                        true,
                        contentType:"application/json");

                return Helper.getData<Message>(response);

            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<Message, ApiCallResponse>(message, apiCallResponse);
        }

        public static Tuple<Message, ApiCallResponse> get(string access_token, string channelId, string messageId)
        {

            ApiCallResponse apiCallResponse = new ApiCallResponse();
            Message message = new Message();
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
                Helper.Response response = Helper.SendGetRequest(
                        requestUrl,
                        headers);

                return Helper.getData<Message>(response);
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<Message, ApiCallResponse>(message, apiCallResponse);
        }

        public static Tuple<Message, ApiCallResponse> delete(string access_token, string channelId, string messageId)
        {
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Message message = new Message();
                
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

                    return Helper.getData<Message>(response);
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Message, ApiCallResponse>(message, apiCallResponse);
            }
        }

        public static Tuple<List<Message>, ApiCallResponse> getMessagesInChannel(string access_token, string channelId, messageParameters parameters = null)
        {
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                List<Message> messages = new List<Message>();
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

                    if (parameters != null)
                    {
                        requestUrl += "/?" + parameters.getQueryString();
                    }

                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);
                    Helper.Response response = Helper.SendGetRequest(
                            requestUrl,
                            headers);

                    return Helper.getData<List<Message>>(response);
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<Message>, ApiCallResponse>(messages, apiCallResponse);
            }
        }

        public class messageParameters
        {
            /// <summary>
            /// Should Messages from muted Users be included? Defaults to false except when you specifically request a Message from a muted User.
            /// </summary>
            public bool include_muted { get; set; }
            /// <summary>
            /// Should deleted Messages be included? Defaults to true.
            /// </summary>
            public bool include_deleted { get; set; }
            /// <summary>
            /// Should machine only Messages be included? Defaults to false.
            /// </summary>
            public bool include_machine { get; set; }
            /// <summary>
            /// Should annotations be included in the response objects? Defaults to false.
            /// </summary>
            public bool include_annotations { get; set; }
            /// <summary>
            /// Should User annotations be included in the response objects? Defaults to false.
            /// </summary>
            public bool include_user_annotations { get; set; }
            /// <summary>
            /// Should Message annotations be included in the response objects? Defaults to false.
            /// </summary>
            public bool include_message_annotations { get; set; }

            public messageParameters()
            {
                include_annotations = true;
            }

            public string getQueryString()
            {
                string queryString = "";
                if (!include_muted)
                {
                    queryString += "include_muted=0&";
                }
                if (include_deleted)
                {
                    queryString += "include_deleted=1&";
                }
                if (include_machine)
                {
                    queryString += "include_machine=1&";
                }
                if (include_annotations)
                {
                    queryString += "include_annotations=1&";
                }
                if (include_user_annotations)
                {
                    queryString += "include_user_annotations=1&";
                }
                if (include_message_annotations)
                {
                    queryString += "include_message_annotations=1&";
                }
                queryString = queryString.TrimEnd('&');
                return queryString;
         }   
        }

        public class messageCreateParameters
        {
            public string text { get; set; }
            public string reply_to { get; set; }
            public int machine_only { get; set; }
            public Entities entities { get; set; }
            public List<Annotation> annotations { get; set; }
            // public List<AppNetDotNet.Model.Annotations.AnnotationReplacement_File> annotations { get; set; }
            public List<string> destinations { get; set; }
        }
    }
}
