using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Info;
using Microsoft.Phone.Tasks;
using System.Text;
using ParkingAssistant.ViewModels;

namespace ParkingAssistant
{
    public partial class MainPage : PhoneApplicationPage
    {
        private const int DEFAULT_ZOOM_LEVEL = 10;
        private const string HEROKU_APP_URL = "http://wp7location.herokuapp.com";

        private GeoCoordinateWatcher _geoCoordinateWatcher;
        private IsolatedStorageSettings _settings = IsolatedStorageSettings.ApplicationSettings;
        private readonly string _uniqueId;

        
        // Constructor
        public MainPage() {
            InitializeComponent();

            GeoCoordinate location;
            if (_settings.TryGetValue<GeoCoordinate>("Location", out location)) {
                setPushPin(location);
            }

            _uniqueId = getUniqueId();

        }

        private string getUniqueId()
        {
            Guid uniqueId;
            if (! _settings.TryGetValue<Guid>("UniqueId", out uniqueId))
            {
                _settings["UniqueId"] = (uniqueId = Guid.NewGuid());
                _settings.Save();
            }

            return uniqueId.ToString();
        }

       
        protected void ParkButton_Click(object sender, RoutedEventArgs e) {

            _geoCoordinateWatcher = new GeoCoordinateWatcher();
            _geoCoordinateWatcher.PositionChanged += _geoCoordinateWatcher_PositionChanged;
            _geoCoordinateWatcher.MovementThreshold = 20;
            _geoCoordinateWatcher.Start();
        }

        private void _geoCoordinateWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            MessageBox.Show(string.Format("New position found: {2} {0}, {1}", e.Position.Location.Longitude, e.Position.Location.Latitude, Environment.NewLine));
            saveLastLocation(e.Position.Location);
            setPushPin(e.Position.Location);
            logLocation(e.Position.Location);
            _geoCoordinateWatcher.Stop();
        }

        private void saveLastLocation(GeoCoordinate location)
        {
            _settings["Location"] = location;
            _settings.Save();
        }

        private void setPushPin(GeoCoordinate location) {
            var pushpin = new Pushpin() { Location = location };
            var button = new Button() { Content = "My Car (Click for Directions)" };
            pushpin.Content = button;

            button.Click += (s, re) => {
                var labledMapLocation = new LabeledMapLocation("My Car", location);
                var bingMapsDirectionsTask = new BingMapsDirectionsTask() { End = labledMapLocation };
                bingMapsDirectionsTask.Show();
            };

            ParkingMap.Children.Clear();
            ParkingMap.Children.Add(pushpin);
            ParkingMap.Center = location;
            ParkingMap.ZoomLevel = DEFAULT_ZOOM_LEVEL;
        }

        private void logLocation(GeoCoordinate location)
        {
            var postVars = string.Format("uid={0}&long={1}&lat={2}", _uniqueId, location.Longitude, location.Latitude);
            var request = WebRequest.CreateHttp(HEROKU_APP_URL);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            request.BeginGetRequestStream(ar =>
            {
                var requestStream = request.EndGetRequestStream(ar);
                using (var sw = new StreamWriter(requestStream))
                {                    
                    sw.Write(postVars);
                }

                request.BeginGetResponse(a =>
                {
                    var response = request.EndGetResponse(a);
                    
                }, null);

            }, null);

        }

        protected void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            getRecent();
        }

        private void getRecent()
        {
            var jsonParser = new DataContractJsonSerializer(typeof (ParkingSpot));
            var client = new WebClient();
            client.DownloadStringCompleted += (s, e) =>
                                                  {                                                      
                                                      HistoryTextBox.Text = e.Result.Replace("},{", Environment.NewLine);                                                      
                                                  };
            client.DownloadStringAsync(new Uri(HEROKU_APP_URL + "/" + _uniqueId));

        }

        
    }
}