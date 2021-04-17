using FluentAssertions;
using In.Reqres.DataModel;
using In.Reqres.Tests.WebServices;
using NUnit.Framework;
using System.Net;

namespace In.Reqres.Api.Tests
{
    [TestFixture]
    public class CreateUserTests
    {

        [Test]
        public void CreateUser_UserShouldGetCreatedStatusCodeWithValidData()
        {
            using (var client = new RequestClient("https://reqres.in"))
            {
                var data = new User()
                {
                    Name = "Sanjeev",
                    Job = "Test Engineer"

                };

                var response = client.Post(data, "/api/users");
                response.StatusCode.Should().Be(HttpStatusCode.Created);
            }
        }

        [Test]
        public void CreateUser_VerifyIdIsGeneratedSuccessfullyWithValidData()
        {

            using (var client = new RequestClient("https://reqres.in"))
            {
                var data = new User()
                {
                    Name = "Sanjeev",
                    Job = "Test Engineer"

                };
                var response = client.Post<User, User>(data, "/api/users");
                response.Id.Should().NotBe(null);

            }
        }
    }
}
