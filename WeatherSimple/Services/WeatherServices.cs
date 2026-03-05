using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherStation
{
    public class WeatherServices
    {
        // Hardcoded API key
        private const string OpenWeather = "ea413b8c6e9657e69c24cc2b83e6d894"; // generated here: https://home.openweathermap.org/api_keys -- DONT USE
        string apiKey = Environment.GetEnvironmentVariable("MY_API_KEY");
        private readonly HttpClient _httpClient = new HttpClient();
        public async Task<string> GetWeatherAsync(string city)
        {
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