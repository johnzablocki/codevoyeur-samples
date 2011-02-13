/////////////////////////////////////////////////////////////////////////////////////////
// define GPS EMULATOR when working with Windows Phone GPS Emulator to simulate location 
#define GPS_EMULATOR
////////////////////////////////////////////////////////////////////////////////////////

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
using Microsoft.Phone.Shell;
using SimpleGeoPlaces.ViewModels;
using System.Device.Location;
using System.IO.IsolatedStorage;
using System.Text;

namespace SimpleGeoPlaces {
    public partial class MainPage : PhoneApplicationPage {

        private IGeoPositionWatcher<GeoCoordinate> _watcher;
        private const string LOCATION_FILE = "location.dat";
        
        
        // Constructor
        public MainPage() {
            InitializeComponent();            
            DataContext = App.MainPageViewModel;

#if GPS_EMULATOR
            _watcher = new GpsEmulatorClient.GeoCoordinateWatcher();            
#else
            _Watcher = new System.Device.Location.GeoCoordinateWatcher();            
            (_watcher as GeoCoordinateWatcher).MovementThreshold = 20;
#endif
            _watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            _watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(_watcher_StatusChanged);
        }

        void _watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e) {
            
        }

        public void OnLoadedPivotItem(object sender, PivotItemEventArgs e) {

            
            //TODO: this should obviously not be in the code behind
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication()) {

                //HACK: default to BK
                var location = "40.672528,-73.983506";
                if (isoStore.FileExists(LOCATION_FILE)) {
                    var file = isoStore.OpenFile(LOCATION_FILE, System.IO.FileMode.Open);   
                    
                    var bytesRead = new byte[file.Length];
                    file.Read(bytesRead, 0, (int)file.Length);
                    location = UnicodeEncoding.Unicode.GetString(bytesRead, 0, (int)file.Length);
                }

                //this feels wrong, I have some Binging to do...
                App.MainPageViewModel = new MainPageViewModel();
                App.MainPageViewModel.LoadData(location, TaxonomyMenu.SelectedItem.ToString());
                DataContext = App.MainPageViewModel;
            }

        }
        
        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e) {

            SearchProgress.Visibility = Visibility.Visible;

            //TODO: this also should obviously not be in the code behind
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication()) {                
                if (isoStore.FileExists(LOCATION_FILE)) {
                    isoStore.DeleteFile(LOCATION_FILE);
                }
                var file = isoStore.CreateFile(LOCATION_FILE);
                string location = string.Format("{0},{1}", e.Position.Location.Latitude, e.Position.Location.Longitude);
                var bytesToWrite = Encoding.Unicode.GetBytes(location);
                file.Write(bytesToWrite, 0, bytesToWrite.Length);
            }

            SearchProgress.Visibility = Visibility.Collapsed;
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e) {
            _watcher.Start();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            PhoneApplicationService.Current.State["SelectedRestaurant"] = e.AddedItems[0];

            NavigationService.Navigate(new Uri("/MapPage.xaml", UriKind.Relative));
        }
    }
}