using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Streams.ApiCalls
{
   
    public class Common
    {
        public static string baseUrl = "https://alpha-api.app.net";
        public static string formatUserIdOrUsername(string userIdentifier)
        {
            if(userIdentifier == null) {
                return null;
            }
            int dummy;
            if (int.TryParse(userIdentifier, out dummy))
            {
                return userIdentifier;
            }
            else
            {
                if (userIdentifier.ToLower() == "me")
                {
                    return userIdentifier;
                }
                else if (userIdentifier.StartsWith("@"))
                {
                    return userIdentifier;
                }
                else
                {
                    return "@" + userIdentifier;
                }
            }
        }
    }
}
