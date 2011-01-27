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
    }
}
