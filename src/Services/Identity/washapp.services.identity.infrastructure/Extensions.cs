using System.Reflection;
using washapp.services.identity.application.Commands;
using washapp.services.identity.application.Commands.Handlers;
using washapp.services.identity.application.DTO;
using washapp.services.identity.application.Services;
using washapp.services.identity.application.Services.Identity;
using washapp.services.identity.domain.Entities;
using washapp.services.identity.domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using washapp.services.identity.infrastructure.Auth.Settings;
using washapp.services.identity.infrastructure.Data;
using washapp.services.identity.infrastructure.Exceptions.Middleware;
using washapp.services.identity.infrastructure.Repositories;
using washapp.services.identity.infrastructure.Services;
using washapp.services.identity.infrastructure.Validators;


namespace washapp.services.identity.infrastructure
{
    public static class Extensions
    { 
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
            AuthenticationSettings authenticationSettings)
        {
            services.AddDbContext<WashAppDbContext>(options=>options.UseSqlServer(configuration.GetConnectionString("WashAppDb")));
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<ErrorHandlingMiddleware>();
            services.AddTransient<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddTransient<IValidator<ChangePassword>, ChangePasswordValidator>();
            services.AddTransient<IJwtProvider, JwtProvider>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddSingleton<AuthenticationSettings>(authenticationSettings);

        }

        public static void UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
        
        public static void PrepDatabase(this IApplicationBuilder app)
        {
            using (var dbContext = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<WashAppDbContext>())
            {
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }

                var user = dbContext.Users.FirstOrDefault(x => x.Login == "WashAppAdmin");
                if (user is null)
                {
                    var userId = Guid.NewGuid();
                    user = new User(
                        userId,
                        "WashAppAdmin",
                        "WashAppAdmin",
                        "WashAppAdmin",
                        DateTime.UtcNow,
                        Roles.Admin);

                    var passwordHasher = new PasswordHasher<User>();
                    var hashedPassword = passwordHasher.HashPassword(user, "pa$$word!");
                    user.SetPassword(hashedPassword);
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                }

            }
        }
    }
}
