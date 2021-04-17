using FluentAssertions;
using In.Reqres.DataModel;
using In.Reqres.Tests.WebServices;
using NUnit.Framework;
using System.Net;

namespace In.Reqres.Api.Tests
{
    [TestFixture]
    public class LoginTestSuite
    {

        [Test]
        public void Login_UserShouldGetOkStatusCodeWithValidData()
        {

            using (var client = new RequestClient("https://reqres.in"))
            {
                var data = new LoginCredentials()
                {
                    Email = "eve.holt@reqres.in",
                    Password = "cityslicka"

                };

                var response = client.Post(data, "/api/login");
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}
