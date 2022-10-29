using RestSharpTest.DataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharpTest.Tests.TestData;
using RestSharpTest.Resources;

namespace RestSharpTest.Helpers;

public class BookHelper
{
    public static async Task<RestResponse<BookJsonModel>> CreateNewBookingData(RestClient restClient)
    {
        restClient = new RestClient();
        restClient.AddDefaultHeader("Accept", "application/json");

        #region Post Method
        var postRequest = new RestRequest(Endpoints.GetURL(Endpoints.BookingEndPoint)).AddJsonBody(GenerateBooking.demoBooking());
        return await restClient.ExecutePostAsync<BookJsonModel>(postRequest);
        #endregion
    }
    public static async Task<RestResponse<Booking>> GetBookById(RestClient restClient, long id)
    {
        restClient = new RestClient();
        restClient.AddDefaultHeader("Accept", "application/json");

        #region Get Request
        var getRequest = new RestRequest(Endpoints.GetUri(Endpoints.BookingEndPoint) + "/" + id);
        return await restClient.ExecuteGetAsync<Booking>(getRequest);
        #endregion
    }
    public static async Task<RestResponse<Booking>> UpdateBookingData(RestClient restClient, long id, Booking objectModel)
    {
        restClient = new RestClient();
        var token = await CreateToken(restClient);

        restClient.AddDefaultHeader("Accept", "application/json");
        restClient.AddDefaultHeader("Cookie", "token=" + token);

        #region Put Method
        var putRequest = new RestRequest(Endpoints.GetUri(Endpoints.BookingEndPoint) + "/" + id).AddJsonBody(objectModel);
        return await restClient.ExecutePutAsync<Booking>(putRequest);
        #endregion

    }
    public static async Task<RestResponse> DeleteBookingData(RestClient restClient, long id)
    {
        restClient = new RestClient();
        var token = await CreateToken(restClient);

        restClient.AddDefaultHeader("Accept", "application/json");
        restClient.AddDefaultHeader("Cookie", "token=" + token);

        #region Delete Method
        var deleteRequest = new RestRequest(Endpoints.GetUri(Endpoints.BookingEndPoint) + "/" + id);
        return await restClient.DeleteAsync(deleteRequest);
        #endregion

    }
    public static async Task<string> CreateToken(RestClient restClient)
    {
        restClient = new RestClient();
        restClient.AddDefaultHeader("Accept", "application/json");

        #region Post Request
        var postRequest = new RestRequest("https://restful-booker.herokuapp.com/auth").AddJsonBody(GenerateToken.generateToken());
        var postResponse = await restClient.ExecutePostAsync<TokenResponse>(postRequest);
        #endregion

        return postResponse.Data.Token;
    }
}
