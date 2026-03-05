using System;
using System.Text.Json;
using System.Text;


namespace WeatherStation
{
    public class AuthService
    {
        string path = @"C:\Users\natd\OneDrive - Netcompany\Desktop\test\WeatherSimple\Services\Authentication.json";
        
        //Hardcoded admin
        private const string AdminUsername = "admin";                                                       //USERNAME
        private const string AdminPassword = "admin";                                                       //PASSWORD
        public bool Login(string username, string password) 
        {
            if (username == AdminUsername && password == AdminPassword)
            {
                // Test to see if tools check where passwords and usernames are stored (in plain text)
                var jsonString = JsonSerializer.Serialize(username + " " + password);
                File.WriteAllText(path, jsonString);
                Console.WriteLine(jsonString);
            }
            return username == AdminUsername && password == AdminPassword;
        }

        // Tak til chat:)
        public static async Task LoginAsync()
        {
            using var client = new HttpClient();

            var json = $"{{\"username\":\"{AdminUsername}\",\"password\":\"{AdminPassword}\"}}";
            var content = new StringContent(json, Encoding.UTF8, "json");

            var response = await client.PostAsync("https://example.com/api/login", content);                //virker ikke

            Console.WriteLine($"Status: {response.StatusCode}");
        }
    }
}
