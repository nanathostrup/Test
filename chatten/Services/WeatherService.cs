using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class WeatherService
    {
        // ❌ Hardcoded API key
        private const string OpenWeatherApiKey = "sk_live_FAKE_WEATHER_API_KEY_987654321";

        // ❌ Another secret type (JWT signing key)
        private const string JwtSigningSecret = "MySuperSecretJwtSigningKey_ChangeMe!";

        private readonly HttpClient _httpClient;

      //  public WeatherService()
      //  {
      //      _httpClient = new HttpClient();
      //  }

        public async Task<string> GetWeatherAsync(string city)
        {
            string url =
                $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={OpenWeatherApiKey}&units=metric";

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                return $"Weather data received successfully.\nJWT Secret in memory: {JwtSigningSecret}\n\n{response}";
            }
            catch (Exception ex)
            {
                return $"Error retrieving weather data: {ex.Message}";
            }
        }
    }
}