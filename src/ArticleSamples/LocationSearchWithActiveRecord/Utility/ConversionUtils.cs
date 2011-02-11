using System;
using System.Collections.Generic;
using System.Text;

namespace LocationSearchWithActiveRecord.Utils
{
    internal static class ConversionUtils
    {
        //Equatorial radius of the earth from WGS 84 in meters, semi major axis = a
        internal const int a = 6378137;
        //flattening = 1/298.257223563 = 0.0033528106647474805
        //first eccentricity squared = e = (2-flattening)*flattening
        internal const double e = 0.0066943799901413165;


        /// <summary>
        /// Calculates the Radius of curvature in the prime vertical for the reference ellipsoid
        /// </summary>
        /// <remarks>
        /// This is the vector that defines the normal surface to any point on the ellipsoid.  It extends from
        /// from the polar axis to that point.  It is used for the longitude, in differentiation of east distances, dE
        /// </remarks>
        /// <param name="lat0">Geodetic latitude in radians</param>
        /// <returns>Length of radius of curvature in the prime vertical</returns>
        public static double CalcRoCinPrimeVertical(this double lat0)
        {
            double Rn = a / Math.Sqrt(1 - e * Math.Pow(Math.Sin(lat0), 2));
            return Rn;
        }

        /// <summary>
        /// Calculates the meridional radius of curvature for the reference ellipsoid
        /// </summary>
        /// <remarks>
        /// This is the radius of a circle that fits the earth curvature in the meridian at the latitude chosen.
        /// It is used for latitude, in differentiation of north distances, dN
        /// </remarks>
        /// <param name="lat0">Geodetic latitude in radians</param>
        /// <returns>Length of meridional radius of curvature</returns>
        public static double CalcMeridionalRadiusOfCurvature(this double lat0)
        {
            double Rm = a * (1 - e) / Math.Pow(1 - e * (Math.Pow(Math.Sin(lat0), 2)), 1.5);
            return Rm;
        }

        /// <summary>
        /// Calculates the true birds-eye view length of the arc between two positions on the globe using parameters from WGS 84,
        /// used in aviation and GPS.
        /// </summary>
        /// <remarks>
        /// An accurate BIRDS EYE numerical approximation with error approaching less than 10 feet at a 50 miles search radius
        /// and only 60 ft at 400 mile search radius.  Error is on the order of (searchRadius/equatorialRadius)^2.  Only accurate for distances
        /// less than 1/4 of the globe (~10,000 km at the equator, and approaching 0 km at the poles).
        /// Geoid height above sea level is assumed to be zero, which is the only deviation from GPS, and another source of error.
        /// </remarks>
        /// <param name="Rm">Meridional Radius of Curvature at the centroid latitude</param>
        /// <param name="Rpv">Radius of Curvature in the Prime Vertical at the centroid latitude</param>
        /// <param name="lat0">Centroid latitude</param>
        /// <param name="lon0">Centroid longitude</param>
        /// <param name="lat">Destination latitude</param>
        /// <param name="lon">Destination longitude</param>
        /// <returns>Distance in meters from the arc between the two points on the globe</returns>
        public static double CalcDistanceLatLons(this double Rm, double Rpv, double lat0, double lon0, double lat, double lon)
        {
            double distance = Math.Sqrt(Math.Pow(Rm, 2) * Math.Pow(lat - lat0, 2) + Math.Pow(Rpv, 2) * Math.Pow(Math.Cos(lat0), 2) * Math.Pow(lon - lon0, 2));
            return distance;
        }
    }
}
