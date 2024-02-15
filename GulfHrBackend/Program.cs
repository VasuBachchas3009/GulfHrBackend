
using System;
using GulfHrBackend.BLL.Interface;
using GulfHrBackend.BLL.Service;
using GulfHrBackend.Core.Middleware;
using GulfHrBackend.DLL;
using GulfHrBackend.DLL.Interface;
using GulfHrBackend.DLL.Repository;
using Microsoft.EntityFrameworkCore;
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
