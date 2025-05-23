using eCommerce.Infrastructure;
using eCommerce.Core;
using eCommerce.API.Middlewares;
using System.Text.Json.Serialization;
using eCommerce.Core.Mapper;
using FluentValidation.AspNetCore;
namespace eCommerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructure();
            builder.Services.AddCore();

            builder.Services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile));

            builder.Services.AddFluentValidationAutoValidation();

            //Add API explorer services
            builder.Services.AddEndpointsApiExplorer();

            //Add swagger generation services to create swagger specification
            builder.Services.AddSwaggerGen();

            //Add cors services
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            var app = builder.Build();
            app.UseExceptionHandlingMiddleware();

            //Routing
            app.UseRouting();
            app.UseSwagger(); //Adds endpoint that can serve the swagger.json
            app.UseSwaggerUI(); //Adds swagger UI (interactive page to explore and test API endpoints)
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers(); 
            app.Run();
        }
    }
}
