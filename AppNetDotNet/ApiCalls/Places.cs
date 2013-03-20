using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Model;
using Newtonsoft.Json;

namespace AppNetDotNet.ApiCalls
{
    public static class Places
    {
        public static Tuple<Place, ApiCallResponse> get(string access_token, string factual_id)
        {
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                Place place = new Place();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<Place, ApiCallResponse>(place, apiCallResponse);
                    }
                    if (string.IsNullOrEmpty(factual_id))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing factual_id";
                        return new Tuple<Place, ApiCallResponse>(place, apiCallResponse);
                    }


                    string requestUrl = Common.baseUrl + "/stream/0/places/" + factual_id;

                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);

                    Helper.Response response = Helper.SendGetRequest(
                            requestUrl,
                            headers);

                    return Helper.getData<Place>(response);
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<Place, ApiCallResponse>(place, apiCallResponse);
            }
        }

        public static Tuple<List<Place>, ApiCallResponse> search(string access_token, 
            decimal latitude, 
            decimal longitude,
            string query = null,
            decimal radius = -1,
            int count = -1,
            decimal altitude = -100000,
            decimal horizontal_accuracy = -100000,
            decimal vertical_accuracy = -100000

            )
        {
            {
                ApiCallResponse apiCallResponse = new ApiCallResponse();
                List<Place> places = new List<Place>();
                try
                {
                    if (string.IsNullOrEmpty(access_token))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "Missing parameter access_token";
                        return new Tuple<List<Place>, ApiCallResponse>(places, apiCallResponse);
                    }
                
                    string searchParameters = "?latitude=" + latitude + "&longitude=" + longitude;
                    searchParameters = searchParameters.Replace(",", ".");
                        if(!string.IsNullOrEmpty(query)) {
                            searchParameters += "&q=" + query;
                        }
                        if (radius >= 0)
                        {
                            searchParameters += "&radius=" + radius;
                        }
                        if (count > 0)
                        {
                            searchParameters += "&count=" + count;
                        }
                        if (altitude != -100000)
                        {
                            searchParameters += "&altitude=" + altitude;
                        }
                        if (horizontal_accuracy != -100000)
                        {
                            searchParameters += "&horizontal_accuracy=" + horizontal_accuracy;
                        }
                        if (vertical_accuracy != -100000)
                        {
                            searchParameters += "&vertical_accuracy=" + vertical_accuracy;
                        }

                        string requestUrl = Common.baseUrl + "/stream/0/places/search" + searchParameters;

                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Authorization", "Bearer " + access_token);

                    Helper.Response response = Helper.SendGetRequest(
                            requestUrl,
                            headers);

                    return Helper.getData<List<Place>>(response);
                }
                catch (Exception exp)
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = exp.Message;
                    apiCallResponse.errorDescription = exp.StackTrace;
                }
                return new Tuple<List<Place>, ApiCallResponse>(places, apiCallResponse);
            }
        }



    }
}
