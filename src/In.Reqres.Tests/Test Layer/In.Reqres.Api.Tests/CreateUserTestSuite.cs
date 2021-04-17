using FluentAssertions;
using In.Reqres.DataModel;
using In.Reqres.Tests.WebServices;
using NUnit.Framework;
using System.Net;

namespace In.Reqres.Api.Tests
{
    [TestFixture]
    public class CreateUserTestSuite : TestBase
    {
        protected string CreateUserEndPoint { get; set; }

        public CreateUserTestSuite()
        {
            CreateUserEndPoint = TestContext.Parameters["CreateUserEndPoint"];
        }

        [Test]
        public void CreateUser_UserShouldGetCreatedStatusCodeWithValidData()
        {
            using (var client = new RequestClient(BaseAddress))
            {
                var data = new User()
                {
                    name = "Sanjeev",
                    job = "Test Engineer"

                };

                var response = client.Post(data, CreateUserEndPoint);
                response.StatusCode.Should().Be(HttpStatusCode.Created);
            }
        }

        [Test]
        public void CreateUser_ValidateResponseDataForValidRequestData()
        {

            using (var client = new RequestClient(BaseAddress))
            {
                var data = new User()
                {
                    name = "Sanjeev",
                    job = "Test Engineer"

                };
                var response = client.Post<User, User>(data, "/api/users");
                response.id.Should().NotBe(null);
                response.name.Should().Be(data.name);
                response.job.Should().Be(data.job);
            }
        }

        [Test]
        public void CreateUser_VerifyResponseHeadersAreComingAsExpected()
        {

            using (var client = new RequestClient(BaseAddress))
            {
                var data = new User()
                {
                    name = "Sanjeev",
                    job = "Test Engineer"

                };
                var response = client.Post(data, CreateUserEndPoint);
                response.Headers.GetValues("Server").Should().Contain("cloudflare");
                response.Headers.GetValues("Set-Cookie").Should().NotBeNull();
            }
        }


    }
}
