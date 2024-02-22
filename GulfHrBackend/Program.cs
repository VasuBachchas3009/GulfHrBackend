
using System;
using GulfHrBackend.BLL.Interface;
using GulfHrBackend.BLL.Service;
using GulfHrBackend.Core.Middleware;
using GulfHrBackend.DLL;
using GulfHrBackend.DLL.Interface;
using GulfHrBackend.DLL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GulfHrBackend.Core.DTO;
using System.Text.Json;
using Microsoft.OpenApi.Models;
namespace GulfHrBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddTransient<IScheduleService,ScheduleService>();
            builder.Services.AddTransient<IScheduleRepository, ScheduleRepository>();


            builder.Services.AddDbContext<AppDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb"))
);
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration["Jwt:Key"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                // Skip the default logic.
                context.HandleResponse();
                CustomResponseDto<string> customExceptionDto = new CustomResponseDto<string>();
                customExceptionDto.RequestId = new Guid();
                context.Response.StatusCode = 401;
                customExceptionDto.Code = 401;
                customExceptionDto.Status = "Failure";
                customExceptionDto.Message = "Unauthorized User";
                customExceptionDto.Errors = "";
                customExceptionDto.data = "";
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync(JsonSerializer.Serialize(customExceptionDto));
            }
        };
    });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GulfHr Backend", Version = "v1" });

                // Define the BearerAuth scheme that's in use
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Make sure the bearer token is applied to all endpoints
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
