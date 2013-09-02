using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.HelpMethods
{
    public class GetParameter
    {
        public static string get(Dictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                return null;
            }
            string parameter_string = "";
            foreach (KeyValuePair<string, object> parameter in parameters)
            {
                if (parameter.Value != null && !string.IsNullOrEmpty(parameter.Key))
                {
                    if (!string.IsNullOrWhiteSpace(parameter.Value.ToString()))
                    {
                        parameter_string += string.Format("{0}={1}&", parameter.Key, System.Web.HttpUtility.UrlEncode(parameter.Value.ToString()));
                    }
                }
            }
            parameter_string = parameter_string.TrimEnd('&');

            return parameter_string;
        }

        public static void add_parameter_to_dictionary(ref Dictionary<string,object> current_dictionary, string key, object value)
        {
            if (current_dictionary == null)
            {
                current_dictionary = new Dictionary<string, object>();
            }
            if (!string.IsNullOrEmpty(key))
            {
                current_dictionary.Add(key, value);
            }
        }
    }
}
