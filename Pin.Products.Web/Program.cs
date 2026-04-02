using Pin.Products.Core.Services;
using Pin.Products.Core.Services.Interfaces;
using Pin.Products.Web.Components;

namespace Pin.Products.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            //register the httpClientService
            builder.Services.AddHttpClient();
            //register service classes
            builder.Services.AddScoped<ICategoryApiService, CategoryApiService>();
            builder.Services.AddScoped<IProductApiService, ProductApiService>();
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
