// Author: Sakshi
// Student Number: A00262877

// Required namespace for working with MVC SelectList
using Microsoft.AspNetCore.Mvc.Rendering;

// Namespace declaration for the Weather App
namespace ASP.NET_MVC_WeatherApp.Models
{
    // Definition of the Geocoder class
    public class Geocoder
    {
        // Latitude of the location
        public string lat { get; set; }

        // Longitude of the location
        public string lon { get; set; }

        // Name of the city
        public string cityName { get; set; }

        // Country code (e.g., US for United States)
        public string countryCode { get; set; }

        // State or region code (if applicable)
        public string stateCode { get; set; }

        // SelectList containing available countries (useful for dropdowns in views)
        public SelectList countries { get; set; }

        // SelectList containing available states (useful for dropdowns in views)
        public SelectList states { get; set; }

        // SelectList containing available cities (useful for dropdowns in views)
        public SelectList cities { get; set; }
    }
}
