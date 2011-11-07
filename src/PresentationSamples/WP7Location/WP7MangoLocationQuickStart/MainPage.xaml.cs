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
using Microsoft.Phone.Controls.Maps.Platform;

namespace WP7MangoLocationQuickStart {
    public partial class MainPage : PhoneApplicationPage {

        private bool _gpsIsReady = false;
        private Location _currentLocation = null;
        private GeoCoordinateWatcher _geoCoordinateWatcher = null;

        public MainPage() {
            InitializeComponent();
        }

        private void ModeButton_Click(object sender, RoutedEventArgs e) {
            if (DemoMap.Mode is RoadMode) {
                DemoMap.Mode = new AerialMode();
            } else {
                DemoMap.Mode = new RoadMode();
            }            
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e) {
            DemoMap.ZoomLevel += 1;
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e) {
            DemoMap.ZoomLevel -= 1;
        }

        private void PinButton_Click(object sender, RoutedEventArgs e) {
            if (_currentLocation != null) {
                DemoMap.Children.Add(new Pushpin() { Location = _currentLocation });
            } else {
                MessageBox.Show("Device not ready");
            }
        }

        private void CenterButton_Click(object sender, RoutedEventArgs e) {
            if (_gpsIsReady && _currentLocation != null) {
                DemoMap.Center = _currentLocation;
            } else {
                MessageBox.Show("Device not ready");
            }
        }

        private void GPSButton_Click(object sender, RoutedEventArgs e) {
            _geoCoordinateWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default) {                
                MovementThreshold = 20
            };
            _geoCoordinateWatcher.Start();

            _geoCoordinateWatcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(geoCoordinateWatcher_PositionChanged);
            _geoCoordinateWatcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(geoCoordinateWatcher_StatusChanged);            
        }

        void geoCoordinateWatcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e) {
            if (e.Status == GeoPositionStatus.Ready) {
                _gpsIsReady = true;
            }
        }

        void geoCoordinateWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e) {
            if (_gpsIsReady) {
                _currentLocation = e.Position.Location;
                _geoCoordinateWatcher.Stop();
                MessageBox.Show(string.Format("New position found: {0}, {1}", e.Position.Location.Latitude, e.Position.Location.Longitude));
            }
        }

    }
}