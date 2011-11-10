using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
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
using Microsoft.Phone.Info;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Text;
using ParkingAssistant.ViewModels;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace ParkingAssistant {
    public partial class MainPage : PhoneApplicationPage {
        private const int DEFAULT_ZOOM_LEVEL = 10;
        private const string HEROKU_APP_URL = "http://wp7location.herokuapp.com";

        private GeoCoordinateWatcher _geoCoordinateWatcher;
        private IsolatedStorageSettings _settings = IsolatedStorageSettings.ApplicationSettings;
        private GeoCoordinate _lastPosition;
        private readonly string _uniqueId;
        private const string MAP_CREDENTIALS = "At4Z1695E557xUQlOUnRA-hTlpwQ7croNwWWjpPekLJwOifpf1_FoEcBMlozuIiu";


        // Constructor
        public MainPage() {
            InitializeComponent();

            GeoCoordinate location;
            if (_settings.TryGetValue<GeoCoordinate>("Location", out location)) {
                setPushPin(location);
            }

            _uniqueId = getUniqueId();            
        }

        private string getUniqueId() {
            Guid uniqueId;
            if (!_settings.TryGetValue<Guid>("UniqueId", out uniqueId)) {
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

        private void _geoCoordinateWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e) {
            MessageBox.Show(string.Format("New position found: {2} {0}, {1}", e.Position.Location.Latitude, e.Position.Location.Longitude, Environment.NewLine));
            saveLastLocation(e.Position.Location);
            setPushPin(e.Position.Location);
            logLocation(e.Position.Location);
            _geoCoordinateWatcher.Stop();
            _lastPosition = e.Position.Location;
        }

        private void saveLastLocation(GeoCoordinate location) {
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

        private void logLocation(GeoCoordinate location) {
            var postVars = string.Format("uid={0}&long={1}&lat={2}", _uniqueId, location.Longitude, location.Latitude);
            var request = WebRequest.CreateHttp(HEROKU_APP_URL);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            request.BeginGetRequestStream(ar => {  
                var requestStream = request.EndGetRequestStream(ar);
                using (var sw = new StreamWriter(requestStream)) {
                    sw.Write(postVars);
                }

                request.BeginGetResponse(a => {
                    var response = request.EndGetResponse(a);

                }, null);

            }, null);
                
        }

        protected void HistoryButton_Click(object sender, RoutedEventArgs e) {
           getRecent();
        }

        protected void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var parkingSpot = e.AddedItems[0] as ParkingSpot;
            var url = "http://dev.virtualearth.net/REST/v1/Locations/" + parkingSpot.Location[0] + "," + parkingSpot.Location[1] + "?key=" + MAP_CREDENTIALS;
            var client = new WebClient();
            client.DownloadStringCompleted += (s, ea) =>
                                                  {
                                                      var obj = JsonConvert.DeserializeObject(ea.Result) as JObject;
                                                      MessageBox.Show(obj.ToString());
                                                  };
            client.DownloadStringAsync(new Uri(url));
        }

        protected void LiveTilesToggle_Checked(object sender, RoutedEventArgs e) {
            
            var firstTile = ShellTile.ActiveTiles.First();
            var checkBox = sender as CheckBox;

            if (checkBox.IsChecked.HasValue && checkBox.IsChecked.Value == true)
            {
                _lastPosition = _lastPosition ?? new GeoCoordinate();
                var tileData = new StandardTileData()
                                   {
                                       BackTitle = "Current Location",
                                       BackContent = _lastPosition.Latitude + ", " + _lastPosition.Longitude
                                       
                                   };
                firstTile.Update(tileData);
            }

        }

        public ObservableCollection<ParkingSpot> Items { get; set; }

        private void getRecent() {  
            var client = new WebClient();
            client.DownloadStringCompleted += (s, e) => {
                                                      var results = JsonConvert.DeserializeObject<IList<ParkingSpot>>(e.Result);
                                                      Items = new ObservableCollection<ParkingSpot>(results.Take(10));
                                                      ItemsList.DataContext = Items;                                                      
                                                  };
            client.DownloadStringAsync(new Uri(HEROKU_APP_URL + "/" + _uniqueId));

        }

        private string formatResults(IList<ParkingSpot> results) {

            var sb = new StringBuilder();
            foreach (var result in results) {
                sb.AppendFormat("{0}", result.Location[0]);
            }
            return sb.ToString();
        }


    }
}