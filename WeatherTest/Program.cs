namespace WeatherStation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var authService = new AuthService();
            var weatherService = new WeatherServices();
            var pwAuthService = new PasswordAuthenticationService();

            //Testing the leak of secrets in "action" not just as an unused function
            authService.TestLeak();
            weatherService.TestLeak();

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            //Test of another way of authentication - sending credentials to a hhtp thing.
            string path = @"C:\Users\natd\OneDrive - Netcompany\Desktop\test\WeatherSimple\Services\PWsAuthentication.json";
            await PasswordAuthenticationService.LoginAsync("path");

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