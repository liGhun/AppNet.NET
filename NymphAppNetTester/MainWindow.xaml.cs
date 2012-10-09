using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppNetDotNet;

namespace NymphAppNetTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.Model.AppNetAccount.clientId = "8D2p3y9FgZWaKuVvkYnkNShS5KXuNB2m";
            AppNetDotNet.Model.AppNetAccount account = AppNetDotNet.Model.AppNetAccount.AuthorizeNewAccount("http://www.nymphicusapp.com/windows/appnet/?newaccount=authsuccess", "basic stream write_post follow messages");
        }

        private void buttonGetPersonalStream_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Streams.getUserStream(textboxAccessToken.Text);
        }

        private void buttonWritePost_Click(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Posts.write(textboxAccessToken.Text, textboxPostText.Text);
        }

        private void buttonGetPostById_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Posts.getById(textboxAccessToken.Text, textboxGetPostById.Text);
        }

        private void buttonDeletePostById_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Posts.delete(textboxAccessToken.Text, textboxDeletePostById.Text);
        }

        private void buttonGetPostRepliesById_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Posts.getRepliesById(textboxAccessToken.Text, textboxGetPostById.Text);
        }

        private void buttonRepostPostbyId_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Posts.repost(textboxAccessToken.Text, textboxGetPostById.Text);
        }

        private void buttonUnrepostById_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Posts.unrepost(textboxAccessToken.Text, textboxGetPostById.Text);
        }

        private void buttonStarById_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Posts.star(textboxAccessToken.Text, textboxGetPostById.Text);
        }

        private void buttonUnstarById_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Posts.unstar(textboxAccessToken.Text, textboxGetPostById.Text);
        }

        private void buttonGetMentionsOfUsername_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Posts.getMentionsOfUsername(textboxAccessToken.Text, textboxUsername.Text);
        }

        private void buttonGetGlobalStream_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Streams.getGlobalStream(textboxAccessToken.Text);
        }
    }
}
