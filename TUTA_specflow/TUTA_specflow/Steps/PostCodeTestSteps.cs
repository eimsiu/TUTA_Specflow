using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using TUTA_specflow.Entities.JsonObjects;

namespace TUTA_specflow.Steps
{
    [Binding]
    public class PostCodeTestSteps
    {
        public static HttpClient HttpClient { get; private set; }
        public static HttpRequestMessage HttpRequestMessage { get; private set; }
        public static HttpResponseMessage HttpResponseMessage { get; private set; }
        internal static PostCodeObject PostCodeResult { get => PostCodeResult; set => PostCodeResult = value; }

        private static PostCodeObject postCodeResult = new PostCodeObject() { };

        private static string _postCodeUri;

        private int responseCodeValue;

        public static void SwapOutHttpClient(HttpClient client)
        {
            HttpClient = client;
        }

        [BeforeScenario]
        public void Before()
        {
            if (HttpClient == null)
                HttpClient = new HttpClient();

            if (HttpRequestMessage == null)
                HttpRequestMessage = new HttpRequestMessage();
        }


        [Given(@"I am using the base url '(.*)' value")]
        public void GivenIAmUsingTheBaseUrlValue(string baseURL)
        {
            if (HttpClient.BaseAddress == null)
            {
                HttpClient.BaseAddress = new Uri(baseURL);
            }
        }

        [Given(@"I setup the request to GET using the provided '(.*)' value")]
        public void GivenISetupTheRequestToGETUsingTheProvidedValue(string postCode)
        {
            HttpRequestMessage = new HttpRequestMessage();
            HttpRequestMessage.Method = new HttpMethod("Get");
            _postCodeUri = postCode;
        }

        [When(@"I send the request")]
        public void WhenISendTheRequest()
        {
            HttpRequestMessage.RequestUri = new Uri(_postCodeUri, UriKind.Relative);
            HttpResponseMessage = HttpClient.SendAsync(HttpRequestMessage).Result;
        }

        [Then(@"I should receive a response")]
        public void ThenIShouldReceiveAResponse()
        {
            string json = @"" + HttpResponseMessage.Content.ReadAsStringAsync().Result + "";

            postCodeResult = JsonConvert.DeserializeObject<PostCodeObject>(json);
        }

        
    }
}
