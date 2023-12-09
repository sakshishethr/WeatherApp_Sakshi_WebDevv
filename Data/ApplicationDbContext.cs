using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeatherApp_Sakshi_WebDev.Models;

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