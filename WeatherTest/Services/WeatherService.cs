using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherStation
{
    public class WeatherServices
    {
        // Hardcoded API key
        private const string heltvildtsejtvejApiKey = "api_XweVmYIoqSCHxVOb4Q6C1zMFs0O92zPu"; //generated here: https://generate-random.org/api-keys with prefix 'api'
        private const string heltvildtsejtvejApiKey2 = "sk_live_skfikf5682lfjas896dsndhfuek9hy654";
        // ❌ Another secret type (JWT signing key)
        private const string JwtSigning = "WOlJeDRXzIDR9N0xXrQjIOYNoMYrlEvMz3HF91RTy"; //generated here: https://jwtsecretkeygenerator.com/
        private const string JwtSigningSecret = "WOlJeDRXzIDR9N0xXrQjIOYNoMYrlEvMz3HF91RTy"; //generated here: https://jwtsecretkeygenerator.com/
        private readonly HttpClient _httpClient;
       public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetWeatherAsync(string city)
        {
            string url =
                $"https://api.heltvildtsejtvejr.org/data/2.5/weather?q={city}&appid={OpenWeatherApiKey}&units=metric"; //Non-existing website

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