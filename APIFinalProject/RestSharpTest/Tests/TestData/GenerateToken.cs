using RestSharpTest.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpTest.Tests.TestData
{
    public class GenerateToken
    {
        public static TokenModel generateToken()
        {
            return new TokenModel
            {
                Username = "admin",
                Password = "password123"
            };
        }
    }
}
