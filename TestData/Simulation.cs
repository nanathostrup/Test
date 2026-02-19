namespace test
{
    class Simulation
    {
        public bool func(string password)
        {
            
            // A function to check if the userinput matches the correct password

            string userInput;

            Console.WriteLine("Please give your password here:");
            userInput = Console.ReadLine();

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