using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlyttaIn.Services.Communications
{
    public class EniroService
    {
        public const string Eniro = "http://api.eniro.com/cs/proximity/basic?profile={0}&key={1}&country=se&version=1.0.1&latitude={2}&longitude={3}&max_distance={{4}}&search_word={5}";
        public const string EniroProfile = "dizablprofile";
        public const string EniroKey = "8234556222978835312";

        public string GetEniro(string lat, string lng, string radiusInMeter, string searchFor)
        {
            var newUrl = string.Format(Eniro, EniroProfile, EniroKey, lat, lng, radiusInMeter, searchFor);
            var res = Helper.CreateHttpGet(newUrl, Helper.ContentType.Json);
            return res;
        }
    }
}

