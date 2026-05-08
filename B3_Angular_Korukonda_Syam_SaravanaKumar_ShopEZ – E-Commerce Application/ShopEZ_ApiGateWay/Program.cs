using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ShopEZ_ApiGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Load ocelot.json — contains all route configurations
            builder.Configuration.AddJsonFile(
                "ocelot.json",
                optional: false,
                reloadOnChange: true);

            // CORS Configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            // Read JWT Key from appsettings.json
            var jwtKey = builder.Configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("Jwt:Key is not configured.");

            // JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", options =>
            {
                // Token validation rules
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey))
                };

                // Custom JWT responses
                options.Events = new JwtBearerEvents
                {
                    // 401 Unauthorized
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(
                            "{\"success\":false,\"message\":\"You are not logged in. Please login first.\",\"data\":null}");
                    },

                    // 403 Forbidden
                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(
                            "{\"success\":false,\"message\":\"Access denied. Only Admins can access this.\",\"data\":null}");
                    }
                };
            });

            // Authorization
            builder.Services.AddAuthorization();

            // Register Ocelot
            builder.Services.AddOcelot();

            var app = builder.Build();

            // Enable CORS
            app.UseCors("AllowAngularApp");

            // Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Ocelot Middleware
            await app.UseOcelot();

            app.Run();
        }
    }
}