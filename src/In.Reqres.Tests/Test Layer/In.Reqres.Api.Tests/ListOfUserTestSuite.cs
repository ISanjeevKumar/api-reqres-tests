using FluentAssertions;
using In.Reqres.DataModel;
using In.Reqres.Tests.WebServices;
using NUnit.Framework;
using System.Diagnostics;
using System.Net;

namespace In.Reqres.Api.Tests
{

    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class ListOfUserTestSuite : TestBase
    {
        protected string ListUsersEndPoint { get; set; }
        public ListOfUserTestSuite()
        {
            ListUsersEndPoint = TestContext.Parameters["ListUsersEndPoint"];
        }

        [Test]
        public void GetListUsers_UserShouldGetOkStatusWithValidEndPoint()
        {
            using (var client = new RequestClient(BaseAddress))
            {
                var response = client.Get(urlParams: ListUsersEndPoint);
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Test]
        public void GetListUsers_ValidateResponseDataShouldContainsDataFields()
        {
            using (var client = new RequestClient(BaseAddress))
            {
                var response = client.Get<ListUsers>(urlParams: ListUsersEndPoint);
                response.data.Should().HaveCountGreaterThan(0);
                response.page.Should().Be(2);
            }
        }

        [Test]
        public void GetListUsers_ValidateResponseDataShouldContainsNoDataForInvalidEndPoint()
        {
            using (var client = new RequestClient(BaseAddress))
            {
                var response = client.Get<ListUsers>(urlParams: "/api/users?page=9");
                response.data.Should().HaveCount(0);
            }
        }

        [Test]
        public void GetListUsers_ValidateResponseHeaders()
        {
            using (var client = new RequestClient(BaseAddress))
            {
                var response = client.Get(urlParams: ListUsersEndPoint);
                response.Headers.GetValues("Server").Should().Contain("cloudflare");
                response.Headers.GetValues("Set-Cookie").Should().NotBeNull();
            }
        }

        [Test]
        public void GetListUsers_ValidateResponseTimeShouldbeLessThanAMinute()
        {
            using (var client = new RequestClient(BaseAddress))
            {
                var watch = Stopwatch.StartNew();
                var response = client.Get(urlParams: ListUsersEndPoint);
                watch.Stop();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                watch.ElapsedMilliseconds.Should().BeLessThan(60000);
            }
        }
    }
}
