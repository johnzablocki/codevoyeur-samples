using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FwixPlaces.Core
{
    public class PlaceComparer : IEqualityComparer<Place>
    {
        #region IEqualityComparer<Place> Members

        public bool Equals(Place x, Place y)
        {
            return (x.Name == y.Name && x.Address == y.Address && x.Postal_Code == y.Postal_Code);
        }

        public int GetHashCode(Place obj)
        {
            return obj.Name.GetHashCode();
        }

        #endregion
    }
}
