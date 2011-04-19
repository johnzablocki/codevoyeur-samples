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
using Microsoft.Phone.Controls.Maps;
using System.Device.Location;

namespace WP7LocationLiveDemo {
    public partial class MainPage : PhoneApplicationPage {

        private IGeoPositionWatcher<GeoCoordinate> _watcher;
         
        // Constructor
        public MainPage() {
            InitializeComponent();
            map1.ZoomBarVisibility = Visibility.Visible;
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            map1.Mode = new AerialMode();
        }

        private void TrackButton_Click(object sender, RoutedEventArgs e) {
            if (null == _watcher) {
                _watcher = new GpsEmulatorClient.GeoCoordinateWatcher();
            }
            _watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(_watcher_PositionChanged);
            _watcher.Start();
        }

        void _watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e) {
            MessageBox.Show(string.Format("New Location Found: {0}, {1}",
                e.Position.Location.Longitude, e.Position.Location.Latitude));
        }

        private void PushpinButton_Click(object sender, RoutedEventArgs e) {
            Pushpin pushpin = new Pushpin();
            pushpin.Location = _watcher.Position.Location;
            pushpin.Content = _watcher.Position.Location.Latitude;
            map1.ZoomLevel = 15;
            map1.Center = _watcher.Position.Location;
            map1.Children.Add(pushpin);
        }
    }
}