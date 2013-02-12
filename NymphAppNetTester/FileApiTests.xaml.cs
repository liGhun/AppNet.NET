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
    /// Interaction logic for FileApiTests.xaml
    /// </summary>
    public partial class FileApiTests : Window
    {
        public string userToken { get; set; }
        System.Collections.ObjectModel.ObservableCollection<File> files;
        File currentFile = new File();

        public FileApiTests(string access_token)
        {
            InitializeComponent();
            userToken = access_token;
            files = new System.Collections.ObjectModel.ObservableCollection<File>();
            listView_files.ItemsSource = files;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            files.Clear();
            textblock_filename.Text = "";
            textblock_id.Text = "";
            textblock_kind.Text = "";
            currentFile = null;
            textblock_headerMetadata.Text = "";
            Tuple<List<File>,ApiCallResponse> receivedFiles = Files.getMyFiles(userToken);
            if (receivedFiles.Item2.success)
            {
                foreach (File file in receivedFiles.Item1)
                {
                    files.Add(file);
                }
            }

        }

        private void button_getFile_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                File file = button.CommandParameter as File;
                if (file != null)
                {
                    image_fullSize.Source = null;
                    Tuple<File,ApiCallResponse> fileContent = AppNetDotNet.ApiCalls.Files.getFile(userToken, file.id);
                    if (fileContent.Item2.success)
                    {
                        textblock_filename.Text = file.name;
                        textblock_id.Text = file.id;
                        textblock_kind.Text = file.kind;
                        textblock_headerMetadata.Text = "Metadata";
                        currentFile = file;
                        if (!string.IsNullOrEmpty(fileContent.Item1.mime_type))
                        {
                            if (fileContent.Item1.mime_type.StartsWith("image"))
                            {
                                BitmapImage bi = new BitmapImage();
                                try
                                {

                                    bi.BeginInit();
                                    bi.UriSource = new Uri(fileContent.Item1.url, UriKind.Absolute);
                                    bi.EndInit();
                                }
                                catch { return; }
                                image_fullSize.Source = bi;
                            }
                        }
                    }
                }
            }
        }

        private void button_uploadFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".*"; // Default file extension
            dlg.Filter = "All files (*.*)|*.*";

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                string filename = dlg.FileName;
                AppNetDotNet.ApiCalls.Files.create(userToken, dlg.FileName,type:textbox_type.Text);
            }
        }

        private void button_renameFile_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                File file = button.CommandParameter as File;
                if (file != null)
                {

                    Tuple<File, ApiCallResponse> fileContent = AppNetDotNet.ApiCalls.Files.update(userToken, file.id, name: textbox_textForChange.Text);
                    if (fileContent.Item2.success)
                    {
                        file = fileContent.Item1;
                        Button_Click_1(null, null);
                    }
                }
            }
        }

        private void button_deleteFile_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                File file = button.CommandParameter as File;
                if (file != null)
                {

                    Tuple<File, ApiCallResponse> fileContent = AppNetDotNet.ApiCalls.Files.delete(userToken, file.id);
                    if (fileContent.Item2.success)
                    {
                        Button_Click_1(null, null);
                    }
                }
            }
        }

        private void button_download_Click_1(object sender, RoutedEventArgs e)
        {
            if(currentFile != null) {
                if (!string.IsNullOrEmpty(currentFile.url))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(currentFile.url);
                    }
                    catch { }
                }
            }
        }

        private void button_uploadFileAndCreatePost_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".*"; // Default file extension
            dlg.Filter = "All files (*.*)|*.*";

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                string filename = dlg.FileName;
                Tuple<File,ApiCallResponse> uploadedFile = AppNetDotNet.ApiCalls.Files.create(userToken, dlg.FileName, type: textbox_type.Text);
                if (uploadedFile.Item2.success)
                {
                    List<File> files = new List<File>();
                    files.Add(uploadedFile.Item1);
                    Posts.create(userToken, textbox_postContent.Text, toBeEmbeddedFiles: files);
                }
            }
        }
    }
}
