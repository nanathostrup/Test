using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherStation
{
    public class WeatherServices
    {
        string? apiKey = Environment.GetEnvironmentVariable("MY_API_KEY"); // Hardcoded API key in env file
        string? defaultCity = Environment.GetEnvironmentVariable("WEATHER_DEFAULT_CITY"); // Non-secret - Test to differntiate between actual secret and non secret

        private readonly HttpClient _httpClient = new HttpClient();
        public async Task<string> GetWeatherAsync(string city) // Testing with a hardcoded secret in the env file
        {
            city ??= defaultCity;
            string url =
             $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric"; // Actually working website fetching weather forecast
            try
            {
                var response = await _httpClient.GetStringAsync(url);
                return $"Weather data received successfully: \n\n{response}";
            }
            catch (Exception ex)
            {
                return $"Error retrieving weather data: {ex.Message}";
            }
        }
    }
}