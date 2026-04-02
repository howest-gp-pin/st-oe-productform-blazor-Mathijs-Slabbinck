using Pin.Products.Core.Services;
using Pin.Products.Core.Services.Interfaces;
using Pin.Products.Web.Components;

namespace Pin.Products.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient<ICategoryApiService, CategoryApiService>(client =>
            {
                string baseUrl = builder.Configuration["ApiSettings:CategoryBaseUrl"]
                    ?? "https://api.escuelajs.co/api/v1/categories/";
                client.BaseAddress = new Uri(baseUrl);
            });

            builder.Services.AddHttpClient<IProductApiService, ProductApiService>(client =>
            {
                string baseUrl = builder.Configuration["ApiSettings:ProductBaseUrl"]
                    ?? "https://api.escuelajs.co/api/v1/products/";
                client.BaseAddress = new Uri(baseUrl);
            });

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            WebApplication app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                context.Response.Headers["X-Frame-Options"] = "DENY";
                context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
                await next();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
