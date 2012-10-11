using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security.Permissions;

namespace NymphAppNetTester
{
    /// <summary>
    /// Interaction logic for BrowserTest.xaml
    /// </summary>
    /// 

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]

    public partial class BrowserTest : Window
    {

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public BrowserTest()
        {
            InitializeComponent();

            
        }

        private void buttonGo_Click_1(object sender, RoutedEventArgs e)
        {
            webbrowserTest.Navigate(textboxUrl.Text);
        }
    }
}
