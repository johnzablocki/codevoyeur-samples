using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using FwixPlaces.Core;
using Hammock;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FwixPlaces
{
    public class Client
    {
        public event Action<IEnumerable<Place>> RequestCompleteEventHandler;

        internal const string AUTHORITY = "http://geoapi.fwix.com/";

        private readonly string _apiKey;

        public Client(string apiKey)
        {
            _apiKey = apiKey;
        }       

        /// <summary>
        /// Get nearby places by postalcode
        /// </summary>
        /// <param name="postalCode"></param>
        /// <param name="options"></param>
        /// <param name="resultsCallback"></param>
        /// <returns></returns>
        public void GetByPostalCode(string postalCode, Action<IEnumerable<Place>> resultsCallback, Dictionary<string, object> options = null)
        {
            var path = string.Format("places.json?postal_code={0}{1}", postalCode, formatOptions(options));
            beginRequest(path, resultsCallback);

        }

         /// <summary>
        /// Get nearby places by neighborhood
        /// </summary>
        /// <param name="postalCode"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public void GetByNeighborhood(string province, string city, string locality, Action<IEnumerable<Place>> resultsCallback, Dictionary<string, object> options = null)
        {
            var path = string.Format("places.json?province={0}&city={1}{2}", province, city, formatOptions(options));
            beginRequest(path, resultsCallback);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="province"></param>
        /// <param name="resultsCallback"></param>
        /// <param name="options"></param>
        public void GetByAddress(string address, string city, string province, Action<IEnumerable<Place>> resultsCallback, Dictionary<string, object> options = null)
        {
            var path = string.Format("places.json?address={0}&city={1}&province={2}{3}", address, city, province, formatOptions(options));
            beginRequest(path, resultsCallback);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="resultsCallback"></param>
        /// <param name="options"></param>
        public void GetByLocation(double latitude, double longitude, Action<IEnumerable<Place>> resultsCallback, Dictionary<string, object> options = null)
        {
            var path = string.Format("places.json?lat={0}&lng={1}{2}", latitude, longitude, formatOptions(options));
            beginRequest(path, resultsCallback);
        }


        private void beginRequest(string path, Action<IEnumerable<Place>> resultsCallback = null)
        {

            var client = new RestClient() { Authority = AUTHORITY };
            var apiKeyParam = "&api_key=" + _apiKey;
            var request = new RestRequest() { Path = path + apiKeyParam };
            client.BeginRequest(request, requestComplete, resultsCallback);

        }

        private void requestComplete(RestRequest request, RestResponse response, object userState)
        {
             var places = response.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<PlaceCollection>(response.Content).Places.Distinct(new PlaceComparer())
                : new List<Place>();

            var callback = userState as Action<IEnumerable<Place>>;
            if (callback != null)
            {
                callback(places);
            } 
        }

        private string formatOptions(Dictionary<string, object> options)
        {
            if (options == null) return string.Empty;
            var sb = new StringBuilder();
            foreach (var key in options.Keys)
            {
                sb.AppendFormat("&{0}={1}", key, options[key]);
            }
            return sb.ToString().TrimEnd(',');
        }
    }
}
