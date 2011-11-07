using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO.IsolatedStorage;
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
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Tasks;

namespace ParkingAssistant
{
    public partial class MainPage : PhoneApplicationPage
    {
        private const int DEFAULT_ZOOM_LEVEL = 10;

        private GeoCoordinateWatcher _geoCoordinateWatcher = new GeoCoordinateWatcher();
        private IsolatedStorageSettings _settings = IsolatedStorageSettings.ApplicationSettings;
        
        // Constructor
        public MainPage() {
            InitializeComponent();

            GeoCoordinate location;
            if (_settings.TryGetValue<GeoCoordinate>("Location", out location)) {
                setPushPin(location);
            }
        }

        protected void ParkButton_Click(object sender, RoutedEventArgs e) {

            _geoCoordinateWatcher.PositionChanged += _geoCoordinateWatcher_PositionChanged;
            _geoCoordinateWatcher.Start();

        }

        private void _geoCoordinateWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e) {
            _settings["Location"] = e.Position.Location;
            setPushPin(e.Position.Location);
            _geoCoordinateWatcher.Stop();            
        }

        private void setPushPin(GeoCoordinate location) {
            var pushpin = new Pushpin() { Location = location };
            var button = new Button() { Content = "My Car (Click for Directions)" };
            pushpin.Content = button;

            button.Click += (s, re) => {
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
    }
}