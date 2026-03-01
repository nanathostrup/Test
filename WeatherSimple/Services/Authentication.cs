using System;
using System.Text.Json;


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
    }
}
