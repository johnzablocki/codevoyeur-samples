using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Shell;
using SimpleGeoPlaces.Models.Features;
using System.Collections.ObjectModel;
using SimpleGeoPlaces.Models;
using SimpleGeoPlaces.State;
using System.Device.Location;

namespace SimpleGeoPlaces.ViewModels {

    public class SearchResultsPageViewModel : ViewModelBase {

        public SearchResultsPageViewModel() {
            LoadData();
        }

        public static string CurrentTaxonomy {
            get { return PhoneApplicationService.Current.State["CurrentTaxonomy"] as string; }
        }

        private ObservableCollection<Feature> _features = null;

        public ObservableCollection<Feature> Features {
            set { _features = value; }
            get {
                if (null == _features) {
                    _features = new ObservableCollection<Feature>();
                }
                return _features;
            }
        }

        public void client_RequestCompleteEventHandler(FeatureCollection obj) {
            LocalStateContainer.Dispatcher.BeginInvoke(() => {
                    foreach (var feature in obj.Features) {
                        Features.Add(feature);
                    }
                });
        }

        public void LoadData() {
            Client client = new Client("vvc2y7nAjkx6fUaJqQ94FT7nAdZCWQrA", "CJYFj8Sy3WwDL2sFfQXJnDdyXh7BqDU2");
            client.RequestCompleteEventHandler += new Action<FeatureCollection>(client_RequestCompleteEventHandler);                        
            client.GetNearbyPlaces(37.7645, -122.4294, CurrentTaxonomy, "restaurant");
        }

    }
}
