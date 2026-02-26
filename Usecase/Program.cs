namespace PracticeAPI1.Controllers
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("testing");            
            var controller = new WeatherForecastWithOpenWeatherController();
            controller.GetWeatherByCity("London");
        }
    }

}