using SalesManagement.Infrastructure;
using SalesManagement.Application;
using System.Text.Json.Serialization;

namespace SalesManagement.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add layer dependency
            builder.Services.ConfigureInfrastructure(builder.Configuration);
            builder.Services.ConfigureApplication();

            // Add services to the container.

            builder.Services.AddControllers()
                .AddNewtonsoftJson(option =>
                     option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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


            app.MapControllers();

            app.Run();
        }
    }
}
