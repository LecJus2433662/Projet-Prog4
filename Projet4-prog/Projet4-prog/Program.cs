using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Projet4_prog.Data;
using Projet4_prog.Models;
using Projet4_prog.Services;
using Serilog;
using System.Text;

namespace Projet4_prog
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/boutique.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Host.UseSerilog();

            // Base de données
            builder.Services.AddDbContext<Projet4_progContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Projet4_progContext")
                    ?? throw new InvalidOperationException("Connection string 'Projet4_progContext' not found.")));

            // Identity
            builder.Services.AddIdentity<UtilisateurApplication, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<Projet4_progContext>()
            .AddDefaultTokenProviders();

            // JWT
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
                    ValidIssuer = builder.Configuration["Jwt:Emetteur"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Cle"]!))
                };
            });

            // AutoMapper
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            // Services
            builder.Services.AddScoped<IProduitService, ProduitService>();
            builder.Services.AddScoped<ICommandeService, CommandeService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Swagger avec support JWT
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Entrez votre token JWT ici."
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Seed des rôles et de l'admin
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == 401)
                {
                    response.ContentType = "application/json";
                    await response.WriteAsync("{\"message\": \"Vous devez être connecté pour accéder à cette ressource.\"}");
                }
                else if (response.StatusCode == 403)
                {
                    response.ContentType = "application/json";
                    await response.WriteAsync("{\"message\": \"Accès refusé. Vous n'avez pas les droits administrateur pour effectuer cette action.\"}");
                }
            });
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UtilisateurApplication>>();

                foreach (var role in new[] { "Admin", "Utilisateur" })
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }

                if (await userManager.FindByNameAsync("admin") == null)
                {
                    var admin = new UtilisateurApplication
                    {
                        UserName = "admin",
                        Email = "admin@boutique.com"
                    };
                    await userManager.CreateAsync(admin, "Admin123!");
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}