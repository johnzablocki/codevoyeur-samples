using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hammock.Authentication.OAuth;
using Hammock;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SimpleGeoWebForms.Models.Features;
using System.Net;

namespace SimpleGeoWebForms.Models {

    public class Client : RestClient {

        internal const string AUTHORITY = "http://api.simplegeo.com";
        internal const string VERSIONPATH = "1.0";

        private readonly OAuthCredentials _credentials = null;

        public Client(string oAuthKey, string oAuthSecret) {

            _credentials = new OAuthCredentials() {
                Type = OAuthType.RequestToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                ConsumerKey = oAuthKey,
                ConsumerSecret = oAuthSecret
            };

            Authority = AUTHORITY;
            VersionPath = VERSIONPATH;
            Credentials = _credentials;

        }

        //TODO: allow multiple category and query search
        public FeatureCollection GetNearbyPlaces(double latitude, double longitude, string query = null, string category = null, int radius = 25) {

            var path = string.Format("places/{0},{1}.json", latitude, longitude);

            //TODO: less ternary operator!
            path += !string.IsNullOrEmpty(query) ? "?q=" + query : "";
            path += !string.IsNullOrEmpty(category) ?
                (string.IsNullOrEmpty(query) ? "?" : "&") + "category=" + category : "";
            path += string.IsNullOrEmpty(path) ? "?" : "&" + "radius=" + radius;

            var request = new RestRequest() { Path = path };
            var response = this.Request(request);

            if (response.StatusCode == HttpStatusCode.OK) {
                var jObj = JObject.Parse(response.Content);
                return JsonConvert.DeserializeObject<FeatureCollection>(response.Content);
            } else {
                //TODO: raise intelligent exceptions
                return new FeatureCollection() { Features = new List<Feature>() };
            }

        }

        public string GetContext(double latitude, double longitude) {

            var path = string.Format("context/{0},{1}.json", latitude, longitude);

            var request = new RestRequest() { Path = path };
            var response = this.Request(request);

            if (response.StatusCode == HttpStatusCode.OK) {
                var jObj = JObject.Parse(response.Content);
                //return JsonConvert.DeserializeObject<FeatureCollection>(response.Content);
                return response.Content;
            } else {
                //TODO: raise intelligent exceptions
                return "";
            }

        }
    }
}
