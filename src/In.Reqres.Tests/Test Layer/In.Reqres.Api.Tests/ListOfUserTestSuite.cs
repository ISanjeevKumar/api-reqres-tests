﻿using FluentAssertions;
using In.Reqres.DataModel;
using In.Reqres.Tests.WebServices;
using NUnit.Framework;
using System.Net;

namespace In.Reqres.Api.Tests
{

    [TestFixture]
    public class ListOfUserTestSuite
    {
        [Test]
        public void GetListOfUsers_UserShouldGetOkStatusWithValidEndPoint()
        {
            using (var client = new RequestClient("https://reqres.in"))
            {
                var response = client.GetHttpResponseMessage("/api/users?page=2");
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}