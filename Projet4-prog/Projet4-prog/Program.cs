using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Projet4_prog.Data;


namespace Projet4_prog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Projet4_progContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Projet4_progContext") ?? throw new InvalidOperationException("Connection string 'Projet4_progContext' not found.")));

            // Add services to the container.
            builder.Services.AddDbContext<Projet4_progContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("Projet4_progContext")
?? throw new InvalidOperationException("Connection string 'Projet4_progContext' not found.")));
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

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


            app.MapControllers();

            app.Run();
        }
    }
}
