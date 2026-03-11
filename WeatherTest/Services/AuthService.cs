using System.Text.Json;


namespace WeatherStation
{
    public class AuthService
    {
        string path = @"C:\Users\natd\OneDrive - Netcompany\Desktop\test\WeatherSimple\Services\Authentication.json";
        
        //Hardcoded admin
        private const string AdminUsername = "admin";                                                       //USERNAME
        private const string AdminPassword = "admin";                                                       //PASSWORD
    
        // Hardcoded tests with "secret" in the variable name with a purely random string and a more unrandom string
        private const string Super_Secret = "password123";                                                  //PASSWORD
        private const string Super_Secret2 = "kfzwfmZFxhV9e8kSd93IT9xJsJvmHYZC";                            //PASSWORD - randomly generated here: https://www.random.org/strings/?num=10&len=32&digits=on&upperalpha=on&loweralpha=on&unique=on&format=html&rnd=new
        
        // Encodings with Base64 and hex
        private const string Base64Secret = "YmFya2ZpZnRoaWNlbWVhbnRzdWNoc2Nob29sYnVzaGs=";                 //ENCODING - Randomly generated here: https://www.convertsimple.com/random-base64-generator/
        private const string HexSecret = "4a6f686e446f654150495365637265744b6579";                          //ENCODING - Randomly generated here: https://www.browserling.com/tools/random-hex
 
        // Hardcoded database connection string (contains password)
        private const string ConnectionString =
            "Server=localhost;Database=WeatherDB;User Id=weather_user;Password=WeatherDBPassword!;";

        // Database URIs that contain passwords
        private const string MySqlUri = "mysql://weather_user:WeatherDBPassword!@localhost:3306/weatherdb"; //PASSWORD INSIDE
        private const string PostgresUri = "postgresql://pg_user:PgPassword123!@localhost:5432/weatherdb";  //PASSWORD INSIDE
        private const string MongoUri = "mongodb://mongoUser:mongoPass123@localhost:27017/weatherdb";       //PASSWORD INSIDE
        private const string RedisUri = "redis://:RedisPassword123@localhost:6379";                         //PASSWORD INSIDE

       
        public bool Login(string username, string password) 
        {
            // Testing to see when input passwords are compared to hardcoded plaintext password and "logged in"/given rights
            if (username == AdminUsername && password == AdminPassword)
            {
                // Test to see if tools check where passwords and usernames are stored (in plain text)
                var jsonString = JsonSerializer.Serialize(username + " " + password);
                File.WriteAllText(path, jsonString);
                Console.WriteLine(jsonString);
            }
            return username == AdminUsername && password == AdminPassword;
        }

        public void TestLeak()
        {
            // Test to see if a leak of a password is detected by tools
            Console.WriteLine(AdminUsername);
            Console.WriteLine(AdminPassword);
            Console.WriteLine(Super_Secret);
            Console.WriteLine(Super_Secret2);
            Console.WriteLine(Base64Secret);
            Console.WriteLine(HexSecret);
            Console.WriteLine(ConnectionString);
            Console.WriteLine(MySqlUri);
            Console.WriteLine(PostgresUri);
            Console.WriteLine(MongoUri);
            Console.WriteLine(RedisUri);
        }
    }
}
