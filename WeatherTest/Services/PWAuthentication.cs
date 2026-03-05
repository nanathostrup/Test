using System.Text.Json;
using System.Text.Encodings;
using System.Text;
using System;


namespace WeatherStation
{
    public class PasswordAuthenticationService
    {        
        //Hardcoded admin
        private const string AdminUsername = "admin";                                                       //USERNAME
        private const string AdminPassword = "admin";                                                       //PASSWORD
        //Fra chatten hehe :)
        public static async Task LoginAsync(string path)
        {
            using var client = new HttpClient();

            var json = $"{{\"username\":\"{AdminUsername}\",\"password\":\"{AdminPassword}\"}}";
            var content = new StringContent(json, Encoding.UTF8, "json");

            var response = await client.PostAsync("https://example.com/api/login", content);                //virker ikke

            Console.WriteLine($"Status: {response.StatusCode}");
        }

    }
}
