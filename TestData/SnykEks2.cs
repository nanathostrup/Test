using System;
using System.Net.Http;
using System.Threading.Tasks;

public class StormySecure
{
    public static async Task<string> GetWeatherAsync(string town)
    {
        // Load the API key from an environment variable
        string? apiKey = Environment.GetEnvironmentVariable("WEATHER_API_KEY");

        // If the API key is missing, throw an error
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new Exception("API key not found in environment variables.");
        }

        string encodedTown = Uri.EscapeDataString(town);
        string encodedApiKey = Uri.EscapeDataString(apiKey);

        string url = $"https://api.weatherwand.xyz/v1/forecast?town={encodedTown}&key={encodedApiKey}";

        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            string errorBody = await response.Content.ReadAsStringAsync();
            throw new Exception($"API request failed: {(int)response.StatusCode} {errorBody}");
        }

        return await response.Content.ReadAsStringAsync();
    }
}