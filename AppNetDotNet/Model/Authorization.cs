using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNetDotNet.Model
{
    public class Authorization
    {
        public static string clientId { get; set; }

        public class serverSideFlow
        {
            private AuthorizationWindow authWindowServerSide { get; set; }

            public serverSideFlow(string clientId, string redirectUrl, string scope)
            {
                authWindowServerSide = new AuthorizationWindow(clientId, redirectUrl, scope, true);
                authWindowServerSide.AuthSuccess += authWindowServerSide_AuthSuccess;
            }

            public void showAuthWindow()
            {
                if (AuthSuccess == null)
                {
                    throw new AuthSuccessNotMonitoredException();
                }
                if (authWindowServerSide != null)
                {
                    authWindowServerSide.Show();
                }
                else
                {
                    throw new AuthWindowNotAvailableException();
                }
            }

            void authWindowServerSide_AuthSuccess(object sender, AuthorizationWindow.AuthEventArgs e)
            {
                AuthSuccess(this, e);
            }

            public event AuthEventHandler AuthSuccess;
            public delegate void AuthEventHandler(object sender, AuthorizationWindow.AuthEventArgs e);
        }

        public class AuthSuccessNotMonitoredException : Exception { }
        public class AuthWindowNotAvailableException : Exception { }

        public static Authorization AuthorizeNewAccount(string redirectUrl, string scope)
        {
            Authorization account = new Authorization();
            AuthorizationWindow authWindow = new AuthorizationWindow(clientId, redirectUrl, scope, true);
            authWindow.AuthSuccess += authWindow_AuthSuccess;
            authWindow.Show();
            return account;
        }

        static void authWindow_AuthSuccess(object sender, AuthorizationWindow.AuthEventArgs e)
        {
            if (e != null)
            {
                
            }
        }

    }
}
