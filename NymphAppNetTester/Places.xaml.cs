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
using System.Collections.ObjectModel;

namespace NymphAppNetTester
{
    /// <summary>
    /// Interaction logic for Places.xaml
    /// </summary>
    public partial class Places : Window
    {
        public string access_token { get; set; }
        public ObservableCollection<Place> places { get; set; }

        public Places(string accessToken)
        {
            InitializeComponent();
            places = new ObservableCollection<Place>();
            listbox_places.ItemsSource = places;
            this.access_token = accessToken;
        }

        private void button_search_Click(object sender, RoutedEventArgs e)
        {
            places.Clear();
            Tuple<Place,ApiCallResponse> result = AppNetDotNet.ApiCalls.Places.get(access_token, textbox_factual_id.Text);
            if (result.Item2.success)
            {
                places.Add(result.Item1);
            }
        }

        private void button_search1_Click(object sender, RoutedEventArgs e)
        {
            places.Clear();
            // the parameters are not added here automatically - for testing please be aware of adding them by using the textboxes
            // also the paramters are not checked (decimal e.g.) - this is just a test app
            // and finally: be aware that decimal are written with , in Germany and with . in the US - the test app uses your local settings!
            Tuple<List<Place>, ApiCallResponse> result = AppNetDotNet.ApiCalls.Places.search(access_token, Convert.ToDecimal(textbox_latitude.Text), Convert.ToDecimal(textbox_longitude.Text),radius:Convert.ToDecimal(textbox_radius.Text),count:Convert.ToInt32(textbox_count.Text), query:textbox_query.Text);
            if (result.Item2.success && result.Item1 != null)
            {
                foreach (Place place in result.Item1)
                {
                    places.Add(place);
                }
            }
        }
    }
}
