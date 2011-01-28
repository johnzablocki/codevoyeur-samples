using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hammock.Authentication.OAuth;
using Hammock;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SimpleGeoPlaces.Models.Features;
using System.Net;

namespace SimpleGeoPlaces.Models {

    public class Client : RestClient {

        internal const string AUTHORITY = "http://api.simplegeo.com";
        internal const string VERSIONPATH = "1.0";

        private readonly OAuthCredentials _credentials = null;
        public event Action<FeatureCollection> RequestCompleteEventHandler;

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

        public void GetNearbyPlaces(double latitude, double longitude, string query = null, string category = null) { 

            var path = string.Format("places/{0},{1}.json", latitude, longitude);
            
            //TODO: less ternary operator!
            path += !string.IsNullOrEmpty(query) ? "?q=" + query : "";
            path += !string.IsNullOrEmpty(category) ? 
                string.IsNullOrEmpty(query) ? "?" : "&" + "category=" + category: "";            

            var request = new RestRequest() { Path = path };            
            BeginRequest(request, RequestComplete);
        }

        public void RequestComplete(RestRequest request, RestResponse response, object userSate) {
            
            FeatureCollection fc = null;
            if (response.StatusCode == HttpStatusCode.OK) {
                var jObj = JObject.Parse(response.Content);
                fc = JsonConvert.DeserializeObject<FeatureCollection>(response.Content);
            } else {
                //TODO: raise intelligent exceptions
                fc = new FeatureCollection() { Features = new List<Feature>() };
            }
            if (null != RequestCompleteEventHandler) {
                RequestCompleteEventHandler(fc);
            }            
        }
    }
}
