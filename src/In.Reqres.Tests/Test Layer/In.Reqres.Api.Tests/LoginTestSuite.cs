﻿using FluentAssertions;
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
        public void Login_UserShouldGetOkStatusCodeWithValidCredentials()
        {

            using (var client = new RequestClient("https://reqres.in"))
            {
                var data = new LoginCredentials()
                {
                    email = "eve.holt@reqres.in",
                    password = "cityslicka"

                };

                var response = client.Post(data,
                                           "/api/login");
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }


        [Test]
        public void Login_UserShouldGetOkStatusCodeWithInvalidValidCredentials()
        {

            using (var client = new RequestClient("https://reqres.in"))
            {
                var data = new LoginCredentials()
                {
                    email = "eve.holt@reqres.in",

                };
                var response = client.Post(data, "/api/login");
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }

        [Test]
        public void Login_UserShouldGetTokenValueWithValidCredentials()
        {

            using (var client = new RequestClient("https://reqres.in"))
            {
                var data = new LoginCredentials()
                {
                    email = "eve.holt@reqres.in",
                    password = "cityslicka"

                };
                var response = client.Post<Success,LoginCredentials>(data, "/api/login");
                response.token.Should().NotBe(null);
            }
        }

        [Test]
        public void Login_UserShouldGetValidErrorMessageWithInvaildParameters()
        {

            using (var client = new RequestClient("https://reqres.in"))
            {
                var data = new LoginCredentials()
                {
                    email = "eve.holt@reqres.in",

                };
                var response = client.Post<ErrorMessage, LoginCredentials>(data,
                                                                           "/api/login");
                response.error.Should().Be("Missing password");
            }
        }

        [Test]
        public void Login_UserShouldGetCookiesInResponseHeaderWithValidCredentials()
        {

            using (var client = new RequestClient("https://reqres.in"))
            {
                var data = new LoginCredentials()
                {
                    email = "eve.holt@reqres.in",
                    password = "cityslicka"

                };
                var response = client.Post(data,
                                           "/api/login");
                response.Headers.GetValues("Set-Cookie").Should().NotBeNull();
            }
        }

        [Test]
        public void Login_UserShouldNotBeAbleToLoginWithValidCredentialsAndInvalidEndPoints()
        {

            using (var client = new RequestClient("https://reqres.in"))
            {
                var data = new LoginCredentials()
                {
                    email = "eve.holt@reqres.in",
                    password = "cityslicka"

                };
                var response = client.Post(data,
                                           "/api/login/InvalidEndPoints");
                response.StatusCode.Should().NotBe(HttpStatusCode.OK)
                    .And.NotBe(HttpStatusCode.Created);
            }
        }
    }
}