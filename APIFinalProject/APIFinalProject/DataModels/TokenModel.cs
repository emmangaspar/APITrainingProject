using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientTest.DataModels
{
    public class TokenModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
    public class TokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
