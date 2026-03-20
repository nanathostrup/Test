using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherStation
{
    public class WeatherExtension
    {
        public void useVariable(string secret)
        {
            Console.WriteLine(secret); //used to test the dataflow analysis, if it can catch a use in another file
        }
    }
}