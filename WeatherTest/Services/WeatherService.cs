namespace WeatherStation
{
    public class WeatherServices
    {
        private readonly HttpClient _httpClient = new HttpClient();

        // Hardcoded API key
        private const string OpenWeather = "ea413b8c6e9657e69c24cc2b83e6d894";                              //API KEY generated here: https://home.openweathermap.org/api_keys
        private const string heltvildtsejtvejApiKey = "api_XweVmYIoqSCHxVOb4Q6C1zMFs0O92zPu";               //API KEY (randomized string with prefix "api") generated here: https://generate-random.org/api-keys with prefix 'api'

        // AWS  
        private const string AwsAccessKey = "AKIAIOSFODNN73XAMPL3";                                         //CLOUD CREDENTIAL - Keyboard smashing
        private const string AwsSecretKey = "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";                     //CLOUD CREDENTIAL
        private const string AwsSessionToken = "IQoJb3JpZ2luX2VjEFAaCXVzLXdlc3QtMiJIMEYCIQDFAK3";           //CLOUD CREDENTIAL
        private const string AwsSessionToken2 = 
        "IQoJb3JpZ2luX2VjEDoaCXVzLWVhc3QtMiJIMEYCIQDQh4gelDqno96q39RwiPT5x7K7SyVOSmeDpUMd9SthWAIhAP5tT81Cb+Rb2zN85delmYB4KECmW1uL7Tr36C/M2GaJKr0DCKP//////////wEQARoMNjY2MzU5NzY0NTI4Igyu9F2yAqZN3dG0q9YqkQMVrg/4mCJjDxg0QmplU581Z2P8LGhGfr9vgei6SaONhhfks5Kt9Ikbh61G9UiQ3SXgPLbHjOfTUueaIIcBz1Y3LcW+WajtfsGfB8CqT76lkJLtkvl+1KjSCVn6k+/K/iWgr3Zc1Ej+qT2djTH4x1OWFNS6i6iCtlUy/Z6i3P2fziHGsEmafkH3ict+07dFb3DA2aRnUhnaCHfQDNd/5ub70oILwB4UgtgGNkbM9SE/NxKgPZY9qIktYifqcgfDyYMYHlvY9XEc0UT2jfaQKDYVgMCdsdsW5mkoBYzLRisQhKxjfwaBpkRtdW8dEHFAG04eV4JSAbOSat3bgUwahATGizOdsMz/qhnS9qzShQGgSR6OU6pDDUtuHCGh0sgwrjsZ+bGDfzkw5Sy3JhjQpozfinCsAmDZ1t3nX6llw9OR9B2mdDHCeccsWGwjIvmprs21FtgjDuKGzaAET6HgQAR+pkFUgxBWVmZArtck1ziG21FEN8pFR75rOgxSkQ3yEZeDZkIIZ/aJnABGvbC3Fbq9ATD6ycuKBjqlAaGPeFKzdCR1dBh4sHQVHejXNegWWZV72n4MLyZx2FE9wLUfPGXXW+pYZg4SySvN0Z4OnGoYdlO/pjKvdRa507mSD8N8EhkwgpJMatFobJb0hsz7GY5flutVSkDfBDYkU91vpl7YCJ5rlvuR0I6iWe+K7smYj5hzm16YokWsRQ4EeWHo0peEJuqTZrZt/U4gHVsFpG44V8Yb6iRdZL78E+5xcgjeFw==";
        
        // AZURE
        private const string AzureStorage =                                                                 //CLOUD STORAGE
        "DefaultEndpointsProtocol=https;AccountName=fakeaccount;AccountKey=FAK3K3Y1234567890ABCDEF==;EndpointSuffix=core.windows.net";

        // GOOGLE
        private const string GoogleApiKey = "AIzaSyDaGmWKa4JsXZ-HjGw7ISLn_3namBGewQe";                      //API - found as an example here: https://docs.cloud.google.com/docs/authentication/api-keys                                                                                              //API

        // STRIPE
        private const string StripeExample = "sk_live_C8YfyXfzocnRZNE36yzd7Pg3Wl0aqCad";                    //API - (sk=secret key) randomly generated here: https://randomkeygen.com/api-key
        private const string heltvildtsejtvejApiKey2 = "sk_live_skfikf5682lfjas896dsndhfuek9hy654";         //API - keyboard smashing
        private const string StripeServer = "sk_t3st_4RxUm8rtZ0phcTFLbHvkTJD5";                             //API - found as an example here (modified e = 3): https://docs.stripe.com/keys
        private const string StripeClient = "pk_t3st_4RxUQ9rE2xn8vIbplcQlCLQN";                             //API - (pk=public key) found as an example here (modified e = 3): https://docs.stripe.com/keys
        // GITHUB
        private const string GitHubClassic = "ghp_abcdefghijklmnopqrstuvwxyz123456";                        //ACCESS TOKEN
        private const string GitHubFineGrained = "github_pat_11ABCDEF1234567890FAK3";                       //ACCESS TOKEN

        // GITLAB
        private const string GitLabToken = "glpat-abcdefghijklmnopqrstuvwxyz";                              //ACCESS TOKEN

        // TELEGRAM
        private const string TelegramBotToken =                                                              //API
            "123456789:AAFAK3-telegram-bot-token-3xampl3";

        // TWILIO
        private const string TwilioAuthToken = "your_auth_token_32charslong";                                //API

        // SENDGRID
        private const string SendGridApiKey = "SG.fak3_sendgrid_api_k3y_3xampl3";                            //API

        // JWT
        private const string JwtSecret = "SuperLongJWTSigningSecretK3y123456789";                            //SIGNiNG SECRET
        private const string JwtExample =                                                                    //AUTHENTICATION TOKEN
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
            "eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkFkbWluIn0." +
            "dBjftJeZ4CVP-mB92K27uhbUJU1p1r_wW1gFWFOEjXk";
        private const string JwtSigning = "WOlJeDRXzIDR9N0xXrQjIOYNoMYrlEvMz3HF91RTy";                      //SIGNING SECRET generated here: https://jwtsecretkeygenerator.com/
        private const string JwtSigningSecret = "WOlJeDRXzIDR9N0xXrQjIOYNoMYrlEvMz3HF91RTy";                //sIGNING SECRET (same secret as above but different variable name, with "secret" in name)

        // SSH
        private const string SSHKey = @"
-----BEGIN RSA PRIVATE KEY-----
v5PkYJ0atm3iKr9aiWgFJYmpuwhsti48AmdyxKykzsM
-----END OPENSSH PRIVATE KEY-----";                                                                         // SSH KEY - generated in terminal using command: ssh-keygen -t
        
        public async Task<string> GetWeatherAsync(string city)
        {
            string url =
             $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={OpenWeather}&units=metric"; // Actually working website fetching weather forecast
            try
            {
                var response = await _httpClient.GetStringAsync(url);
                return $"Weather data received successfully: \n\n{response}";
            }
            catch (Exception ex)
            {
                return $"Error retrieving weather data: {ex.Message}";
            }        
        }
        public void TestLeak()
        {
            // Testing to see if a leak is detected with tools
            Console.WriteLine(OpenWeather);
            Console.WriteLine(heltvildtsejtvejApiKey);
            Console.WriteLine(AwsAccessKey);
            Console.WriteLine(AwsSecretKey);
            Console.WriteLine(AwsSessionToken);
            Console.WriteLine(AwsSessionToken2);
            Console.WriteLine(AzureStorage);
            Console.WriteLine(GoogleApiKey);
            Console.WriteLine(FirebaseConfig);
            Console.WriteLine(StripeLive);
            Console.WriteLine(StripeTest);
            Console.WriteLine(heltvildtsejtvejApiKey2);
            Console.WriteLine(GitHubClassic);
            Console.WriteLine(GitHubFineGrained);
            Console.WriteLine(GitLabToken);
            Console.WriteLine(SlackBot);
            Console.WriteLine(SlackWebhook);
            Console.WriteLine(DiscordBotToken);
            Console.WriteLine(TelegramBotToken);
            Console.WriteLine(TwilioAuthToken);
            Console.WriteLine(SendGridApiKey);
            Console.WriteLine(JwtSecret);
            Console.WriteLine(JwtExample);
            Console.WriteLine(JwtSigning);
            Console.WriteLine(JwtSigningSecret);
            Console.WriteLine(SshPrivateKey);
            Console.WriteLine(Pkcs8PrivateKey);
        }
    }
}