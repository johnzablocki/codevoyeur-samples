#define GPS_EMULATOR

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
using System.Device.Location;

namespace HartfordLiveDemo
{
    public partial class MainPage : PhoneApplicationPage
    {

        private IGeoPositionWatcher<GeoCoordinateWatcher> _watcher;

        public MainPage()
        {
            InitializeComponent();
         
#if GPS_EMULATOR
            _watcher = new GpsEmulatorClient.GeoCoordinateWatcher()
                as IGeoPositionWatcher<GeoCoordinateWatcher>;
#else
            _watcher = new GeoCoordinateWatcher() 
                as IGeoPositionWatcher<GeoCoordinateWatcher>;
#endif
            _watcher.Start();
            _watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinateWatcher>>(_watcher_PositionChanged);


        }

        void _watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinateWatcher> e)
        {
            MessageBox.Show(e.Position.Location.ToString());
        }
        
    }
}