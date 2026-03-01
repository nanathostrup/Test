namespace WeatherStation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var authService = new AuthService();
            var weatherService = new WeatherServices();

            //Testing the leak of secrets in "action" not just as an unused funciton
            authService.TestLeak();
            weatherService.TestLeak();

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (!authService.Login(username, password))
            {
                Console.WriteLine("Authentication failed.");
                return;
            }

            Console.Write("Enter city: ");
            string city = Console.ReadLine();

            string weather = await weatherService.GetWeatherAsync(city);
            Console.WriteLine(weather);
        }
    }
}