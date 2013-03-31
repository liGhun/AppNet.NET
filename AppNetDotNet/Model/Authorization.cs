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
                throw new NotImplementedException();
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
                    authWindowServerSide.webBrowserAuthorization.Navigate(authWindowServerSide.authUrl);
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

        public class clientSideFlow
        {
            private AuthorizationWindow authWindowClientSide { get; set; }
            /// <summary>
            /// Opens a window where the user enters his credentals to authorize your app
            /// You MUST listen on the AuthSuccess event which will give you the access token on success
            /// Be aware that you need to change the registry to bring the WebBrowser control out of quirks mode as described on 
            /// https://github.com/liGhun/AppNet.NET/blob/master/README.md
            /// See Model.Authorization.registerAppInRegistry() to do so automatically
            /// </summary>
            /// <param name="clientId">Your app's client ID</param>
            /// <param name="redirectUrl">The url where App.net will redirect the user on success. While not really important on the client side flow itself it needs to be an available URL</param>
            /// <param name="scope">The space separated list of the scopes your app needs</param>
            public clientSideFlow(string clientId, string redirectUrl, string scope)
            {
                authWindowClientSide = new AuthorizationWindow(clientId, redirectUrl, scope);
                authWindowClientSide.AuthSuccess += authWindowServerSide_AuthSuccess;
            }

            public void showAuthWindow()
            {
                if (AuthSuccess == null)
                {
                    throw new AuthSuccessNotMonitoredException();
                }
                if (authWindowClientSide != null)
                {
                    authWindowClientSide.Show();
                    authWindowClientSide.startAuthorization();
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
            AuthorizationWindow authWindow = new AuthorizationWindow(clientId, redirectUrl, scope);
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



        /// <summary>
        /// Registers the app to use not the quirks mode in browser which would let the auth window fail
        /// See http://www.west-wind.com/weblog/posts/2011/May/21/Web-Browser-Control-Specifying-the-IE-Version for more details
        /// If this registry key is not set the authorization window will fail
        /// </summary>
        /// <param name="whichBrowserToUse">Which type of IE to use (should be at least IE 8 always)</param>
        /// <param name="assemblyName">Name of the .exe (leave empty for autodetect)</param>
        /// <param name="alsoCreateVshostEntry">Will create a second entry with assembly.vshost.exe instead of assembly.exe as this procress is the debugging process in Visual Studio</param>
        public static bool registerAppInRegistry(registerBrowserEmulationValue whichBrowserToUse, string assemblyName = "", bool alsoCreateVshostEntry = true)
        {
            if (whichBrowserToUse == registerBrowserEmulationValue.NoChange)
            {
                return true;
            }
            Microsoft.Win32.RegistryKey registryKey = null;
            Microsoft.Win32.RegistryKey registryKey64 = null;
            if (string.IsNullOrEmpty(assemblyName))
            {
                assemblyName = System.IO.Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().CodeBase);
            }

            registryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION");
            if (Environment.Is64BitOperatingSystem)
            {
                registryKey64 = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION");
            }
            if (whichBrowserToUse == registerBrowserEmulationValue.Remove)
            {
                try
                {
                    registryKey.DeleteValue(assemblyName);
                    if (registryKey64 != null)
                    {
                        registryKey64.DeleteValue(assemblyName);
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                int browser_value = 0;
                // browser registry values taken from http://msdn.microsoft.com/en-us/library/ee330730%28v=vs.85%29.aspx#browser_emulation
                switch (whichBrowserToUse)
                {
                    case registerBrowserEmulationValue.IE7IfDoctypeAvailable:
                        browser_value = 7000;
                        break;
                    case registerBrowserEmulationValue.IE8IfDoctypeAvailable:
                        browser_value = 8000;
                        break;
                    case registerBrowserEmulationValue.IE8Always:
                        browser_value = 8888;
                        break;
                    case registerBrowserEmulationValue.IE9IfDoctypeAvailable:
                        browser_value = 9000;
                        break;
                    case registerBrowserEmulationValue.IE9Always:
                        browser_value = 9999;
                        break;
                    case registerBrowserEmulationValue.IE10IfDoctypeAvailable:
                        browser_value = 10000;
                        break;
                    case registerBrowserEmulationValue.IE10Always:
                        browser_value = 10001;
                        break;
                    default:
                        browser_value = 0;
                        break;
                }
                try
                {
                    registryKey.SetValue(assemblyName, browser_value, Microsoft.Win32.RegistryValueKind.DWord);
                    if (alsoCreateVshostEntry)
                    {
                        registryKey.SetValue(assemblyName.ToLower().Replace(".exe", ".vshost.exe"), browser_value, Microsoft.Win32.RegistryValueKind.DWord);
                    }
                    if (registryKey64 != null)
                    {
                        registryKey64.SetValue(assemblyName, browser_value, Microsoft.Win32.RegistryValueKind.DWord);
                        if (alsoCreateVshostEntry)
                        {
                            registryKey64.SetValue(assemblyName.ToLower().Replace(".exe", ".vshost.exe"), browser_value, Microsoft.Win32.RegistryValueKind.DWord);
                        }
                    }
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                    return false;
                }

            }
            return true;
        }

        public enum registerBrowserEmulationValue
        {
            NoChange,
            Remove,
            IE7IfDoctypeAvailable,
            IE8IfDoctypeAvailable,
            IE8Always,
            IE9IfDoctypeAvailable,
            IE9Always,
            IE10IfDoctypeAvailable,
            IE10Always
        }
    }
}
