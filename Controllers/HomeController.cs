// Author: Sakshi
// Student Number: A00262877
// Import necessary libraries and namespaces
using System.Diagnostics; // Provides classes for interacting with system processes, event logs, and performance counters
using Microsoft.AspNetCore.Mvc; // Provides necessary components for building ASP.NET MVC web applications
using WeatherApp_Sakshi_WebDev.Models; // Importing models specific to WeatherApp_Sakshi_WebDev

// Define the namespace for the WeatherApp_Sakshi_WebDev Controllers
namespace WeatherApp_Sakshi_WebDev.Controllers
{
    // HomeController class, inheriting from Controller class
    

    public class HomeController : Controller
    {
        // Logger for HomeController, used for logging information and errors
        private readonly ILogger<HomeController> _logger;

        // Constructor for HomeController, initializing the logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Action method for handling the Index view
        public IActionResult Index()
        {
            // Returns the Index view
            return View();
        }

        // Action method for handling the About view
        public IActionResult About()
        {
            // Returns the About view
            return View();
        }

        // Action method for handling the Privacy view
        public IActionResult Privacy()
        {
            // Returns the Privacy view
            return View();
        }

        // Action method for handling errors, configured to not cache the response
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Returns the Error view with an ErrorViewModel containing the request ID
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

