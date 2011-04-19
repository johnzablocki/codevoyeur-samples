using System;
using System.Collections.Generic;
using System.Text;
using Castle.ActiveRecord;
using NHibernate.Expression;
using Castle.ActiveRecord.Queries;
using System.Collections;
using LocationSearchWithActiveRecord.Utils;

namespace LocationSearchWithActiveRecord
{
    [Serializable, ActiveRecord("locations")]
    public class Location : ActiveRecordBase<Location>
    {

        #region Constants
        //Equatorial radius of the earth from WGS 84 in meters, semi major axis = a
        internal static int a = 6378137;
        //flattening = 1/298.257223563 = 0.0033528106647474805
        //first eccentricity squared = e = (2-flattening)*flattening
        internal static double e = 0.0066943799901413165;
        //Miles to Meters conversion factor (take inverse for opposite)
        internal static double milesToMeters = 1609.347;
        //Degrees to Radians converstion factor (take inverse for opposite)
        internal static double degreesToRadians = Math.PI / 180; 
        #endregion

        [PrimaryKey(PrimaryKeyType.Identity, "location_id")]
        public int Id { get; set; }

        [Property("city")]
        public string City { get; set; }

        [Property("state")]
        public string State { get; set; }

        [Property("zip")]
        public string Zip { get; set; }

        [Property("latitude")]
        public double Latitude { get; set; }

        [Property("longitude")]
        public double Longitude { get; set; }

        public static Location[] FindAllOrderdByCity()
        {
            return FindAll(new Order("City", true));
        }

        public static Location FindByCityAndState(string city, string state)
        {
            return FindFirst(new EqExpression("City", city, true),
                             new EqExpression("State", state, true));
        }

        public static  Location FindByZip(string zip)
        {
            return FindFirst(new EqExpression("Zip", zip));
        }
        
        public static Location[] FindNearbyLocations(Location centroidLocation, double searchRadius)
        {
            searchRadius = searchRadius * milesToMeters;

            //lat naught and lon naught are the geodetic parameters in radians
            double lat0 = Convert.ToDouble(centroidLocation.Latitude) * degreesToRadians;
            double lon0 = Convert.ToDouble(centroidLocation.Longitude) * degreesToRadians;

            //Find reference ellipsoid radii
            double Rm = lat0.CalcMeridionalRadiusOfCurvature();
            double Rpv = lat0.CalcRoCinPrimeVertical();

            //Throw exception if search radius is greater than 1/4 of globe and thus breaks accuracy of model (mostly pertinent for russia, alaska, canada, peru, etc.)
            if (Rpv * Math.Cos(lat0) * Math.PI / 2 < searchRadius)
            {
                throw new ApplicationException("Search radius was too great to achieve an accurate result with this model.");
            }

            //Find boundaries for curvilinear plane that encloses search ellipse
            double latMax = (searchRadius / Rm + lat0) / degreesToRadians;
            double lonMax = (searchRadius / (Rpv * Math.Cos(lat0)) + lon0) / degreesToRadians;
            double latMin = (lat0 - searchRadius / Rm) / degreesToRadians;
            double lonMin = (lon0 - searchRadius / (Rpv * Math.Cos(lat0))) / degreesToRadians;

            //Return postal codes in curvilinear plane
            Location[] nearbyLocations = Location.FindAll
                (
                    Expression.Gt("Latitude", latMin),
                    Expression.Lt("Latitude", latMax),
                    Expression.Gt("Longitude", lonMin),
                    Expression.Lt("Longitude", lonMax)
                );              

            //Now calc distances from centroid, and remove results that were returned in the curvilinear plane, but are outside of the ellipsoid geodetic
            Dictionary<string, Location> distinctLocations = new Dictionary<string, Location>();
            for (int i = 0; i < nearbyLocations.Length; i++)
            {
                string key = nearbyLocations[i].City + "::" + nearbyLocations[i].State;
                if (!distinctLocations.ContainsKey(key))
                {
                    if (!(Rm.CalcDistanceLatLons(Rpv, lat0, lon0,
                            Convert.ToDouble(nearbyLocations[i].Latitude) * degreesToRadians,
                            Convert.ToDouble(nearbyLocations[i].Longitude) * degreesToRadians) > searchRadius))
                    {
                        distinctLocations.Add(key, nearbyLocations[i]);
                    }
                }

            }

            nearbyLocations = new Location[distinctLocations.Count];
            distinctLocations.Values.CopyTo(nearbyLocations, 0);
            return nearbyLocations;
        }

    }
}
