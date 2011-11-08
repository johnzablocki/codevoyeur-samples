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

namespace ParkingAssistant.ViewModels
{
    [DataContract]
    public class ParkingSpot
    {
        [DataMember]
        public double Long { get; set; }
        
        [DataMember]
        public double Lat { get; set; }        
    }
}
