using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Api.Classes;
using TaskManager.Api.Classes.SwaggerFilters;
using TaskManager.Consts;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Repositories;
using TaskManager.Services;

namespace TaskManager.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddModelsServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );
            });
            return services;
        }

        public static IServiceCollection AddMyAuthentication(this IServiceCollection services)
        {
            // can you fix this code
            // services
            //     .AddIdentityCore<User>(options =>
            //     {
            //         options.User.RequireUniqueEmail = true;
            //         options.Password.RequireDigit = false;
            //         options.Password.RequiredLength = 6;
            //         options.Password.RequireNonAlphanumeric = false;
            //         options.Password.RequireLowercase = false;
            //         options.Password.RequireUppercase = false;
            //     })
            //     .AddEntityFrameworkStores<TaskManagerContext>()
            //     .AddDefaultTokenProviders();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "Jwt:Issuer",
                        ValidAudience = "Jwt:Audience",
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes("Jwt:Key")
                        ),
                    };
                });

            services.AddAuthorization();
            return services;
        }

        public static IServiceCollection ConfigureDataBase(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            string connectionString = configuration.GetValue<string>(
                SystemEnvironments.POSTGRES_CONNECTION_STRING
            )!;

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(
                    nameof(connectionString),
                    "Connection string is null or empty"
                );

            services.AddDbContext<TaskManagerContext>(options =>
                options.UseNpgsql(connectionString)
            );
            return services;
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // Для System.Text.Json
                options.SchemaFilter<EnumSchemaFilter>();
            });
            return services;
        }
    }
}
