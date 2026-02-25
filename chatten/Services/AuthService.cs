using System;

namespace Services
{
    public class AuthService
    {
        // ❌ Hardcoded admin credentials
        private const string AdminUsername = "admin";
        private const string AdminPassword = "admin";

        // ❌ Hardcoded database connection string (contains password)
        private const string ConnectionString =
            "Server=localhost;Database=WeatherDB;User Id=weather_user;Password=WeatherDBPassword!;";

        public bool Login(string username, string password)
        {
            Console.WriteLine("Connecting to DB with connection string:");
            Console.WriteLine(ConnectionString); // intentional exposure for testing

            return username == AdminUsername && password == AdminPassword;
        }
    }
}