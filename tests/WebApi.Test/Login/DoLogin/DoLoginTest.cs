using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Login.DoLogin
{
    public class DoLoginTest : IClassFixture<CustemWebApplicationFactory>
    {
        private const string METHOD = "api/Login";

        private readonly HttpClient _httpClient;
        private readonly string _email;
        private readonly string _name;
        private readonly string _password;

        public DoLoginTest(CustemWebApplicationFactory webApplicationFactory)
        {
            _httpClient = webApplicationFactory.CreateClient();
            _name = webApplicationFactory.GetName();
            _email = webApplicationFactory.GetEmail();
            _password = webApplicationFactory.GetPassword();
        }

        [Fact]
        public async Task Success()
        {
            var request = new RequestLoginJson
            {
                Email = _email,
                Password = _password,
            };

            var response = await _httpClient.PostAsJsonAsync(METHOD, request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("name").GetString().Should().Be(_name);
            responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Invalid_Login(string cultureInfo)
        {
            var request = RequestLoginJsonBuilder.Build();

            _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(cultureInfo));

            var result = await _httpClient.PostAsJsonAsync(METHOD, request);

            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(cultureInfo));

            errors.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
        }
    }
}
