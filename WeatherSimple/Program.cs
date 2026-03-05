namespace WeatherStation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Weather Station ===");

            var authService = new AuthService();
            var weatherService = new WeatherStation.WeatherServices();

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

            await AuthService.LoginAsync(); //Selv om funktion ikke virker testes det om den bliver detected når "i brug".
        }
    }
}
