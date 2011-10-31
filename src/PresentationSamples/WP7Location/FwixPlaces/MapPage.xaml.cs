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
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using FwixPlaces.Core;

namespace FwixPlaces {
    public partial class Map : PhoneApplicationPage {
        public Map() {
            InitializeComponent();

            App.MapPageViewModel.LoadData(PhoneApplicationService.Current.State["SelectedRestaurant"] as Place);            
            DataContext = App.MapPageViewModel;            
        }

        void button_Click(object sender, RoutedEventArgs e) {
            var task = new PhoneCallTask();
            task.PhoneNumber = App.MapPageViewModel.PhoneNumber;
            task.Show(); 
        }
    
    }
}
