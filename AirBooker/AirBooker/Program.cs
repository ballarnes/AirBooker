using AirBooker.Data;
using AirBooker.Data.Contexts;
using AirBooker.Data.Entities;
using AirBooker.Data.Repositories;
using AirBooker.Data.Repositories.Contracts;
using AirBooker.Domain.Services;
using AirBooker.Domain.Services.Contracts;
using AirBooker.Infrastructure.Contracts.Services;
using AirBooker.Infrastructure.Services;
using AirBooker.Web;
using Microsoft.EntityFrameworkCore;

namespace AirBooker
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            var configuration = GetConfiguration();
            builder.Services.Configure<AirBookerConfig>(configuration);

			/*builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));*/

            builder.Services.AddTransient<IBookingRepository, BookingRepository>();
            builder.Services.AddTransient<IBookingService, BookingService>();

            builder.Services.AddTransient<IFlightRepository, FlightRepository>();
            builder.Services.AddTransient<IFlightService, FlightService>();

            builder.Services.AddTransient<IAirportRepository, AirportRepository>();
            builder.Services.AddTransient<IAirportService, AirportService>();

            builder.Services.AddTransient<IAirlineRepository, AirlineRepository>();
            builder.Services.AddTransient<IAirlineService, AirlineService>();

            builder.Services.AddTransient<IDocumentService, DocumentService>();

            builder.Services.AddDbContextFactory<ApplicationDbContext>(opts => opts.UseSqlServer(configuration["DefaultConnection"]));
            builder.Services.AddScoped<IDbContextWrapper<ApplicationDbContext>, DbContextWrapper<ApplicationDbContext>>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();

            CreateDbIfNotExists(app);

            app.Run();
		}

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    DbInitializer.Initialize(context).Wait();

                    logger.LogInformation("DB was successfully seeded.");
                }
                catch (Exception ex)
                {
                    logger.LogCritical(ex, "An error occurred creating/seeding the DB!");
                }
            }
        }
    }
}