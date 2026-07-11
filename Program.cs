
using Microsoft.EntityFrameworkCore;
using ProductManagementWebAPI.Automapper;
using ProductManagementWebAPI.Data;
using ProductManagementWebAPI.Repository;

namespace ProductManagementWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            //Add AppDbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb"));
            });

            // Add services to the container.
            builder.Services.AddScoped<IProductRepository, ProductRepository>();



            //Add Automapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"];
            }, typeof(AppMapper).Assembly);

            builder.Services.AddControllers();
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
