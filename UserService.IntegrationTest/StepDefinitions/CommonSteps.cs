using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace UserServie.IntegrationTests.StepDefinitions
{
    [Binding]
    public class CommonSteps : IClassFixture<WebApplicationFactory<TestStartup>>
    {
        private WebApplicationFactory<TestStartup> factory;

        private HttpClient client { get; set; }

        private HttpResponseMessage response { get; set; }

        public CommonSteps(WebApplicationFactory<TestStartup> factory)
        {
              this.factory = factory;
        }

        [Given(@"I am a client")]
        public void GivenIAmAClient()
        {
            this.client = this.factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"http://localhost/")
            });
        }

        [When(@"I make a post request to ""(.*)"" with the following data ""(.*)""")]
        public virtual async Task WhenIMakeAPostRequestToWithTheFollowingData(string resourceEndPoint, string postDataJson)
        {
            var postRelativeUri = new Uri(resourceEndPoint, UriKind.Relative);
            var content = new StringContent(postDataJson, Encoding.UTF8, "application/json");
            this.response = await this.client.PostAsync(postRelativeUri, content).ConfigureAwait(false);
        }

        [Then(@"the response status code is ""(.*)""")]
        public void ThenTheResponseStatusCodeIs(int statusCode)
        {
            var expectedStatusCode = (HttpStatusCode)statusCode;
            Assert.Equal(expectedStatusCode, this.response.StatusCode);
        }

        [Then(@"the response data should be ""(.*)""")]
        public void ThenTheResponseDataShouldBe(string expectedResponse)
        {
            var responseData = this.response.Content.ReadAsStringAsync().Result;
            Assert.Equal(expectedResponse, responseData);
        }
    }
}
