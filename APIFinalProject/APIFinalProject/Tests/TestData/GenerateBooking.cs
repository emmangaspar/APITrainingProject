using HttpClientTest.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientTest.Tests.TestData
{
    public class GenerateBooking
    {
        public static Booking demoBooking()
        {
            return new Booking
            {
                Firstname = "Emman",
                Lastname = "Gaspar",
                Totalprice = 111,
                Depositpaid = true,
                Bookingdates = new Bookingdates(){
                    Checkin = DateTime.Parse("2018-01-01"),
                    Checkout = DateTime.Parse("2018-01-02")
                },
                Additionalneeds = "Breakfast"
            };
        }
    }
}
