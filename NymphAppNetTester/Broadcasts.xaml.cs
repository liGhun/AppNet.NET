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

namespace NymphAppNetTester
{
    /// <summary>
    /// Interaction logic for Broadcasts.xaml
    /// </summary>
    public partial class Broadcasts : Window
    {
        public string accessToken { get; set; }

        public Broadcasts(string access_token)
        {
            InitializeComponent();
            accessToken = access_token;
        }

        private void button_send_broadcast_Click(object sender, RoutedEventArgs e)
        {
            AppNetDotNet.ApiCalls.Broadcasts.create_message(
                accessToken, 
                textbox_channel.Text, 
                textbox_subject.Text,  
                text:textbox_broadcast_body.Text,
                read_more_link:textbox_read_more_link.Text);
        }

        private void button_send_with_image_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "Images (*.png,*.jpg,*.gif,*.tif)|*.png;*.jpeg;*.jpg;*.gif;*.tif;*.tiff"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                string filename = dlg.FileName;
                Tuple<File, ApiCallResponse> uploadedFile = AppNetDotNet.ApiCalls.Files.create(accessToken, dlg.FileName, type: "de.li-ghun.image", mimetype:"image/" + System.IO.Path.GetExtension(dlg.FileName).TrimStart('.'));
                if (uploadedFile.Item2.success)
                {
                    List<File> files = new List<File>();
                    files.Add(uploadedFile.Item1);
                    AppNetDotNet.ApiCalls.Broadcasts.create_message(
                        accessToken,
                        textbox_channel.Text,
                        textbox_subject.Text,
                        text: textbox_broadcast_body.Text,
                        read_more_link: textbox_read_more_link.Text,
                        toBeEmbeddedFiles: files);
                }
            }
        }

        private void button_get_entries_Click(object sender, RoutedEventArgs e)
        {
            Tuple<List<Broadcast.Broadcast_Message>, ApiCallResponse> entries = AppNetDotNet.ApiCalls.Broadcasts.getMessagesInChannel(accessToken, textbox_channel.Text);
            if (entries.Item2.success)
            {
                listview_broadcast_entries.ItemsSource = entries.Item1;
            }
        }

        private void button_get_channels_Click(object sender, RoutedEventArgs e)
        {
            Tuple<List<Broadcast.Broadcast_Channel>, ApiCallResponse> channels = AppNetDotNet.ApiCalls.Broadcasts.getOfCurrentUser(accessToken);
            if (channels.Item2.success)
            {
                listview_broadcast_entries.ItemsSource = channels.Item1;
            }
        }

    }
}
