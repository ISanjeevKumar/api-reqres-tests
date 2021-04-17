using FluentAssertions;
using In.Reqres.DataModel;
using In.Reqres.Tests.WebServices;
using NUnit.Framework;
using System.Net;

namespace In.Reqres.Api.Tests
{
    [TestFixture]
    public class RegisterTests
    {

        [Test]
        public void Resigter_UserShouldGetOkStatusCodeWithValidData()
        {

            using (var client = new RequestClient("https://reqres.in"))
            {
                var data = new LoginCredentials()
                {
                    Email = "eve.holt@reqres.in",
                    Password = "pistol"

                };

                var response = client.Post(data, "/api/login");
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}
