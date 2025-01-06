using CatAdoptionMobileApp.Api.Services;
using CatAdoptionMobileApp.Api.Services.Interfaces;
using CatAdoptionMobileApp.EntityFramework.DbContexts;
using CatAdoptionMobileApp.EntityFramework.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace CatAdoptionMobileApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if(connectionString == null)
            {
                throw new InvalidOperationException("Connection string not found");
            }

            // Add authentication
            builder.Services.AddAuthentication(options =>
            {
                // Set the default authentication scheme to JWT
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // Set the default challenge scheme to JWT
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                // Add JWT bearer
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = TokenService.GetTokenValidationParameters(builder.Configuration);
                });

            // Add db context
            builder.Services.AddDbContext<CatAdoptionDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add repositories
            builder.Services.AddTransient<CatRepository>()
                .AddTransient<UserRepository>()
                .AddTransient<UserCatRepository>();

            // Add services
            builder.Services.AddTransient<IUserCatService, UserCatProvider>()
                .AddTransient<ICatService, CatProvider>()
                .AddTransient<IAuthService, AuthProvider>()
                .AddTransient<TokenService>();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                // Migration DB
                ApplyDbMigrations(app.Services);

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.MapControllers();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }


        /// <summary>
        /// Apply database migrations and seed cats in the database (test data) 
        /// </summary>
        /// <param name="serviceProvider"></param>
        static void ApplyDbMigrations(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CatAdoptionDbContext>();

                if(dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }

                dbContext.SeedCats();
            }
        }
    }
}
