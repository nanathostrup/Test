using System;

namespace Services
{
    public class AuthService
    {
        // ❌ Hardcoded admin credentials
        private const string AdminUsername = "admin";
        private const string AdminPassword = "admin";
        // Hardcoded tests with "secret" amd a purely random string and a more purposeful string
        private const string Super_Secret = "password123";
        private const string Super_Secret2 = "kfzwfmZFxhV9e8kSd93IT9xJsJvmHYZC"; //randomly generated here: https://www.random.org/strings/?num=10&len=32&digits=on&upperalpha=on&loweralpha=on&unique=on&format=html&rnd=new

        // ❌ Hardcoded database connection string (contains password)
        private const string ConnectionString =
            "Server=localhost;Database=WeatherDB;User Id=weather_user;Password=WeatherDBPassword!;";

        public bool Login(string username, string password) 
        {
            Console.WriteLine("Connecting to DB with connection string:");
            Console.WriteLine(ConnectionString); // intentional exposure for testing
            if(password == Super_Secret | Super_Secret2)
            {
                Console.WriteLine("Checking if the hardcoded password is detected when 'used' for something i.e. entering if state");
            }
            return username == AdminUsername && password == AdminPassword;
        }
    }
}
