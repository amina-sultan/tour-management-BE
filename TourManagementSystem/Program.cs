using Microsoft.EntityFrameworkCore;
using TourManagementSystem.Data;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TourManagementSystem.Repositories;
using TourManagementSystem.Services;
using TourManagementSystem.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            }

        );

        builder.Services.AddCors(c =>
        {
            c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

        builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

        var jwtKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
        builder.Services.AddAuthentication(options =>
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
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(jwtKey)
            };
            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        error = "You are an unauthorized user."
                    }));
                },
                OnForbidden = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        error = "You do not have permission to access this resource."
                    }));
                }
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("admin"));
        });

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<BookingService>();
        builder.Services.AddScoped<BlogRepository>();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
 

        app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        {
            app.UseSwagger();
            app.UseSwaggerUI();

        }

        app.UseAuthorization();

        builder.Services.AddAuthentication();

        app.UseAuthentication();
        app.UseCors("AllowReactApp");
        app.MapControllers();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}