using System;
using System.Collections.Generic;
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
using Microsoft.Phone.Controls.Maps.Core;
using Microsoft.Phone.Controls.Maps;
using System.Device.Location;

namespace WP7LocationQuickStart {
    public partial class MainPage : PhoneApplicationPage {

        private IGeoPositionWatcher<GeoCoordinate> _watcher;
        
        // Constructor
        public MainPage() {
            InitializeComponent();            

        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e) {
            SampleMap.ZoomLevel += 1;
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e) {
            SampleMap.ZoomLevel -= 1;
        }

        private void RoadButton_Click(object sender, RoutedEventArgs e) {
            SampleMap.Mode = new RoadMode();
        }

        private void AerialButton_Click(object sender, RoutedEventArgs e) {
            SampleMap.Mode = new AerialMode();
        }

        private void PushPinButton_Click(object sender, RoutedEventArgs e) {

            if (null == _watcher || _watcher.Status != GeoPositionStatus.Ready) {
                MessageBox.Show("Location not yet available");
                return;
            }
            
            var pin = new Pushpin();
            pin.Location = _watcher.Position.Location;
            pin.Content = string.Format("{0}\r\n{1}", pin.Location.Longitude, pin.Location.Latitude);
            SampleMap.Children.Add(pin);
        }

        private void LocationButton_Click(object sender, RoutedEventArgs e) {
            _watcher = new GpsEmulatorClient.GeoCoordinateWatcher();
            _watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(_watcher_PositionChanged);
            _watcher.Start();
        }

        void _watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e) {
            MessageBox.Show(string.Format("New location detected\r\n {0}, {1}", e.Position.Location.Latitude, e.Position.Location.Longitude));
            SampleMap.Center = e.Position.Location;
            SampleMap.ZoomLevel = 15;
        }
    }
}