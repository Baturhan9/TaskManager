using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

        public static IServiceCollection AddMyAuthentication(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
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
                        ValidIssuer = configuration.GetValue<string>(SystemEnvironments.JWT_ISSUER),
                        ValidAudience = configuration.GetValue<string>(
                            SystemEnvironments.JWT_AUDIENCE
                        ),
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                configuration.GetValue<string>(SystemEnvironments.JWT_KEY)
                            )
                        ),
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "Developer",
                    policy =>
                        policy.RequireAssertion(ctx =>
                            ctx.User.IsInRole(UserRoles.Admin)
                            || ctx.User.IsInRole(UserRoles.Senior)
                            || ctx.User.IsInRole(UserRoles.Tester)
                            || ctx.User.IsInRole(UserRoles.Developer)
                        )
                );

                options.AddPolicy(
                    "Tester",
                    policy =>
                        policy.RequireAssertion(ctx =>
                            ctx.User.IsInRole(UserRoles.Admin)
                            || ctx.User.IsInRole(UserRoles.Senior)
                            || ctx.User.IsInRole(UserRoles.Tester)
                        )
                );

                options.AddPolicy(
                    "Senior",
                    policy =>
                        policy.RequireAssertion(ctx =>
                            ctx.User.IsInRole(UserRoles.Admin)
                            || ctx.User.IsInRole(UserRoles.Senior)
                        )
                );

                options.AddPolicy("Admin", policy => policy.RequireRole(UserRoles.Admin));
            });
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
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme,
                    },
                };

                options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement { { securityScheme, Array.Empty<string>() } }
                );

                options.SchemaFilter<EnumSchemaFilter>();
            });
            return services;
        }
    }
}
