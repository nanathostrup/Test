using System.Security.Cryptography.X509Certificates;

namespace test
{
    class Simulation
    {
        public string password;
        public bool func()
        {
            
            // A function to check if the userinput matches the correct password
            password = "123";

            Console.WriteLine("Please give your password here:");
            string userInput = Console.ReadLine();

            if (userInput == password)
            {
                Console.WriteLine(":)");
                return true;
            }
            else
            {
                Console.WriteLine(":(");
                return false;            
            }
        }

    }



}