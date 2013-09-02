using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Model;

namespace AppNetDotNet.ApiCalls
{
    public static class Searches
    {
        /// <summary>
        /// An easy search query about a string - if hashtag, mention or so are automatically extracted
        /// In fact a search is started with only the query paramter being set (see App.net developer documentation
        /// </summary>
        /// <param name="access_token">the access token</param>
        /// <param name="search_string">the query string</param>
        /// <returns></returns>
        public static Tuple<List<Post>, ApiCallResponse> search_string(string access_token, string search_string)
        {
            return search_extended(access_token, query: search_string);
        }

        /// <summary>
        /// A complete search API implementation with all parameters included as optional parameters
        /// You should of course set at least one...
        /// Parameter description is copy & paste from the App.net developer documentation
        /// </summary>
        /// <param name="access_token">the access token</param>
        /// <param name="index">Type of index to use. The default (and currently, the only) index is complete, which searches all posts. We may add additional index types later (e.g., an index only of recent posts, for speed.)</param>
        /// <param name="order">One of: id (default), score. Searches of ordering id are returned in roughly the same order as other streams, and support pagination. Searches of ordering score are returned by a relevance score. Currently, the only ordering that supports pagination is id, and we are working on improving relevance scores.</param>
        /// <param name="query">Automatically attempts to extract hashtags and mentions while searching text. If you do not want this behavior, you can use more specific parameters below.</param>
        /// <param name="text">Include posts containing certain text.</param>
        /// <param name="hashtags">Only include posts tagged with certain hashtags. Do not include #</param>
        /// <param name="links">Only include posts linking to certain URLs</param>
        /// <param name="link_domains">Only include posts linking to certain domains. Do not include "www."</param>
        /// <param name="leading_mentions">Only include posts directed at users, by username. Do not include @</param>
        /// <param name="annotation_types">Only include posts with a specific annotation type, e.g., net.app.core.fallback_url</param>
        /// <param name="attachment_types">Only include posts with a specific file type attached via the net.app.core.file_list annotation</param>
        /// <param name="crosspost_url">Only include posts which are crossposts of a specific URL, via the net.app.core.crosspost annotation</param>
        /// <param name="crosspost_domain">Similar to crosspost_url, but only match on the host portion of the URL. Do not include "www."</param>
        /// <param name="place_id">Only include posts which are check-ins at a specific place, via the net.app.core.checkin annotation</param>
        /// <param name="is_reply">Only include replies</param>
        /// <param name="is_directed">Only include posts with leading mentions, i.e., posts which were directed at other users</param>
        /// <param name="has_location">Only include posts with leading mentions, i.e., posts which were directed at other users</param>
        /// <param name="has_checkin">Only include posts containing place IDs, i.e., tagged with the net.app.core.checkin annotation</param>
        /// <param name="is_crosspost">Only include posts which are crossposts, i.e., tagged with the net.app.core.crosspost annotation</param>
        /// <param name="has_attachment">Only include posts with file attachments</param>
        /// <param name="has_oembed_photo">Only include posts with photo oembed annotations</param>
        /// <param name="has_oembed_video">Only include posts with video (not html5video) oembed annotations</param>
        /// <param name="has_oembed_html5video">Only include posts with html5video oembed annotations</param>
        /// <param name="has_oembed_rich">Only include posts with rich oembed anntations</param>
        /// <param name="language">Only include posts with a certain language tagged with the net.app.core.language annotation.</param>
        /// <param name="client_id">Only include posts created by a certain app. Use the alphanumeric client_id</param>
        /// <param name="creator_id">Only include posts created by a specific user. Use the user ID, not the username</param>
        /// <param name="reply_to">Only include immediate replies to a given post ID</param>
        /// <param name="thread_id">Only include posts on a specific thread</param>
        /// <returns></returns>
        public static Tuple<List<Post>, ApiCallResponse> search_extended(
            string access_token,
            string index = null,
            string order = null,
            string query = null,
            string text = null,
            string hashtags = null,
            string links = null,
            string link_domains = null,
            string leading_mentions = null,
            string annotation_types = null,
            string attachment_types = null,
            string crosspost_url = null,
            string crosspost_domain = null,
            string place_id = null,
            bool_parameter is_reply = null,
            bool_parameter is_directed = null,
            bool_parameter has_location = null,
            bool_parameter has_checkin = null,
            bool_parameter is_crosspost = null,
            bool_parameter has_attachment = null,
            bool_parameter has_oembed_photo = null,
            bool_parameter has_oembed_video = null,
            bool_parameter has_oembed_html5video = null,
            bool_parameter has_oembed_rich = null,
            string language = null,
            string client_id = null,
            string creator_id = null,
            string reply_to = null,
            string thread_id = null) 
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            List<Post> posts = new List<Post>();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<List<Post>, ApiCallResponse>(posts, apiCallResponse);
                }

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "index", index);
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "order", order);
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "query", query);
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "text", text);
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "hashtags", hashtags);
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "links", links);
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "link_domains", link_domains);
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "leading_mentions", leading_mentions);
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "annotation_types", annotation_types);
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "attachment_types", attachment_types);
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "crosspost_url", crosspost_url);
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "crosspost_domain", crosspost_domain);
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "place_id",place_id);
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "is_reply", is_reply);
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "is_directed", is_directed); 
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "has_location", has_location); 
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "has_checkin",has_checkin); 
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "is_crosspost", is_crosspost); 
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "has_attachment", has_attachment); 
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "has_oembed_photo", has_oembed_photo); 
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "has_oembed_video", has_oembed_video); 
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "has_oembed_html5video", has_oembed_html5video); 
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "has_oembed_rich", has_oembed_rich); 
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "language", language); 
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "client_id", client_id); 
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "creator_id", creator_id); 
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "reply_to", reply_to); 
                HelpMethods.GetParameter.add_parameter_to_dictionary(ref parameters, "thread_id", thread_id);

                string query_string = HelpMethods.GetParameter.get(parameters);

                if(string.IsNullOrWhiteSpace(query_string)) {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter - please provide at least one paramter for the search";
                    return new Tuple<List<Post>, ApiCallResponse>(posts, apiCallResponse);
                }

                string requestUrl = Common.baseUrl + "/stream/0/posts/search?" + query_string;

                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);

                Helper.Response response = Helper.SendGetRequest(
                        requestUrl,
                        headers);

                return Helper.getData<List<Post>>(response);
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<List<Post>, ApiCallResponse>(posts, apiCallResponse);
        }
    }
}

