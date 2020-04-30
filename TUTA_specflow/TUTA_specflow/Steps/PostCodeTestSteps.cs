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
        [Then(@"I should receive a status code equal (.*)")]
        public void ThenIShouldReceiveAStatusCodeEqual(int responseCode)
        {
            responseCodeValue = responseCode;

            Assert.That((int)HttpResponseMessage.StatusCode, Is.EqualTo(responseCodeValue));
        }
        [Then(@"I validate country should have (.*) value")]
        public void ThenIValidateCountryShouldHaveValue(string country)
        {
            Assert.That(postCodeResult.result.country == country);
        }
        [Then(@"I validate that geographic Cartesian coordinates have (.*) value and (.*) value")]
        public void ThenIValidateThatGeographicCartesianCoordinatesHaveValueAndValue(int eastings, int northings)
        {
            Assert.That((int)postCodeResult.result.eastings, Is.EqualTo(eastings));
            Assert.That((int)postCodeResult.result.northings, Is.EqualTo(northings));
        }
        [Then(@"I validate that codes\.admin_district has value '(.*)'")]
        public void ThenIValidateThatCodes_Admin_DistrictHasValue(string code_adminDistrict)
        {
            Assert.That((string)postCodeResult.result.codes.admin_district == code_adminDistrict);
        }
        [Then(@"I validate that '(.*)' has value of either nhs_ha, msoa or lsoa")]
        public void ThenIValidateThatHasValueOfEitherNhs_HaMsoaOrLsoa(string ResponseObjectValue)
        {
            switch (ResponseObjectValue)
            {
                
            }    
        }
      
            
        
    }
}
