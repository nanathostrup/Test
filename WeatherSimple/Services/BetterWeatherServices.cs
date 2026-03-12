using System;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;

namespace WeatherStation
{
    public class BetterWeatherServices
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly SecretClient _secretClient = new SecretClient();
        public async Task<string> GetWeatherAsync(string city) // Testing with a better kept secret
        {
            try
            {
                //TODO - EXTRACT API IN A BETTER WAY
                string url =
                 $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric"; // Actually working website fetching weather forecast
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