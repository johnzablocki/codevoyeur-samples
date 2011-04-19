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
using System.Device.Location;
using SimpleGeoPlaces.Models.Features;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using Microsoft.Phone.Controls.Maps;

namespace SimpleGeoPlaces.ViewModels {
    
    public class MapPageViewModel : ViewModelBase {

        private string _title;

        public string Title {
            get { return _title.ToLower(); }
            set {
                NotifyPropertyChanged("Title");
                _title = value; 
            }
        }

        private string _phoneNumber;

        public string PhoneNumber {
            get { return _phoneNumber; }
            set {
                NotifyPropertyChanged("PhoneNumber");
                _phoneNumber = value; 
            }
        }

        private string _address;

        public string Address {
            get { return _address; }
            set {
                NotifyPropertyChanged("Address");
                _address = value; 
            }
        }       

        private GeoCoordinate _center;

        public GeoCoordinate Center {
            get { return _center; }
            set {
                NotifyPropertyChanged("GeoCoordinate");
                _center = value; 
            }
        }

        private ObservableCollection<PushpinViewModel> _pushpins;

        public ObservableCollection<PushpinViewModel> Pushpins {
            get { return _pushpins; }
            set { _pushpins = value; }
        }
        

        public void LoadData(Feature feature) {
            _title = feature.Properties.Name;
            _phoneNumber = feature.Properties.Phone;
            _address = feature.Properties.Address;
            _center = new GeoCoordinate(feature.Geometry.Coordinates[1], feature.Geometry.Coordinates[0]);
            
            _pushpins = new ObservableCollection<PushpinViewModel>();
            _pushpins.Add(new PushpinViewModel() { 
                Location = Center, 
                PinText = _address + Environment.NewLine + _phoneNumber 
            });
        }

       
    }
}
