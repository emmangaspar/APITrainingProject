using Newtonsoft.Json;
using RestSharp;
using RestSharpTest.DataModels;
using RestSharpTest.Helpers;
using RestSharpTest.Resources;
using System.Net;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace RestSharpTest.Tests
{
    [TestClass]
    public class RestSharpTests : ApiBaseTest
    {
        [TestMethod]
        public async Task CreateNewBookingData()
        {
            #region Creating and retrieving new booking data
            var postReponse = await BookHelper.CreateNewBookingData(RestClient);
            var getReponse = await BookHelper.GetBookById(RestClient, postReponse.Data.Bookingid);
            var createdBookingData = postReponse.Data;
            var retrievedBookingData = getReponse.Data;
            #endregion

            #region Assertion
            Assert.AreEqual(HttpStatusCode.OK, postReponse.StatusCode, "Status code is not equal to 201");

            Assert.AreEqual(createdBookingData.Booking.Firstname, retrievedBookingData.Firstname, "First Name didn't matched");
            Assert.AreEqual(createdBookingData.Booking.Lastname, retrievedBookingData.Lastname, "Last Name didn't matched");
            Assert.AreEqual(createdBookingData.Booking.Totalprice, retrievedBookingData.Totalprice, "Total Price didn't matched");
            Assert.AreEqual(createdBookingData.Booking.Depositpaid, retrievedBookingData.Depositpaid, "Deposit Paid didn't matched");
            Assert.AreEqual(createdBookingData.Booking.Bookingdates.Checkin, retrievedBookingData.Bookingdates.Checkin, "Checkin date didn't matched");
            Assert.AreEqual(createdBookingData.Booking.Bookingdates.Checkout, retrievedBookingData.Bookingdates.Checkout, "Checkout date didn't matched");
            Assert.AreEqual(createdBookingData.Booking.Additionalneeds, retrievedBookingData.Additionalneeds, "Additional needs didn't matched");
            #endregion

            #region Cleanup
            var deleteRequest = await BookHelper.DeleteBookingData(RestClient, createdBookingData.Bookingid);
            Assert.AreEqual(HttpStatusCode.Created, deleteRequest.StatusCode, "Status code is not equal to 201");
            #endregion
        }
        [TestMethod]
        public async Task UpdateFirstAndLastName()
        {
            #region create data and send put request
            var postResponse = await BookHelper.CreateNewBookingData(RestClient);
            var postCreatedBooking = postResponse.Data;
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
            var putResponse = await BookHelper.UpdateBookingData(RestClient, postCreatedBooking.Bookingid, booking);
            var updatedBookingData = putResponse.Data;
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
            var deleteRequest = await BookHelper.DeleteBookingData(RestClient, postCreatedBooking.Bookingid);
            Assert.AreEqual(HttpStatusCode.Created, deleteRequest.StatusCode, "Status code is not equal to 201");
            #endregion
        }
        [TestMethod]
        public async Task DeleteBookingRecord()
        {
            #region Creating a new booking data
            var postResponse = await BookHelper.CreateNewBookingData(RestClient);
            var postCreatedBooking = postResponse.Data;
            #endregion

            #region Act
            var deleteResponse = await BookHelper.DeleteBookingData(RestClient, postCreatedBooking.Bookingid);
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
            var postResponse = await BookHelper.CreateNewBookingData(RestClient);
            var postCreatedBooking = postResponse.Data;
            #endregion

            #region Act
            var getResponse = await BookHelper.GetBookById(RestClient, (long)Convert.ToDouble(InvalidCode));
            #endregion

            #region Assertion of 404 status code
            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode, "Status code is not equal to 404");
            #endregion

            #region Cleanup
            var deleteRequest = await BookHelper.DeleteBookingData(RestClient, postCreatedBooking.Bookingid);
            Assert.AreEqual(HttpStatusCode.Created, deleteRequest.StatusCode, "Status code is not equal to 201");
            #endregion
        }
    }
}