using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.ApiCalls
{
    public class Common
    {
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
