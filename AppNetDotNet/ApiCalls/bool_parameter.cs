using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.ApiCalls
{
    public class bool_parameter
    {
        public bool? value { get; set; }
        public override string ToString()
        {
            if (value == null)
            {
                return "";
            }
            else if (value == true)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        public bool_parameter(bool value)
        {
            this.value = value;
        }

        public static string get_get_parameter(string parameter_name, bool_parameter parameter_value, bool include_tailing_ampersand = false) {
            string text = "";
            if (!string.IsNullOrWhiteSpace(parameter_name) && parameter_value != null)
            {
                text = string.Format("{0}={1}", parameter_name, parameter_value);
                if (include_tailing_ampersand)
                {
                    text += "&";
                }
            }
            return text;
        }
    }
}
