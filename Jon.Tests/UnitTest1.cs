using Allure.Net.Commons.Attributes;
using Allure.NUnit;
using Jon.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;


namespace WebApplication1.Tests;

[AllureNUnit]
[AllureSuite("WeatherForecast API Tests Suite")]
[AllureEpic("WeatherForecast API Tests")]
public sealed class WeatherForecastTests
{
    [AllureStory("The WeatherForecast API returns five forecasts.")]
    [Test]
    public async Task GetWeatherForecast_ReturnsFiveForecasts()
    {
        await using var application = new WebApplicationFactory<Jon.Api.Program>();
        using var client = application.CreateClient();

        var response = await client.GetAsync("/WeatherForecast");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var forecasts =
            await response.Content.ReadFromJsonAsync<WeatherForecast[]>();

        Assert.That(forecasts, Is.Not.Null);
        Assert.That(forecasts, Has.Length.EqualTo(5));
    }
}