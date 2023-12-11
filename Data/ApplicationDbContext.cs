// Sakshi
// Student Number: A00262877
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ASP.NET_MVC_WeatherApp.Models;

namespace WeatherApp_Sakshi_WebDev.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
	}
}