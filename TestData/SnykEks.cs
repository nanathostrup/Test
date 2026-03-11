using System;
using System.Net.Http;
using System.Threading.Tasks;

public class Stormy
{
    private static readonly string ApiKey = "secret_5ebe2294ecd0e0f08eab7690d2a6ee69"; // <-- Oops

    public static async Task<string> GetWeatherAsync(string town)
    {
        string encodedTown = Uri.EscapeDataString(town);
        string encodedApiKey = Uri.EscapeDataString(ApiKey);

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