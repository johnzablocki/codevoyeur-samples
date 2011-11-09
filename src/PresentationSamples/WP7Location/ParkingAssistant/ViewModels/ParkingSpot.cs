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
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace ParkingAssistant.ViewModels
{
    public class ParkingSpot
    {
        public IList<float> Location { get; set; }

        public DateTime Ts { get; set; }        
    }
}
