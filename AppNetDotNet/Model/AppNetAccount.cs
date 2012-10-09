using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNetDotNet.Model
{
    public class AppNetAccount
    {
        public static string clientId { get; set; }

        public static AppNetAccount AuthorizeNewAccount(string redirectUrl, string scope)
        {
            AppNetAccount account = new AppNetAccount();
            Authorize_new_App authWindow = new Authorize_new_App(clientId, redirectUrl, scope);
            authWindow.Show();
            return account;
        }

    }
}
