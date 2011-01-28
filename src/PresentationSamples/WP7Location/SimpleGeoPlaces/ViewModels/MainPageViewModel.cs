﻿using System;
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
using SimpleGeoPlaces.Models.Features;
using SimpleGeoPlaces.State;
using SimpleGeoPlaces.Models;

namespace SimpleGeoPlaces.ViewModels {
    public class MainPageViewModel : ViewModelBase {

        private ObservableCollection<string> _taxonomies;

        public ObservableCollection<string> Taxonomies {
            get {

                if (null == _taxonomies) {
                    _taxonomies = new ObservableCollection<string>() {
                        "Vegetarian", "Vegan", "Raw", "Natural", "Organic"
                    };
                }

                return _taxonomies;
            }
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

        public void LoadData(string taxonomy) {
            Client client = new Client("vvc2y7nAjkx6fUaJqQ94FT7nAdZCWQrA", "CJYFj8Sy3WwDL2sFfQXJnDdyXh7BqDU2");
            client.RequestCompleteEventHandler += new Action<FeatureCollection>(client_RequestCompleteEventHandler);
            client.GetNearbyPlaces(40.672528, -73.983506, taxonomy, "restaurant", 300);
        }

    }
}
