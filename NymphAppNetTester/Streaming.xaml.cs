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
using AppNetDotNet.Model;
using AppNetDotNet.ApiCalls;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace NymphAppNetTester
{
    /// <summary>
    /// Interaction logic for Streaming.xaml
    /// </summary>
    public partial class StreamingTest : Window
    {
        public string access_token { get; set; }
        ObservableCollection<string> rawJsonStrings { get; set; }

        AppNetDotNet.ApiCalls.Streaming.UserStream userStream;

        public StreamingTest(string access_token)
        {
            InitializeComponent();
            rawJsonStrings = new ObservableCollection<string>();
            listview_items.ItemsSource = rawJsonStrings;
            this.access_token = access_token;
            updates = new List<string>();
        }

        private void buttonUserStream_Click(object sender, RoutedEventArgs e)
        {
            if (userStream == null)
            {
                userStream = new Streaming.UserStream(access_token);
                IAsyncResult asyncResult = userStream.StartUserStream(
                    followersCallback: followersCallback,
                    unifiedCallback: unifiedCallback,
                    streamCallback: streamCallback,
                    channelsCallback: channelsCallback,
                    rawJsonCallback:rawJsonCallback);
                userStream.subscribe_to_endpoint(userStream.available_endpoints["Stream"]);
              //  userStream.subscribe_to_endpoint(userStream.available_endpoints["Unified"]);
              //  userStream.subscribe_to_endpoint(userStream.available_endpoints["Channels"]);
              //  userStream.subscribe_to_endpoint(userStream.available_endpoints["Followers"]);
                buttonUserStream.Content = "Stop user stream";
            }
            else
            {
                userStream.stopUserStream();
                userStream = null;
                buttonUserStream.Content = "Start user stream";
            }
        }

        public List<string> updates { get; set; }

        public void rawJsonCallback(string json)
        {
            if (json != null)
            {
                updates.Add(json);
                this.Dispatcher.Invoke(new Action(add_string));
            }
        }

        public void followersCallback(List<User> users, bool is_deleted = false)
        {
            if (users != null)
            {
                foreach (User user in users)
                {
                    if (!is_deleted)
                    {
                        updates.Add("New follower: " + user.ToString());
                    }
                    else
                    {
                        updates.Add("Unfollowed by: " + user.ToString());
                    }
                    this.Dispatcher.Invoke(new Action(add_string));
                }
            }
        }

        public void unifiedCallback(List<Post> posts, bool is_deleted = false)
        {
            if (posts != null)
            {
                foreach (Post post in posts)
                {
                    updates.Add(post.ToString());
                    this.Dispatcher.Invoke(new Action(add_string));
                }
            }
        }

        public void streamCallback(List<Post> posts, bool is_deleted = false)
        {
            if (posts != null)
            {
                foreach (Post post in posts)
                {
                    updates.Add(post.ToString());
                    this.Dispatcher.Invoke(new Action(add_string));
                }
            }
        }

        public void channelsCallback(List<Message> messages, bool is_deleted = false)
        {
            if (messages != null)
            {
                foreach (Message message in messages)
                {
                    updates.Add(message.ToString());
                    this.Dispatcher.Invoke(new Action(add_string));
                }
            }
        }

        public void add_string() {
            List<string> to_be_removed = new List<string>();
            foreach (string json in updates)
            {
                to_be_removed.Add(json);
            }
            foreach(string json in updates) {
                rawJsonStrings.Add(json);
            }
            foreach (string delete in to_be_removed)
            {
                updates.Remove(delete);
            }
       }
    }
}
