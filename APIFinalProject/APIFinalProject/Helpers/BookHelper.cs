using HttpClientTest.DataModels;
using HttpClientTest.Resources;
using HttpClientTest.Tests;
using HttpClientTest.Tests.TestData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientTest.Helpers
{
    public class BookHelper
    {
        public static async Task <HttpResponseMessage> CreateNewBookingData()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var request = JsonConvert.SerializeObject(GenerateBooking.demoBooking());
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            #region Post Request
            return await httpClient.PostAsync(Endpoints.GetURL(Endpoints.BookingEndPoint), postRequest);
            #endregion
    
        }
        public static async Task<HttpResponseMessage> GetBookById(long id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            #region Get Request
            return await httpClient.GetAsync(Endpoints.GetUri(Endpoints.BookingEndPoint) + "/" + id);
            #endregion
        }
        public static async Task<HttpResponseMessage> UpdateBookingData(long id, Booking objectModel)
        {
            var httpClient = new HttpClient();
            var token = await CreateToken();

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Cookie", "token=" + token);

            var request = JsonConvert.SerializeObject(objectModel);
            var putRequest = new StringContent(request, Encoding.UTF8, "application/json");

            #region Sending Put Request
            return await httpClient.PutAsync(Endpoints.GetUri(Endpoints.BookingEndPoint) + "/" + id, putRequest);
            #endregion

        }
        public static async Task<HttpResponseMessage> DeleteBookingData(long id)
        {
            var httpClient = new HttpClient();
            var token = await CreateToken();

            #region Sending Delete Request
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Cookie", "token=" + token);
            
            return await httpClient.DeleteAsync(Endpoints.GetUri(Endpoints.BookingEndPoint) + "/" + id);
            #endregion
        }
        public static async Task<string> CreateToken()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var request = JsonConvert.SerializeObject(GenerateToken.generateToken());
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            #region Post Request
            var httpResponse = await httpClient.PostAsync("https://restful-booker.herokuapp.com/auth", postRequest);

            var token = JsonConvert.DeserializeObject<TokenResponse>(httpResponse.Content.ReadAsStringAsync().Result);
            #endregion

            return token.Token;
        }
    }
}
