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

namespace SimpleGeoPlaces {
    public partial class MainPage : PhoneApplicationPage {
        // Constructor
        public MainPage() {
            InitializeComponent();            
            DataContext = App.MainPageViewModel;
        }

        public void OnLoadedPivotItem(object sender, PivotItemEventArgs e) {
            
            //this feels wrong, I have some Binging to do...
            App.MainPageViewModel = new MainPageViewModel();
            App.MainPageViewModel.LoadData(TaxonomyMenu.SelectedItem.ToString());
            DataContext = App.MainPageViewModel;
        }
    }
}