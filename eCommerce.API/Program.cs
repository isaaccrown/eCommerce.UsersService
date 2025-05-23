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

            var app = builder.Build();
            app.UseExceptionHandlingMiddleware();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers(); 
            app.Run();
        }
    }
}
