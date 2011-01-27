using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hammock.Authentication.OAuth;
using Hammock;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Prohibition.SimpleGeo.Features;
using System.Net;

namespace Prohibition.SimpleGeo {

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

        public void GetNearbyPlaces(double latitude, double longitude, Action<FeatureCollection> fcCallback, string query = null, string category = null) { 

            var path = string.Format("places/{0},{1}.json", latitude, longitude);
            
            //TODO: less ternary operator!
            path += !string.IsNullOrEmpty(query) ? "?q=" + query : "";
            path += !string.IsNullOrEmpty(category) ? 
                !string.IsNullOrEmpty(query) ? "?" : "&" + "category=" + category: "";

            var request = new RestRequest() { Path = path };
            var callback = new RestCallback((restRequest, restResponse, userState) => {
                FeatureCollection fc = null;
                if (restResponse.StatusCode == HttpStatusCode.OK) {
                    var jObj = JObject.Parse(restResponse.Content);
                    fc = JsonConvert.DeserializeObject<FeatureCollection>(restResponse.Content);
                } else {
                    //TODO: raise intelligent exceptions
                    fc = new FeatureCollection() { Features = new List<Feature>() };
                }
                fcCallback(fc);
            });

            var client = new RestClient();
            var credentials = new OAuthCredentials() {
                Type = OAuthType.RequestToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                ConsumerKey = "vvc2y7nAjkx6fUaJqQ94FT7nAdZCWQrA",
                ConsumerSecret = "CJYFj8Sy3WwDL2sFfQXJnDdyXh7BqDU2"
            };

            client.Authority = AUTHORITY;
            client.VersionPath = VERSIONPATH;
            client.Credentials = credentials;

            client.BeginRequest(request, callback);
        }

    }
}
