using System;

namespace Services
{
    public class AuthService
    {
        //Hardcoded admin
        private const string AdminUsername = "admin";
        private const string AdminPassword = "admin";
        // Hardcoded tests with "secret" amd a purely random string and a more purposeful string
        private const string Super_Secret = "password123";
        private const string Super_Secret2 = "kfzwfmZFxhV9e8kSd93IT9xJsJvmHYZC"; //randomly generated here: https://www.random.org/strings/?num=10&len=32&digits=on&upperalpha=on&loweralpha=on&unique=on&format=html&rnd=new
        // ===== DATABASE URIs =====
        private const string MySqlUri = "mysql://weather_user:WeatherDBPassword!@localhost:3306/weatherdb";

        private const string PostgresUri = "postgresql://pg_user:PgPassword123!@localhost:5432/weatherdb";

        private const string MongoUri = "mongodb://mongoUser:mongoPass123@localhost:27017/weatherdb";

        private const string RedisUri = "redis://:RedisPassword123@localhost:6379";

        // ===== AWS =====
        private const string AwsAccessKey = "AKIA3XAMPLEFAK3K3Y123";
        private const string AwsSecretKey = "wJalrXUtnFEMI/K7MDENG/bPxRfiCYFAK3K3Y";
        private const string AwsSessionToken = "IQoJb3JpZ2luX2VjEFAaCXVzLXdlc3QtMiJIMEYCIQDFAKe";

        // ===== AZURE =====
        private const string AzureStorage =
            "DefaultEndpointsProtocol=https;AccountName=fakeaccount;AccountKey=FAK3K3Y1234567890ABCDEF==;EndpointSuffix=core.windows.net";

        // ===== GOOGLE =====
        private const string GoogleApiKey = "AIzaSyD-FAK3-K3Y-123456789";
        private const string FirebaseConfig = @"{
            ""apiKey"": ""AIzaSyD-FAK3-K3Y-123456789"",
            ""authDomain"": ""weatherapp.firebaseapp.com"",
            ""projectId"": ""weatherapp"",
            ""storageBucket"": ""weatherapp.appspot.com"",
            ""messagingSenderId"": ""1234567890"",
            ""appId"": ""1:1234567890:web:abcdef123456""
        }";

        // ===== STRIPE =====
        private const string StripeLive = "sk_live_51NFAK3K3Y123456789";
        private const string StripeTest = "sk_test_51NFAK3K3Y123456789";

        // ===== GITHUB =====
        private const string GitHubClassic = "ghp_abcdefghijklmnopqrstuvwxyz123456";
        private const string GitHubFineGrained = "github_pat_11ABCDEF1234567890FAK3";

        // ===== GITLAB =====
        private const string GitLabToken = "glpat-abcdefghijklmnopqrstuvwxyz";

        // ===== SLACK =====
        private const string SlackBot = "xoxb-123456789012-abcdefghijklmnopqrstuvwxyz";
        private const string SlackWebhook =
            "https://hooks.slack.com/services/T00000000/B00000000/FAK3WebhookKey";

        // ===== DISCORD =====
        private const string DiscordBotToken =
            "MTIzNDU2Nzg5MDEyMzQ1Njc4.YFAK3.discordToken3xampl3";

        // ===== TELEGRAM =====
        private const string TelegramBotToken =
            "123456789:AAFAK3-telegram-bot-token-3xampl3";

        // ===== TWILIO =====
        private const string TwilioSid = "ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
        private const string TwilioAuthToken = "your_auth_token_32charslong";

        // ===== SENDGRID =====
        private const string SendGridApiKey = "SG.fak3_sendgrid_api_k3y_3xampl3";

        // ===== JWT =====
        private const string JwtSecret = "SuperLongJWTSigningS3cretK3y123456789";
        private const string JwtExample =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
            "eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkFkbWluIn0." +
            "dBjftJeZ4CVP-mB92K27uhbUJU1p1r_wW1gFWFOEjXk";

        // ===== SSH PRIVATE KEY =====
        private const string SshPrivateKey = @"
-----BEGIN OPENSSH PRIVATE KEY-----
b3BlbnNzaC1rZXktdjEAAAAABG5vbmUAAAAEbm9uZQAAAAAAAAABAAAAlwAAAAdz
c2gtcnNhAAAAAwEAAQAAAYEAFAKEKEYDATAEXAMPLE123456789
-----END OPENSSH PRIVATE KEY-----";

        // ===== PKCS8 PRIVATE KEY =====
        private const string Pkcs8PrivateKey = @"
-----BEGIN PRIVATE KEY-----
MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDFAKEKEYDATA
-----END PRIVATE KEY-----";

        // ===== BASE64 SECRET =====
        private const string Base64Secret =
            "U3VwZXJTZWNyZXRLZXlGb3JUZXN0aW5nMTIz";

        // ===== HEX SECRET =====
        private const string HexSecret =
            "4a6f686e446f654150495365637265744b6579";

        // ===== OBFUSCATED SECRET =====
        private static string Obfuscated =
            "ghp_" + "abcd" + "efgh" + "ijkl" + "mnop" + "qrst" + "uvwx";

 
        // Hardcoded database connection string (contains password)
        private const string ConnectionString =
            "Server=localhost;Database=WeatherDB;User Id=weather_user;Password=WeatherDBPassword!;";

        public bool Login(string username, string password) 
        {
            Console.WriteLine("Testing secret exposure...");

            Console.WriteLine(MySqlUri);
            Console.WriteLine(PostgresUri);
            Console.WriteLine(MongoUri);
            Console.WriteLine(RedisUri);

            Console.WriteLine(AwsAccessKey);
            Console.WriteLine(AwsSecretKey);
            Console.WriteLine(AzureStorage);

            Console.WriteLine(GitHubClassic);
            Console.WriteLine(GitHubFineGrained);
            Console.WriteLine(GitLabToken);

            Console.WriteLine(SlackBot);
            Console.WriteLine(SlackWebhook);
            Console.WriteLine(DiscordBotToken);
            Console.WriteLine(TelegramBotToken);

            Console.WriteLine(TwilioSid);
            Console.WriteLine(TwilioAuthToken);
            Console.WriteLine(SendGridApiKey);

            Console.WriteLine(JwtSecret);
            Console.WriteLine(JwtExample);

            Console.WriteLine(SshPrivateKey);
            Console.WriteLine(Pkcs8PrivateKey);

            Console.WriteLine(Base64Secret);
            Console.WriteLine(HexSecret);
            Console.WriteLine(Obfuscated);
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("------------------------------------------");


            Console.WriteLine("Connecting to DB with connection string:");
            Console.WriteLine(ConnectionString); // intentional exposure for testing
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine("Testing Username and password usage");
            if(password == Super_Secret | Super_Secret2)
            {
                Console.WriteLine("Checking if the hardcoded password is detected when 'used' for something i.e. entering if statement");
            }

            return username == AdminUsername && password == AdminPassword;
        }
    }
}
