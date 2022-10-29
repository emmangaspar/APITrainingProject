using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientTest.Resources
{
    public class Endpoints
    {
        //Base URL
        public const string baseURL = "https://restful-booker.herokuapp.com/";
        public static readonly string BookingEndPoint = "booking";
        public static string GetURL(string endpoint) => $"{baseURL}{endpoint}";
        public static Uri GetUri(string endpoint) => new Uri(GetURL(endpoint));

    }
}
