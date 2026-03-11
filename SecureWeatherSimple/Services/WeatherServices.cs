using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


//FRA CORTEX
//STORAGE OF API KEY SECURELY


[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string _apiKey;

    // Inject IConfiguration via the constructor
    public WeatherForecastController(IConfiguration configuration)
    {
        _configuration = configuration;
        
        // Retrieve the secret. It will come from User Secrets (local) or Key Vault (prod).
        _apiKey = _configuration["ApiKey"] ?? "DefaultKeyIfNotSet"; 
    }

    [HttpGet]
    public IActionResult Get()
    {
        // Use the API key
        if (_apiKey.StartsWith("my-local-dev"))
        {
            return Ok($"Successfully retrieved the LOCAL API Key: {_apiKey}");
        }
        else
        {
            // In a real app, you wouldn't return the key.
            // This is just for demonstration.
            return Ok("Successfully retrieved the PRODUCTION API Key from Key Vault.");
        }
    }
}