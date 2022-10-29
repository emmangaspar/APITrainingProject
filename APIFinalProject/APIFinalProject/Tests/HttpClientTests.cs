using HttpClientTest.DataModels;
using System.Net.Http;
using HttpClientTest.Tests.TestData;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using HttpClientTest.Resources;
using HttpClientTest.Helpers;
using RestSharp;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace HttpClientTest.Tests
{
    [TestClass]
    public class HttpClientTests
    {
        [TestMethod]
        public async Task CreateNewBook()
        {
            #region Creating a new booking data
            var postResponse = await BookHelper.CreateNewBookingData();
            #endregion
            
            #region Act
            var postCreatedBooking = JsonConvert.DeserializeObject<BookJsonModel>(postResponse.Content.ReadAsStringAsync().Result);
            var getResponse = await BookHelper.GetBookById(postCreatedBooking.Bookingid);
            var getCreatedBooking = JsonConvert.DeserializeObject<Booking>(getResponse.Content.ReadAsStringAsync().Result);   
            #endregion

            #region Assertion of created data
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode, "Status code is not equal to 201");
            Assert.AreEqual(getCreatedBooking.Firstname, postCreatedBooking.Booking.Firstname, "First Name didn't matched.");
            Assert.AreEqual(getCreatedBooking.Lastname, postCreatedBooking.Booking.Lastname, "Last Name didn't matched.");
            Assert.AreEqual(getCreatedBooking.Totalprice, postCreatedBooking.Booking.Totalprice, "Total price didn't matched.");
            Assert.AreEqual(getCreatedBooking.Additionalneeds, postCreatedBooking.Booking.Additionalneeds, "Additional needs didn't matched.");
            Assert.AreEqual(getCreatedBooking.Bookingdates.Checkin, postCreatedBooking.Booking.Bookingdates.Checkin, "Check in date didn't matched.");
            Assert.AreEqual(getCreatedBooking.Bookingdates.Checkout, postCreatedBooking.Booking.Bookingdates.Checkout, "Check out date didn't matched.");
            #endregion

            #region Cleanup
            var deleteRequest = await BookHelper.DeleteBookingData(postCreatedBooking.Bookingid);
            Assert.AreEqual(HttpStatusCode.Created, deleteRequest.StatusCode, "Status code is not equal to 201");
            #endregion

        }
        [TestMethod]
        public async Task UpdateFirstAndLastName()
        {
            #region create data and send put request
            var postResponse = await BookHelper.CreateNewBookingData();
            var postCreatedBooking = JsonConvert.DeserializeObject<BookJsonModel>(postResponse.Content.ReadAsStringAsync().Result);
            Booking booking = new Booking()
            {
                Firstname = "EmmanUpdated",
                Lastname = "GasparUpdated",
                Totalprice = 111,
                Depositpaid = true,
                Bookingdates = new Bookingdates()
                {
                    Checkin = DateTime.Parse("2018-01-01"),
                    Checkout = DateTime.Parse("2018-01-02")
                },
                Additionalneeds = "Breakfast"
            };
            #endregion

            #region Act
            var putResponse = await BookHelper.UpdateBookingData(postCreatedBooking.Bookingid, booking);
            var updatedBookingData = JsonConvert.DeserializeObject<Booking>(putResponse.Content.ReadAsStringAsync().Result);
            #endregion

            #region Assertion of created data
            Assert.AreEqual(HttpStatusCode.OK, putResponse.StatusCode, "Status code is not equal to 201");
            Assert.AreEqual(updatedBookingData.Firstname, booking.Firstname, "First Name didn't matched.");
            Assert.AreEqual(updatedBookingData.Lastname, booking.Lastname, "Last Name didn't matched.");
            Assert.AreEqual(updatedBookingData.Totalprice, postCreatedBooking.Booking.Totalprice, "Total price didn't matched.");
            Assert.AreEqual(updatedBookingData.Additionalneeds, postCreatedBooking.Booking.Additionalneeds, "Additional needs didn't matched.");
            Assert.AreEqual(updatedBookingData.Bookingdates.Checkin, postCreatedBooking.Booking.Bookingdates.Checkin, "Check in date didn't matched.");
            Assert.AreEqual(updatedBookingData.Bookingdates.Checkout, postCreatedBooking.Booking.Bookingdates.Checkout, "Check out date didn't matched.");
            #endregion

            #region Cleanup
            var deleteRequest = await BookHelper.DeleteBookingData(postCreatedBooking.Bookingid);
            Assert.AreEqual(HttpStatusCode.Created, deleteRequest.StatusCode, "Status code is not equal to 201");
            #endregion
        }
        [TestMethod]
        public async Task DeleteBookingRecord() {
            #region Creating a new booking data
            var postResponse = await BookHelper.CreateNewBookingData();
            var postCreatedBooking = JsonConvert.DeserializeObject<BookJsonModel>(postResponse.Content.ReadAsStringAsync().Result);
            #endregion

            #region Act
            var deleteResponse = await BookHelper.DeleteBookingData(postCreatedBooking.Bookingid);
            #endregion

            #region Assertion status code
            Assert.AreEqual(HttpStatusCode.Created, deleteResponse.StatusCode, "Status code is not equal to 201");
            #endregion
        }
        [TestMethod]
        public async Task GetInvalidBookId()
        {
            #region create data and send put request
            var InvalidCode = "99999999999999999999";
            var postResponse = await BookHelper.CreateNewBookingData();
            var postCreatedBooking = JsonConvert.DeserializeObject<BookJsonModel>(postResponse.Content.ReadAsStringAsync().Result);
            #endregion

            #region Act
            var getResponse = await BookHelper.GetBookById((long)Convert.ToDouble(InvalidCode));
            #endregion

            #region Assertion of 404 status code
            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode, "Status code is not equal to 404");
            #endregion

            #region Cleanup
            var deleteRequest = await BookHelper.DeleteBookingData(postCreatedBooking.Bookingid);
            Assert.AreEqual(HttpStatusCode.Created, deleteRequest.StatusCode, "Status code is not equal to 201");
            #endregion
        }
    }
}