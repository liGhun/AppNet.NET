using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppNetDotNet.Model;
using Newtonsoft.Json;
using System.Net;

namespace AppNetDotNet.ApiCalls
{
    public class Files
    {
        public static Encoding encoding = Encoding.ASCII;

        public static Tuple<List<File>, ApiCallResponse> getMyFiles(string access_token, FileQueryParameters parameter = null)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            List<File> files = new List<File>();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<List<File>, ApiCallResponse>(files, apiCallResponse);
                }

                string requestUrl = Common.baseUrl + "/stream/0/users/me/files";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);
                apiCallResponse = new ApiCallResponse(response);
                if (apiCallResponse.success)
                {
                    files = JsonConvert.DeserializeObject<List<File>>(response.Content);
                }
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<List<File>, ApiCallResponse>(files, apiCallResponse);
        }


        public static Tuple<List<File>, ApiCallResponse> getFiles(string access_token, string comma_separated_list_of_ids, FileQueryParameters parameter = null)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            List<File> files = new List<File>();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<List<File>, ApiCallResponse>(files, apiCallResponse);
                }
                if (string.IsNullOrEmpty(comma_separated_list_of_ids))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter comma_separated_list_of_ids";
                    return new Tuple<List<File>, ApiCallResponse>(files, apiCallResponse);
                }

                string requestUrl = Common.baseUrl + "/stream/0/files?ids=" + comma_separated_list_of_ids;
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);
                apiCallResponse = new ApiCallResponse(response);
                if (apiCallResponse.success)
                {
                    files = JsonConvert.DeserializeObject<List<File>>(response.Content);
                }
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<List<File>, ApiCallResponse>(files, apiCallResponse);
        }

        public static Tuple<File, ApiCallResponse> getFile(string access_token, string file_id, FileQueryParameters parameter = null)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            File file = new File();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<File, ApiCallResponse>(file, apiCallResponse);
                }
                if (string.IsNullOrEmpty(file_id))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter file_id";
                    return new Tuple<File, ApiCallResponse>(file, apiCallResponse);
                }

                string requestUrl = Common.baseUrl + "/stream/0/files/" + file_id;
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);
                Helper.Response response = Helper.SendGetRequest(requestUrl, headers);
                apiCallResponse = new ApiCallResponse(response);
                if (apiCallResponse.success)
                {
                    file = JsonConvert.DeserializeObject<File>(response.Content);
                }
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<File, ApiCallResponse>(file, apiCallResponse);
        }

        public static Tuple<File, ApiCallResponse> setContent(string access_token, string file_id, string local_file_path, string file_type = null, FileQueryParameters parameter = null)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            File file = new File();
            try
            {
                if (string.IsNullOrEmpty(access_token))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter access_token";
                    return new Tuple<File, ApiCallResponse>(file, apiCallResponse);
                }
                if (string.IsNullOrEmpty(local_file_path))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter local_file_path";
                    return new Tuple<File, ApiCallResponse>(file, apiCallResponse);
                }
                if (string.IsNullOrEmpty(file_id))
                {
                    apiCallResponse.success = false;
                    apiCallResponse.errorMessage = "Missing parameter file_id";
                    return new Tuple<File, ApiCallResponse>(file, apiCallResponse);
                }

                string requestUrl = Common.baseUrl + "/stream/0/files/" + file_id + "/content";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);

                    if (!System.IO.File.Exists(local_file_path))
                    {
                        apiCallResponse.success = false;
                        apiCallResponse.errorMessage = "File not found";
                        apiCallResponse.errorDescription = "The local file has not been found at " + local_file_path;
                    }
                    else
                    {
                        System.IO.FileStream fs = new System.IO.FileStream(local_file_path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        byte[] data = new byte[fs.Length];
                        fs.Read(data, 0, data.Length);
                        fs.Close();
                         
                        Helper.Response response = Helper.SendPutRequestBinaryDataOnly(
                                requestUrl,
                                data,
                                headers,
                                true,
                                contentType: Helper.getMimeFromFile(local_file_path)
                                );

                        apiCallResponse = new ApiCallResponse(response);

                        if (apiCallResponse.success)
                        {
                            file = JsonConvert.DeserializeObject<File>(response.Content);
                        }
                    }
            
            }
            catch (WebException e)
            {
                WebResponse response = e.Response;
                
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    System.IO.Stream data = response.GetResponseStream();
                    
                        string text = new System.IO.StreamReader(data).ReadToEnd();
                        Console.WriteLine(text);
                    
                
            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<File, ApiCallResponse>(file, apiCallResponse);
        }

        public static Tuple<File, ApiCallResponse> create(string access_token, string local_file_path = null, string name = null, string type = null, List<Annotation> annotations = null, string kind = null, FileQueryParameters parameter = null)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            File file = new File();
            if (string.IsNullOrEmpty(access_token))
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = "Missing parameter access_token";
                return new Tuple<File, ApiCallResponse>(new File(), apiCallResponse);
            }
            if (string.IsNullOrEmpty(type))
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = "Missing parameter type";
                return new Tuple<File, ApiCallResponse>(new File(), apiCallResponse);
            }

            try
            {

                FileCreateParameters tempFile = new FileCreateParameters();
                if (!string.IsNullOrEmpty(local_file_path))
                {
                    tempFile.name = System.IO.Path.GetFileName(local_file_path);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    tempFile.name = name;
                }

                tempFile.type = type;
                tempFile.annotations = annotations;
                tempFile.kind = kind;

                string requestUrl = Common.baseUrl + "/stream/0/files/";


                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;

                string jsonString = JsonConvert.SerializeObject(tempFile, Formatting.None, settings);

                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);

                Helper.Response response = Helper.SendPostRequestStringDataOnly(
                        requestUrl,
                        jsonString,
                        headers,
                        true,
                        contentType: "application/json");

                apiCallResponse = new ApiCallResponse(response);

                if (apiCallResponse.success)
                {
                    file = JsonConvert.DeserializeObject<File>(response.Content);
                    if (!string.IsNullOrEmpty(local_file_path))
                    {
                        return setContent(access_token, file.id, local_file_path);
                    }
                }

            }
            catch (WebException e)
            {
                WebResponse response = e.Response;

                HttpWebResponse httpResponse = (HttpWebResponse)response;
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                System.IO.Stream data = response.GetResponseStream();

                string text = new System.IO.StreamReader(data).ReadToEnd();
                Console.WriteLine(text);


            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<File, ApiCallResponse>(file, apiCallResponse);
        }

        public static Tuple<File, ApiCallResponse> update(string access_token, string file_id, string name = null, List<Annotation> annotations = null, FileQueryParameters parameter = null)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            File file = new File();
            if (string.IsNullOrEmpty(access_token))
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = "Missing parameter access_token";
                return new Tuple<File, ApiCallResponse>(new File(), apiCallResponse);
            }
            if (string.IsNullOrEmpty(file_id))
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = "Missing parameter file_id";
                return new Tuple<File, ApiCallResponse>(new File(), apiCallResponse);
            }
            if (string.IsNullOrEmpty(name) && annotations == null)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = "Missing parameter name or annotations - at least one of those must be set";
                return new Tuple<File, ApiCallResponse>(new File(), apiCallResponse);
            }


            try
            {

                FileUpdateParameters tempFile = new FileUpdateParameters();
                tempFile.name = name;
                tempFile.annotations = annotations;

                string requestUrl = Common.baseUrl + "/stream/0/files/" + file_id;
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;

                string jsonString = JsonConvert.SerializeObject(tempFile, Formatting.None, settings);

                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);

                Helper.Response response = Helper.SendPutRequestStringDataOnly(
                        requestUrl,
                        jsonString,
                        headers,
                        true,
                        contentType: "application/json");

                apiCallResponse = new ApiCallResponse(response);

                if (apiCallResponse.success)
                {
                    file = JsonConvert.DeserializeObject<File>(response.Content);
                }

            }
            catch (WebException e)
            {
                WebResponse response = e.Response;

                HttpWebResponse httpResponse = (HttpWebResponse)response;
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                System.IO.Stream data = response.GetResponseStream();

                string text = new System.IO.StreamReader(data).ReadToEnd();
                Console.WriteLine(text);


            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<File, ApiCallResponse>(file, apiCallResponse);
        }

        public static Tuple<File, ApiCallResponse> delete(string access_token, string file_id, FileQueryParameters parameter = null)
        {
            ApiCallResponse apiCallResponse = new ApiCallResponse();
            File file = new File();
            if (string.IsNullOrEmpty(access_token))
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = "Missing parameter access_token";
                return new Tuple<File, ApiCallResponse>(new File(), apiCallResponse);
            }
            if (string.IsNullOrEmpty(file_id))
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = "Missing parameter file_id";
                return new Tuple<File, ApiCallResponse>(new File(), apiCallResponse);
            }
            try
            {
                string requestUrl = Common.baseUrl + "/stream/0/files/" + file_id;
              
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + access_token);

                Helper.Response response = Helper.SendDeleteRequest( 
                        requestUrl,
                        headers
                        );

                apiCallResponse = new ApiCallResponse(response);

                if (apiCallResponse.success)
                {
                    file = JsonConvert.DeserializeObject<File>(response.Content);
                }

            }
            catch (WebException e)
            {
                WebResponse response = e.Response;

                HttpWebResponse httpResponse = (HttpWebResponse)response;
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                System.IO.Stream data = response.GetResponseStream();

                string text = new System.IO.StreamReader(data).ReadToEnd();
                Console.WriteLine(text);


            }
            catch (Exception exp)
            {
                apiCallResponse.success = false;
                apiCallResponse.errorMessage = exp.Message;
                apiCallResponse.errorDescription = exp.StackTrace;
            }
            return new Tuple<File, ApiCallResponse>(file, apiCallResponse);
        }


        public class FileQueryParameters
        {
            public string file_types { get; set; }
            public int include_incomplete { get; set; }
            public int include_annotations { get; set; }
            public int include_file_annotations { get; set; }
            public int include_user_annotations { get; set; }
        }

        private class FileCreateParameters
        {
            public string type { get; set; }
            public string name { get; set; }
            public string kind { get; set; }
            public List<Annotation> annotations { get; set; }
        }

        private class FileUpdateParameters
        {
            public string name { get; set; }
            public List<Annotation> annotations { get; set; }
        }
    }
}
