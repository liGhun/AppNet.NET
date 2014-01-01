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
        public static Tuple<Message, ApiCallResponse> createPrivateMessage(string access_token, string text, List<string> receipientUsersnameOrIds, string reply_to = null, List<Annotation> annotations = null, Entities entities = null, int machineOnly = 0, bool? parse_links = null, List<File> toBeEmbeddedFiles = null)
        {
            return create(access_token, text, "pm", receipientUsersnameOrIds, reply_to:reply_to, annotations:annotations, entities:entities, machineOnly:machineOnly, parse_links:parse_links, toBeEmbeddedFiles:toBeEmbeddedFiles);
        }

        public static Tuple<Message, ApiCallResponse> create(
            string access_token, 
            string text, 
            string channelId, 
            List<string> receipientUsersnameOrIds, 
            string reply_to = null, 
            List<Annotation> annotations = null, 
            Entities entities = null, 
            int machineOnly = 0, 
            bool? parse_links = true,
            bool? parse_markdown_links = true,
            List<File> toBeEmbeddedFiles = null)
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
                if (string.IsNullOrEmpty(text) && machineOnly == 0)
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

                if(entities == null && (parse_links != null || parse_markdown_links != null)) {
                    entities = new Entities();
                    entities.hashtags = null;
                    entities.links = null;
                    entities.mentions = null;
                }

                if (entities != null)
                {
                    entities.parse_links = parse_links;
                    entities.parse_markdown_links = parse_markdown_links;
                }

                messageCreateParameters messageContent = new messageCreateParameters();
                messageContent.text = text;
                messageContent.reply_to = reply_to;
                messageContent.entities = entities;
                messageContent.machine_only = machineOnly;
                messageContent.destinations = receipientUsersnameOrIds;

                if (toBeEmbeddedFiles != null)
                {
                    List<AppNetDotNet.Model.Annotations.AnnotationReplacement_File> files = new List<AppNetDotNet.Model.Annotations.AnnotationReplacement_File>();
                    if (annotations == null)
                    {
                        annotations = new List<Annotation>();
                    }
                    foreach (File file in toBeEmbeddedFiles)
                    {
                        AppNetDotNet.Model.Annotations.AnnotationReplacement_File fileReplacementAnnotation = new AppNetDotNet.Model.Annotations.AnnotationReplacement_File(file);
                        Annotation file_annotation = new Annotation(fileReplacementAnnotation.type, fileReplacementAnnotation.value);
                        annotations.Add(file_annotation);
                    }
                }

                List<Annotation.JSON_body> annotations_body = new List<Annotation.JSON_body>();
                if (annotations != null)
                {
                    if (annotations.Count > 0)
                    {
                        foreach (Annotation annotation in annotations)
                        {
                            annotations_body.Add(annotation.json_body);
                        }
                        messageContent.annotations = annotations_body;
                    }
                }

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;

                string jsonString = JsonConvert.SerializeObject(messageContent, Formatting.None, settings);
                if (toBeEmbeddedFiles != null)
                {
                    jsonString = jsonString.Replace("netAppCoreFile_dummy_for_replacement", "+net.app.core.file");
                }

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
            public List<Annotation.JSON_body> annotations { get; set; }
            // public List<AppNetDotNet.Model.Annotations.AnnotationReplacement_File> annotations { get; set; }
            public List<string> destinations { get; set; }
         
        }
    }
}
