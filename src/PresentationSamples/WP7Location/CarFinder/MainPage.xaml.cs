using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Controls.Maps.Platform;
using System.Device.Location;
using Microsoft.Phone.Controls.Maps;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Info;

namespace CarFinder
{
    public partial class MainPage : PhoneApplicationPage
    {

        private const int DEFAULT_ZOOM_LEVEL = 10;

        private GeoCoordinateWatcher _geoCoordinateWatcher = new GeoCoordinateWatcher();
        private IsolatedStorageSettings _settings = IsolatedStorageSettings.ApplicationSettings;
        private string _deviceId = null;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            GeoCoordinate location;
            if (_settings.TryGetValue<GeoCoordinate>("Location", out location))
            {
                setPushPin(location);
            }

            _deviceId = DeviceExtendedProperties.GetValue("DeviceUniqueId").ToString();
        }

        protected void ParkButton_Click(object sender, RoutedEventArgs e)
        {

            _geoCoordinateWatcher.PositionChanged += _geoCoordinateWatcher_PositionChanged;
            _geoCoordinateWatcher.Start();

        }

        private void _geoCoordinateWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            _settings["Location"] = e.Position.Location;
            logLocation(e.Position.Location);
            setPushPin(e.Position.Location);
            _geoCoordinateWatcher.Stop();
        }

        private void setPushPin(GeoCoordinate location)
        {
            var pushpin = new Pushpin() { Location = location };
            var button = new Button() { Content = "My Car (Click for Directions)" };
            pushpin.Content = button;

            button.Click += (s, re) =>
            {
                var labledMapLocation = new LabeledMapLocation("Current", location);
                var bingMapsDirectionsTask = new BingMapsDirectionsTask();
                bingMapsDirectionsTask.End = labledMapLocation;
                bingMapsDirectionsTask.Show();
            };

            ParkingMap.Children.Clear();
            ParkingMap.Children.Add(pushpin);
            ParkingMap.Center = location;
            ParkingMap.ZoomLevel = DEFAULT_ZOOM_LEVEL;
        }

        private void logLocation(GeoCoordinate location)
        {
            var request = WebRequest.CreateHttp("http://localhost:3000");
            request.Method = "POST";
            request.ContentType = "text/json";
            request.BeginGetRequestStream(ar =>
            {
                var requestStream = request.EndGetRequestStream(ar);
                using (var sw = new StreamWriter(requestStream))
                {                    
                    sw.Write("");
                }

                //request.BeginGetResponse(a =>
                //{
                //    var response = request.EndGetResponse(a);
                //    var responseStream = response.GetResponseStream();
                //    using (var sr = new StreamReader(responseStream())
                //    {
                //        // Parse the response message here
                //    }

                //}, null);

            }, null);

        }
    }
}