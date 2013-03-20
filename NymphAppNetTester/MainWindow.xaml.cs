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
using AppNetDotNet.Model;
using AppNetDotNet.ApiCalls;

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

            this.textboxAccessToken.Text = Properties.Settings.Default.AccessToken;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           /* Authorization.serverSideFlow apnAuthProcess = new Authorization.serverSideFlow("8D2p3y9FgZWaKuVvkYnkNShS5KXuNB2m", "http://www.nymphicusapp.com/windows/appnet/serverSide", "basic stream write_post follow messages");
            apnAuthProcess.AuthSuccess += authProcess_AuthSuccess;
            apnAuthProcess.showAuthWindow();
            * */

            Authorization.clientSideFlow  apnAuthProcess = new Authorization.clientSideFlow("8D2p3y9FgZWaKuVvkYnkNShS5KXuNB2m", "http://www.nymphicusapp.com/windows/appnet/chapper/", "basic stream write_post follow messages files");
            apnAuthProcess.AuthSuccess += authProcess_AuthSuccess;
            apnAuthProcess.showAuthWindow();

            //AppNetDotNet.Model.Authorization.clientId = "8D2p3y9FgZWaKuVvkYnkNShS5KXuNB2m";
            //AppNetDotNet.Model.Authorization account = AppNetDotNet.Model.Authorization.AuthorizeNewAccount("http://www.nymphicusapp.com/windows/appnet/serverSide", "basic stream write_post follow messages files");
        }

        void authProcess_AuthSuccess(object sender, AuthorizationWindow.AuthEventArgs e)
        {
            if (e != null)
            {
                if (e.success)
                {
                    MessageBox.Show(e.accessToken, "Access token");
                    Tuple<Token,ApiCallResponse> token = Tokens.get(e.accessToken);
                    this.textboxAccessToken.Text = e.accessToken;
                }
                else
                {
                    MessageBox.Show(e.error, "Authorization failed");
                }
            }
        }

        private void buttonGetPersonalStream_Click_1(object sender, RoutedEventArgs e)
        {
            Tuple<List<Post>, ApiCallResponse> streamItems;
            ParametersMyStream parameters = new ParametersMyStream();
            parameters.count = 100;
            parameters.include_annotations = true;

            streamItems = AppNetDotNet.ApiCalls.SimpleStreams.getUserStream(textboxAccessToken.Text,parameters);
            Console.WriteLine(streamItems.ToString());
        }

        private void buttonWritePost_Click(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Posts.create(textboxAccessToken.Text, textboxPostText.Text);
        }

        private void buttonGetPostById_Click_1(object sender, RoutedEventArgs e)
        {
            Tuple<Post, ApiCallResponse> post;
           post = AppNetDotNet.ApiCalls.Posts.getById(textboxAccessToken.Text, textboxGetPostById.Text);
           Console.WriteLine(post.ToString());
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
            AppNetDotNet.ApiCalls.Posts.getMentionsOfUsernameOrId(textboxAccessToken.Text, textboxUsername.Text);
        }

        private void buttonGetGlobalStream_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.SimpleStreams.getGlobalStream(textboxAccessToken.Text);
        }

        private void buttonGetUserById_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Users.getUserByUsernameOrId (textboxAccessToken.Text, textboxUsername.Text);
        }

        private void buttonFollow_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Users.followByUsernameOrId(textboxAccessToken.Text, textboxUsername.Text);
        }

        private void buttonUnfollow_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Users.unfollowByUsernameOrId(textboxAccessToken.Text, textboxUsername.Text);
        }

        private void buttonGetFollowings_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Users.getFollowingsOfUser(textboxAccessToken.Text, textboxUsername.Text);
        }

        private void buttonGetFollowers_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Users.getFollowersOfUser(textboxAccessToken.Text, textboxUsername.Text);
        }

        private void buttonMute_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Users.mute(textboxAccessToken.Text, textboxUsername.Text);
        }

        private void buttonUnmute_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Users.unmute(textboxAccessToken.Text, textboxUsername.Text);
        }

        private void buttonGetMutedUsers_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Users.getMutedUsers(textboxAccessToken.Text);
        }

        private void buttonSearchUser_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Users.searchUsers(textboxAccessToken.Text, textboxSearchUser.Text);
        }

        private void buttonGetReposters_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Users.getRepostersOfPost(textboxAccessToken.Text, textboxGetPostById.Text);
        }

        private void buttonGetUsersWhoStarred_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Users.getUserWhoStarredAPost(textboxAccessToken.Text, textboxGetPostById.Text);
        }

        private void buttonGetToken_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Tokens.get(textboxAccessToken.Text);
        }

        private void buttonGetFilter_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Filters.getForCurrentUser(textboxAccessToken.Text);
        }

        private void buttonBrowserTest_Click_1(object sender, RoutedEventArgs e)
        {
            BrowserTest test = new BrowserTest();
            test.Show();
        }

        private void buttonStoreStreamMarker_Click_1(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.StreamMarkers.set(textboxAccessToken.Text, "global", textboxGetPostById.Text, 50);
        }

        private void buttonCreatePM_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textboxPmReceipients.Text))
            {
                string[] receipients = textboxPmReceipients.Text.Split(',');
                List<string> receipientsList = new List<string>(receipients);
                AppNetDotNet.ApiCalls.Messages.createPrivateMessage(textboxAccessToken.Text, textboxMessageText.Text, receipientsList);
            }

        }

        private void buttonChannelsOfLoogedinUser_Click(object sender, RoutedEventArgs e)
        {
            Tuple<List<Channel>,ApiCallResponse> response = AppNetDotNet.ApiCalls.Channels.Subscriptions.getOfCurrentUser(textboxAccessToken.Text);
            if (response.Item2.success)
            {
                if (response.Item1 != null)
                {
                    List<string> ids = new List<string>();
                    foreach (Channel channel in response.Item1)
                    {
                        if (!string.IsNullOrEmpty(channel.id))
                        {
                            ids.Add(channel.id);
                        }
                    }
                    textboxChannelIds.Text = string.Join(",", ids.ToArray());
                }
            }
        }

        private void buttonGetMessagesInChannel_Click(object sender, RoutedEventArgs e)
        {
            Tuple<List<Message>, ApiCallResponse> response = AppNetDotNet.ApiCalls.Messages.getMessagesInChannel(textboxAccessToken.Text, textboxChannelIds.Text);
        }

        private void button_openFilesWindow_Click(object sender, RoutedEventArgs e)
        {
            FileApiTests fileWindow = new FileApiTests(textboxAccessToken.Text);
            fileWindow.Show();
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.AccessToken = textboxAccessToken.Text;
            Properties.Settings.Default.Save();
        }

        private void button_openPlacesWindow_Click(object sender, RoutedEventArgs e)
        {
            NymphAppNetTester.Places window = new Places(textboxAccessToken.Text);
            window.Show();
        }
    }
}
