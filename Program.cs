using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using AuraCommerce.Data;
using AuraCommerce.Services;

namespace AuraCommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AuraCommerceContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("AuraCommerceContext") ?? throw new InvalidOperationException("Connection string 'AuraCommerceContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews()
            .AddRazorRuntimeCompilation();

            builder.Services.AddScoped<SeedingService>();
            builder.Services.AddScoped<SellerService>();
            builder.Services.AddScoped<DepartmentService>();
            builder.Services.AddTransient<EmailService>();

            var app = builder.Build();

            var ptBR = new CultureInfo("pt-BR");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(ptBR),
                SupportedCultures = new List<CultureInfo> { ptBR },
                SupportedUICultures = new List<CultureInfo> { ptBR }
            };

            app.UseRequestLocalization(localizationOptions);

            if (app.Environment.IsDevelopment())
            {
                // Cria um escopo temporário para pegar o serviço de Seed
                using (var scope = app.Services.CreateScope())
                {
                    var seedingService = scope.ServiceProvider.GetRequiredService<SeedingService>();
                    seedingService.Seed();
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AuraCommerceContext>();
                var seedingService = services.GetRequiredService<SeedingService>();

                // Isso garante que o banco seja criado e os dados inseridos
                context.Database.EnsureCreated();
                seedingService.Seed();
            }

            app.Run();
        }
    }
}