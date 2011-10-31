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
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Phone.Shell;
using FwixPlaces.State;
using FwixPlaces.Core;
using System.Collections.Generic;
using FwixClient.Constants;

namespace FwixPlaces.ViewModels
{

    public class MainPageViewModel : ViewModelBase
    {

        public event Action BeginDownload;
        public event Action EndDownload;

        private const string MISSING_PHONE_NUMBER = "+1";

        private ObservableCollection<string> _taxonomies;

        public ObservableCollection<string> Taxonomies
        {
            get
            {

                if (null == _taxonomies)
                {
                    _taxonomies = new ObservableCollection<string>() {
                        "vegetarian", "vegan", "raw", "natural", "organic"
                    };
                }

                return _taxonomies;
            }
        }

        private ObservableCollection<Place> _places = null;

        public ObservableCollection<Place> Places
        {
            set { _places = value; }
            get { return _places ?? (_places = new ObservableCollection<Place>()); }
        }      

        public void LoadData(string location, string taxonomy)
        {
            var client = new Client("50c870509300");

            var locationSplit = location.Split(',');
            var latitude = double.Parse(locationSplit[0]);
            var longitude = double.Parse(locationSplit[1]);

            if (null != BeginDownload)
            {
                BeginDownload();
            }
                
            client.GetByLocation(latitude, longitude, client_RequestCompleteEventHandler, new Dictionary<string, object> { { "query", taxonomy }, {"categories", CategoryConstants.FOOD_BEVERAGE }});
        }

        public void client_RequestCompleteEventHandler(IEnumerable<Place> places) {
            LocalStateContainer.Dispatcher.BeginInvoke(() => {
                    foreach (var place in places) {                        
                        Places.Add(place);
                    }
                    if (null != EndDownload) {
                        EndDownload();
                    }
                });
        }

    }
}
