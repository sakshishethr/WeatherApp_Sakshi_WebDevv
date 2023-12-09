using System.Net; // Provides a variety of classes and utilities for working with network protocols.
using System.Text.Json.Serialization; // Provides attributes and utilities for object serialization and deserialization to/from JSON.
using ASP.NET_MVC_WeatherApp.Models; // Namespace for models specific to the ASP.NET MVC Weather App.
using Microsoft.AspNetCore.Mvc; // Provides classes and attributes necessary for creating controllers and action results in MVC.
using WeatherApp_Sakshi_WebDev.Class; // Namespace for specific classes in the WeatherApp_Sakshi_WebDev project.
using Newtonsoft.Json; // A popular library for JSON processing.
using System.Text; // Provides classes for working with text, primarily in the form of character strings.
using System; // The fundamental namespace for basic .NET data types and utilities.
using System.Collections.Generic; // Contains interfaces and classes that define various collections of objects.
using System.Linq; // Provides classes and interfaces that support LINQ queries.
using System.Web; // Provides a set of services and features for web applications.
using System.IO; // Contains types for reading and writing to files and data streams.
using static System.Net.WebRequestMethods; // Contains constants for HTTP verbs.
using Microsoft.Extensions.Primitives; // Provides basic utility classes and primitive data types.
using Humanizer; // A library that manipulates and displays strings, enums, dates, times, timespans, numbers and quantities.
using System.Security.Cryptography.X509Certificates; // Provides classes for working with X.509 certificates.
using Newtonsoft.Json.Linq; // Provides LINQ to JSON capabilities.
using System.Globalization; // Provides classes and methods for working with culture-specific information.
using Microsoft.AspNetCore.Mvc.Rendering; // Provides classes for rendering HTML in views.
using System.Net.Http.Headers; // Provides support for collections of HTTP headers.
using Microsoft.AspNetCore.Authorization; // Provides classes and interfaces for authorization.

// Author: Sakshi
// Student Number: A00262877
namespace ASP.NET_MVC_WeatherApp.Controllers
{
    // Controller for OpenWeatherMap functionality.
    public class OpenWeatherMapController : Controller
    {
        // HttpClient instance for sending HTTP requests.
        HttpClient client = new HttpClient();

        // Authorizes access to the Index action.
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return View(); // Returns the Index view.
        }

        // Method to create and return an instance of OpenWeatherMap with preset latitude and longitude.
        public OpenWeatherMap FillCity()
        {
            OpenWeatherMap openWeatherApp = new OpenWeatherMap
            {
                lat = "33.44",
                lon = "-94.04"
            };

            return openWeatherApp;
        }


        [HttpPost]
		public async Task<Geocoder> ConvertGeocode(string cityName, string stateCode, string countryCode)
		{
            Geocoder geocoder = new Geocoder();			
            if (cityName != null && stateCode != null && countryCode != null)
			{
				string apiKey = "38c0927bf0a04a186a70bcaa540afe92";
				string url = $"http://api.openweathermap.org/geo/1.0/direct?q={cityName},{stateCode},{countryCode}&limit={1}&appid={apiKey}";

				HttpResponseMessage responseBody = await client.GetAsync(url);

				if(responseBody.IsSuccessStatusCode)
				{
                    string apiResponse = await responseBody.Content.ReadAsStringAsync();
				
					JArray jsonResults = JArray.Parse(apiResponse);
					if (jsonResults.Count > 0)
					{
						geocoder.lat = (string)jsonResults[0]["lat"];
						geocoder.lon = (string)jsonResults[0]["lon"];
						geocoder.cityName = (string)jsonResults[0]["name"];
						geocoder.countryCode = (string)jsonResults[0]["country"];
						geocoder.stateCode = (string)jsonResults[0]["state"];

						return geocoder;
					}
					else
					{
						return geocoder;
						//put error message in this path
					}
				}
				else
				{
                    return geocoder;
                }
            }
			else
			{
				return geocoder;
			}
        }

		[HttpPost]
		public async Task<ActionResult> GetWeather(string cityName, string stateCode, string countryCode)
		{
			OpenWeatherMap openWeatherMap = FillCity();			
			Geocoder geocoder = await ConvertGeocode(cityName, stateCode, countryCode);

			if (geocoder.lat != null && geocoder.lon != null)
			{
				
				string apiKey = "38c0927bf0a04a186a70bcaa540afe92";

				string url = $"https://api.openweathermap.org/data/3.0/onecall?lat={geocoder.lat}&lon={geocoder.lon}&exclude=hourly,minutely,daily,alerts,timezone,timezone_offset&appid={apiKey}&units=imperial";

                HttpResponseMessage responseBody = await client.GetAsync(url);

				if(responseBody.IsSuccessStatusCode)
				{
					string apiResponse = await responseBody.Content.ReadAsStringAsync();
					Root rootObject = JsonConvert.DeserializeObject<Root>(apiResponse);
					
					var weatherIcon = $"https://openweathermap.org/img/wn/{rootObject.current.weather[0].icon}@2x.png";
					
					StringBuilder sb = new StringBuilder();
					sb.Append("<table><tr><th>Weather Description</th></tr>");
					sb.Append("<tr><td>Coordinates: </td><td>" + rootObject.lat +", "+ rootObject.lon + "</td></tr>");
					sb.Append("<tr><td>City/State: </td><td>" + geocoder.cityName +", "+ geocoder.stateCode + "</td></tr>");
					sb.Append("<tr><td>Country/Major City: </td><td>" + rootObject.timezone + "</td></tr>");
					sb.Append("<tr><td>Wind: </td><td>" + rootObject.current.wind_speed + " MPH</td></tr>");
					sb.Append("<tr><td>Temperature: </td><td>" + rootObject.current.temp + " °F</td></tr>");
					sb.Append("<tr><td>Humidity: </td><td>" + rootObject.current.humidity + "</td></tr>");
					sb.Append("<tr><td>Weather: </td><td>" + rootObject.current.weather[0].description.ToUpper() +" "+ $"<img src='{weatherIcon}'  >" + "</td></tr>");
					sb.Append("</table>");                   
                    openWeatherMap.apiResponse = sb.ToString();
				}								
			}
            else
			{
				return View("Index");
			}
			return RedirectToAction("Forecast", openWeatherMap);
		}
		public IActionResult Forecast(OpenWeatherMap openWeatherMap)
		{			
			return View(openWeatherMap);
		}
	}
}
