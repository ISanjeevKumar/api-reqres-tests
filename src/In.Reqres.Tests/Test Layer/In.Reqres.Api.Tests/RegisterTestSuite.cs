using FluentAssertions;
using In.Reqres.DataModel;
using In.Reqres.Tests.WebServices;
using NUnit.Framework;
using System.Net;

namespace In.Reqres.Api.Tests
{
    [TestFixture]
    public class RegisterTestSuite
    {

        [Test]
        public void Resigter_UserShouldGetOkStatusCodeWithValidData()
        {

            using (var client = new RequestClient("https://reqres.in"))
            {
                var data = new LoginCredentials()
                {
                    email = "eve.holt@reqres.in",
                    password = "pistol"

                };

                var response = client.Post(data, "/api/register");
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Test]
        public void Resigter_UserShouldGetTokenValueInResponseWithValidData()
        {

            using (var client = new RequestClient("https://reqres.in"))
            {
                var data = new LoginCredentials()
                {
                    email = "eve.holt@reqres.in",
                    password = "pistol"

                };

                var response = client.Post<Success,LoginCredentials>(data, "/api/register");
                response.token.Should().NotBeNull();
            }
        }

        [Test]
        public void Resigter_UserShouldGetBadRequestStatusCodeWithInvalidData()
        {

            using (var client = new RequestClient("https://reqres.in"))
            {
                var data = new LoginCredentials()
                {
                    email = "sydney@fife",
                    password = "InvalidPassword"

                };

                var response = client.Post(data, "/api/register");
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }

        [Test]
        public void Resigter_UserShouldGetErrorMessageWithInvalidData()
        {

            using (var client = new RequestClient("https://reqres.in"))
            {
                var data = new LoginCredentials()
                {
                    email = "sydney@fife",
                    password = "InvalidPassword"

                };

                var response = client.Post<ErrorMessage, LoginCredentials>(data, "/api/register");
                response.error.Should().Be("Note: Only defined users succeed registration");
            }
        }

        [Test]
        public void Resigter_UserShouldNotBeAbleToRegisterWithInvalidEndPointAndValidData()
        {

            using (var client = new RequestClient("https://reqres.in"))
            {
                var data = new LoginCredentials()
                {
                    email = "eve.holt@reqres.in",
                    password = "pistol"

                };

                var response = client.Post(data, "/api/register/InvalidEndPoints");
                response.StatusCode.Should().NotBe(HttpStatusCode.OK)
                   .And.NotBe(HttpStatusCode.Created);
            }
        }

    }
}
